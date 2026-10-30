"""Build the checks, exercise SMTP against loopback only, and render the real template.

Run after building BMF_WebSite: python tests/PasswordEmail/verify.py
Requires the cryptography package to generate an ephemeral test TLS certificate.
"""
from pathlib import Path
import base64
import ctypes
import datetime
from email import policy
from email.parser import BytesParser
import socket
import re
import shutil
import ssl
import subprocess
import threading
import xml.etree.ElementTree as ET
from cryptography import x509
from cryptography.hazmat.primitives import hashes, serialization
from cryptography.hazmat.primitives.asymmetric import rsa
from cryptography.x509.oid import NameOID

ROOT = Path(__file__).resolve().parents[2]
# Prevent Windows error dialogs from keeping a failed test executable alive.
ctypes.windll.kernel32.SetErrorMode(0x0001 | 0x0002)
WORK = ROOT / 'artifacts/password-email-checks'
WORK.mkdir(parents=True, exist_ok=True)
OUT = WORK / 'bin'
OUT.mkdir(parents=True, exist_ok=True)
for assembly in (ROOT / 'BMF_WebSite/bin').glob('*.dll'):
    shutil.copy2(assembly, OUT / assembly.name)
key = rsa.generate_private_key(public_exponent=65537, key_size=2048)
name = x509.Name([x509.NameAttribute(NameOID.COMMON_NAME, 'localhost')])
now = datetime.datetime.now(datetime.timezone.utc)
cert = (x509.CertificateBuilder().subject_name(name).issuer_name(name).public_key(key.public_key())
        .serial_number(x509.random_serial_number()).not_valid_before(now - datetime.timedelta(minutes=1))
        .not_valid_after(now + datetime.timedelta(hours=1))
        .add_extension(x509.SubjectAlternativeName([x509.DNSName('localhost')]), critical=False).sign(key, hashes.SHA256()))
cert_path = WORK / 'fixture.crt'
cert_path.write_bytes(cert.public_bytes(serialization.Encoding.PEM))
key_path = WORK / 'fixture.key'
key_path.write_bytes(key.private_bytes(serialization.Encoding.PEM, serialization.PrivateFormat.PKCS8, serialization.NoEncryption()))
tls = ssl.SSLContext(ssl.PROTOCOL_TLS_SERVER)
tls.load_cert_chain(cert_path, key_path)
listener = socket.socket()
listener.bind(('127.0.0.1', 0))
listener.listen(2)
listener.settimeout(20)
port = listener.getsockname()[1]
errors = []


def smtp_fixture():
    try:
        for attempt in range(3):
            conn, _ = listener.accept()
            conn.settimeout(15)
            stream = conn.makefile('rwb', buffering=0)

            def reply(value):
                stream.write(value + b'\r\n')

            reply(b'220 localhost test fixture')
            assert stream.readline().upper().startswith(b'EHLO ')
            reply(b'250-localhost\r\n250 STARTTLS')
            assert stream.readline().strip().upper() == b'STARTTLS'
            reply(b'220 Ready for TLS')
            stream.close()
            conn = tls.wrap_socket(conn, server_side=True)
            stream = conn.makefile('rwb', buffering=0)
            assert stream.readline().upper().startswith(b'EHLO ')
            reply(b'250-localhost\r\n250 AUTH XOAUTH2')
            auth = stream.readline().strip().split()
            assert [part.upper() for part in auth[:2]] == [b'AUTH', b'XOAUTH2']
            assert base64.b64decode(auth[2]) == b'user=bahrainmotorfederation@outlook.com\x01auth=Bearer fixture-token\x01\x01'
            if attempt == 1:
                reply(b'535 5.7.3 Authentication unsuccessful')
            else:
                reply(b'235 2.7.0 Authenticated')
                assert stream.readline().upper().startswith(b'MAIL FROM:')
                reply(b'250 OK')
                assert b'member@example.com' in stream.readline()
                reply(b'250 OK')
                assert stream.readline().strip().upper() == b'DATA'
                reply(b'354 End with a dot')
                lines = []
                while True:
                    line = stream.readline()
                    assert line, 'SMTP client disconnected during DATA'
                    if line == b'.\r\n':
                        break
                    lines.append(line[1:] if line.startswith(b'..') else line)
                message = BytesParser(policy=policy.default).parsebytes(b''.join(lines))
                assert message.get_body(preferencelist=('html',)).get_content().find('sample-password') >= 0
                assert any(part.get('Content-ID') == '<bmf-logo>' for part in message.walk())
                reply(b'250 Accepted')
            if stream.readline().strip().upper() == b'QUIT' and attempt != 2:
                reply(b'221 Bye')
            stream.close()
            conn.close()
    except Exception as exc:
        errors.append(exc)
    finally:
        listener.close()


csc = Path(r'C:\Program Files\Microsoft Visual Studio\18\Professional\MSBuild\Current\Bin\Roslyn\csc.exe')
exe = OUT / 'PasswordEmailChecks.exe'
refs = ['BMF_WebSite.dll', 'System.Web.Http.dll', 'System.Net.Http.Formatting.dll', 'EntityFramework.dll', 'MailKit.dll', 'MimeKit.dll']
subprocess.run([str(csc), '/nologo', '/out:' + str(exe),
                '/r:System.Web.dll', '/r:System.Net.Http.dll', '/r:System.Configuration.dll', '/r:System.Drawing.dll',
                *['/r:' + str(OUT / ref) for ref in refs], str(Path(__file__).with_name('Checks.cs')),
                str(Path(__file__).with_name('RecoveryApiChecks.cs'))], check=True)
config = ET.Element('configuration')
startup = ET.SubElement(config, 'startup')
ET.SubElement(startup, 'supportedRuntime', version='v4.0', sku='.NETFramework,Version=v4.7.2')
settings = ET.SubElement(config, 'appSettings')
for suffix, value in [('Host', 'localhost'), ('Port', str(port)), ('Username', 'bahrainmotorfederation@outlook.com')]:
    ET.SubElement(settings, 'add', key='PasswordEmail.Smtp' + suffix, value=value)
runtime = re.search(r'<runtime>.*?</runtime>', (ROOT / 'BMF_WebSite/Web.config').read_text(encoding='utf-8-sig'), re.S).group()
Path(str(exe) + '.config').write_text(ET.tostring(config, encoding='unicode').replace('</configuration>', runtime + '</configuration>'))
thread = threading.Thread(target=smtp_fixture, daemon=True)
thread.start()
preview = ROOT / 'docs/password-email/preview.html'
preview.parent.mkdir(parents=True, exist_ok=True)
result = subprocess.run([str(exe), str(preview), str(cert_path)], cwd=ROOT, timeout=45)
thread.join(timeout=2)
if errors:
    raise RuntimeError('SMTP fixture failed') from errors[0]
if thread.is_alive():
    raise RuntimeError('SMTP fixture did not finish')
if result.returncode:
    raise SystemExit(result.returncode)
print('PASS: MIME message received by local STARTTLS server; rejection path verified. No external email sent.')
