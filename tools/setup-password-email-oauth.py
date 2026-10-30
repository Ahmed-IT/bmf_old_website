"""Authorize the BMF Outlook mailbox once; store only a private refresh token.

python tools/setup-password-email-oauth.py
The script checks SMTP authentication but never sends an email.
"""
import argparse
import base64
import json
from pathlib import Path
import smtplib
import ssl
import time
import urllib.error
import urllib.parse
import urllib.request
import xml.etree.ElementTree as ET


def post(url, fields):
    request = urllib.request.Request(url, data=urllib.parse.urlencode(fields).encode())
    try:
        with urllib.request.urlopen(request, timeout=30) as response:
            return json.load(response)
    except urllib.error.HTTPError as error:
        result = json.load(error)
        if result.get('error') in ('authorization_pending', 'slow_down'):
            return result
        # Do not print server responses that could include token material.
        raise SystemExit('Microsoft authorization failed: ' + result.get('error', 'unknown_error')) from None


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--client-id', default='7c6d3bb7-8f32-4aed-a8a6-b3761bde16bd')
    parser.add_argument('--tenant', default='consumers')
    args = parser.parse_args()
    base = 'https://login.microsoftonline.com/' + urllib.parse.quote(args.tenant, safe='') + '/oauth2/v2.0/'
    scope = 'offline_access https://outlook.office.com/SMTP.Send'
    device = post(base + 'devicecode', {'client_id': args.client_id, 'scope': scope})
    print(device['message'], flush=True)
    print('Sign in as bahrainmotorfederation@outlook.com and approve BMF Password Email.', flush=True)
    deadline = time.monotonic() + device['expires_in']
    interval = device.get('interval', 5)
    while time.monotonic() < deadline:
        time.sleep(interval)
        result = post(base + 'token', {'client_id': args.client_id, 'device_code': device['device_code'],
                      'grant_type': 'urn:ietf:params:oauth:grant-type:device_code'})
        if result.get('error') == 'authorization_pending':
            continue
        if result.get('error') == 'slow_down':
            interval += 5
            continue
        if not result.get('access_token') or not result.get('refresh_token'):
            raise SystemExit('Microsoft did not return the required OAuth tokens.')
        # Verify the token belongs to the intended sender before persisting it.
        with smtplib.SMTP('smtp-mail.outlook.com', 587, timeout=30) as smtp:
            smtp.ehlo()
            smtp.starttls(context=ssl.create_default_context())
            smtp.ehlo()
            sasl = 'user=bahrainmotorfederation@outlook.com\x01auth=Bearer ' + result['access_token'] + '\x01\x01'
            code, _ = smtp.docmd('AUTH', 'XOAUTH2 ' + base64.b64encode(sasl.encode()).decode())
            if code == 334:
                code, _ = smtp.docmd('')
            if code != 235:
                raise SystemExit('Outlook rejected OAuth SMTP authentication (status ' + str(code) + '). No tokens saved.')
        root = ET.Element('appSettings')
        ET.SubElement(root, 'add', key='PasswordEmail.OAuthRefreshToken', value=result['refresh_token'])
        destination = Path(__file__).resolve().parents[1] / 'BMF_WebSite/App_Data/PasswordEmail.tokens.local.config'
        destination.parent.mkdir(parents=True, exist_ok=True)
        ET.ElementTree(root).write(destination, encoding='utf-8', xml_declaration=True)
        print('OAuth2 SMTP authentication succeeded. Private refresh token saved. No email sent.', flush=True)
        return
    raise SystemExit('Microsoft sign-in expired. Run this script again when ready to sign in.')


if __name__ == '__main__':
    main()
