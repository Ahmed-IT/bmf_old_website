# BMF App password email

`POST /api/SendAccountPassword` accepts `{"Email_Address":"member@example.com"}`. It trims and validates exactly one email address, performs a case-insensitive lookup of active, non-deleted accounts, and emails only the matched account's stored password to its registered address. Request-supplied IDs, names, and passwords cannot select the account or replace its credentials. Missing/deleted/inactive accounts and ambiguous active matches never send email. The existing successful `200 "Sent"` wire response is preserved.

## Recovery HTTP contract and app messages

Error bodies are JSON strings with safe descriptions; SMTP, OAuth, and database diagnostics are not returned. The latest app maps the HTTP status to the following text rather than displaying arbitrary server response bodies:

| HTTP | Meaning | Latest app message |
| --- | --- | --- |
| 200, JSON `"Sent"` | The registered account's email was accepted by the mail server | Password email sent. Please check your inbox and junk folder. |
| 400 | Missing/malformed email, multiple addresses, header injection, or invalid model | Enter a valid email address. |
| 404 | No active, non-deleted account matches the email | This email address was not found for an active account. Check the address or contact BMF support. |
| 409 | More than one active account matches | More than one active account uses this email address. Please contact BMF support. |
| 500 | Database/processing failure or invalid stored account details | Unable to process your password request. Please try again later. |
| 502 | Permanent SMTP command/recipient/message rejection | The email server could not accept your password email. Please contact BMF support. |
| 503 | Temporary SMTP rejection, authentication, configuration, or connection failure | Password recovery email is temporarily unavailable. Please try again later. |

The app additionally maps upstream 401/403, 408/504, and 429 to safe account-access, timeout, and rate-limit messages. It rejects all non-200 responses and unexpected/malformed success bodies. Timeout guidance asks the member to check their inbox before retrying, since a request may have reached the server. User cancellation remains cancellation; the form stays open on errors, shows the message, and clears its busy indicator in `finally`. The recovery request includes only the trimmed email, never stale credentials from another account form.

An SMTP disconnect failure after acceptance does not turn a submitted email into a retryable failure. SMTP acceptance does not independently confirm inbox delivery.

The design uses the website's red (`#cb1a2b`), charcoal (`#21252e`), white background, and BMF logo. The HTML and logo are embedded in the application assembly so they are included in builds and publishing. The email uses inline styles, presentation tables, a mobile layout, an embedded CID logo, HTML-encoded member values, and a plain text fallback.

The logo header is composed as a 1200-by-340-pixel RGB image with opaque white padding around the existing BMF logo. The entire image scales to the email width. This puts the white background into the MIME image attachment itself, addressing the reported mail-client dark mode override of HTML background colors. The attachment has no alpha channel, and tests inspect its white pixels. The original website logo asset is unchanged.

Open [preview.html](preview.html) to view the actual rendered template with fictional account details. Edit `BMF_WebSite/EmailTemplates/PasswordRecovery.html` to change the design, then rebuild and run the checks to refresh the preview.

## OAuth2 SMTP configuration

The public settings in `BMF_WebSite/Web.config` specify `smtp-mail.outlook.com`, port `587`, and `bahrainmotorfederation@outlook.com`. The sender uses MailKit, TLS 1.2, mandatory STARTTLS, and SASL XOAUTH2. SMTP password authentication is not used.

The **BMF Password Email** application was registered in the signed-in Entra directory with client ID `7c6d3bb7-8f32-4aed-a8a6-b3761bde16bd`. It supports personal Microsoft accounts and organizational accounts, with delegated Exchange `SMTP.Send` permission. The mailbox signs in against the `consumers` authority. It needs neither an app password nor a client secret.

The mailbox owner completed Microsoft consent on 30 October 2026. Live checks verified token renewal, access-token caching, and Outlook SMTP authentication using the compiled .NET application. A requested test email was sent to `a.it@live.com` with subject `[TEST] Your BMF App account details` and fictional password details. Outlook accepted it with SMTP status `2.0.0 OK`; inbox receipt has not been independently checked.

The refresh token is stored only in `BMF_WebSite/App_Data/PasswordEmail.tokens.local.config`, excluded from Git and publishing. The server caches access tokens until one minute before expiry, serializes refresh requests, and saves rotated refresh tokens atomically. Tokens and SMTP diagnostics are never returned to API callers or logged.

For deployment, provision that private token file separately into the deployed `App_Data` directory, or set `BMF_MAIL_OAUTH_REFRESH_TOKEN` as the initial refresh token. The application identity needs write access to `App_Data` to persist rotated tokens. Keep this file accessible only to the application identity and administrators. An existing token file takes precedence over the initial environment value. Re-authorize if Microsoft revokes the grant or requires a new sign-in:

```powershell
python tools/setup-password-email-oauth.py
```

This opens a Microsoft device sign-in flow requesting `offline_access` and `https://outlook.office.com/SMTP.Send`, verifies authentication as the exact configured mailbox, and saves the token privately. It sends no email. The setup script uses the registered client ID by default; use `--client-id` and `--tenant` for another registration. Public application settings can be overridden in the optional `App_Data/PasswordEmail.local.config` file; an example is supplied.

Microsoft documents the [SMTP OAuth2 flow](https://learn.microsoft.com/en-us/exchange/client-developer/legacy-protocols/how-to-authenticate-an-imap-pop-smtp-application-by-using-oauth) and the [device authorization flow](https://learn.microsoft.com/en-us/entra/identity-platform/v2-oauth2-device-code).

## Verification

Build `BMF_WebSite/BMF_WebSite.csproj` with Visual Studio MSBuild, then run:

```powershell
python tests/PasswordEmail/verify.py
```

The checks use the compiled website assembly. They cover HTML encoding, literal template tokens in passwords, missing profiles, recipient isolation, plain text and HTML parts, the opaque inline logo, invalid API requests, account eligibility, normalized lookup and registered recipient selection, duplicate matches, stored-data failures, HTTP statuses, awaited submission, successful STARTTLS/XOAUTH2 delivery to a local SMTP fixture, authentication rejection, and disconnect failure after acceptance. Account lookup and delivery boundaries are overridden with synthetic data for controller tests; the production EF query is not executed against a live database. The fixture uses a fictional token and a short-lived certificate trusted only by the test executable. Python's `cryptography` package is required for the test certificate. No real member accounts or external recipients are used. The test executable may require normal Windows TLS provider access when run from a restricted sandbox.

After these checks, optionally verify live OAuth token renewal and SMTP authentication, without sending mail:

```powershell
.\artifacts\password-email-checks\bin\PasswordEmailChecks.exe --live (Resolve-Path BMF_WebSite).Path
```

To explicitly send a test email with the actual template and a fictional password to a chosen recipient:

```powershell
.\artifacts\password-email-checks\bin\PasswordEmailChecks.exe --send-test (Resolve-Path BMF_WebSite).Path recipient@example.com
```

The responsive design was checked at 320, 360, 375, 390, 414, 600, 768, 900, 1024, and 1440 pixels. Normal content and unusually long unbroken member names, email addresses, and passwords fit without horizontal overflow. The layout uses fluid tables, a maximum card width, wrapping account details, a scalable logo, and mobile spacing and typography. See [mobile preview](preview-mobile.png) and [desktop preview](preview-desktop.png). These are browser layout checks; rendering inside individual email clients has not been independently tested.
