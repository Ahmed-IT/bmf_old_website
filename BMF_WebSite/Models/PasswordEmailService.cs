using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BMF_WebSite.Models
{
    public static class PasswordEmailService
    {
        private const string ResourcePrefix = "BMF_WebSite.EmailTemplates.";

        public static string Render(string fullName, string emailAddress, string password)
        {
            using (var stream = typeof(PasswordEmailService).Assembly.GetManifestResourceStream(ResourcePrefix + "PasswordRecovery.html"))
            {
                if (stream == null)
                    throw new InvalidOperationException("The password email template is missing.");

                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    // Replace in one pass so values containing template tokens remain unchanged.
                    return System.Text.RegularExpressions.Regex.Replace(reader.ReadToEnd(), @"\{\{(FullName|EmailAddress|Password)\}\}", match =>
                    {
                        switch (match.Groups[1].Value)
                        {
                            case "FullName": return HttpUtility.HtmlEncode(string.IsNullOrWhiteSpace(fullName) ? "BMF member" : fullName);
                            case "EmailAddress": return HttpUtility.HtmlEncode(emailAddress);
                            default: return HttpUtility.HtmlEncode(password);
                        }
                    });
                }
            }
        }

        public static MailMessage CreateMessage(string fullName, string emailAddress, string password)
        {
            var message = new MailMessage();
            try
            {
                message.From = new MailAddress(GetSetting("Username"), "Bahrain Motor Federation");
                message.To.Add(new MailAddress(emailAddress));
                message.Subject = "Your BMF App account details";
                message.SubjectEncoding = Encoding.UTF8;
                message.BodyEncoding = Encoding.UTF8;
                message.Body = "Dear " + (string.IsNullOrWhiteSpace(fullName) ? "BMF member" : fullName) + ",\r\n\r\n"
                    + "You requested your BMF App account details.\r\n\r\n"
                    + "Email address: " + emailAddress + "\r\nPassword: " + password + "\r\n\r\n"
                    + "Open the BMF App and sign in with these details. We recommend changing your password after signing in.\r\n"
                    + "If you did not request this email, contact info@bmf.com.bh.\r\n\r\nBahrain Motor Federation\r\nhttps://bmf.com.bh";
                message.IsBodyHtml = false;

                var htmlView = AlternateView.CreateAlternateViewFromString(Render(fullName, emailAddress, password), Encoding.UTF8, MediaTypeNames.Text.Html);
                message.AlternateViews.Add(htmlView);
                htmlView.LinkedResources.Add(new LinkedResource(CreateLogoHeader(), "image/png")
                {
                    ContentId = "bmf-logo",
                    TransferEncoding = TransferEncoding.Base64
                });
                return message;
            }
            catch
            {
                message.Dispose();
                throw;
            }
        }

        private static Stream CreateLogoHeader()
        {
            using (var source = typeof(PasswordEmailService).Assembly.GetManifestResourceStream(ResourcePrefix + "Logo.png"))
            {
                if (source == null)
                    throw new InvalidOperationException("The BMF email logo is missing.");

                // The entire header is an opaque image: mail apps can recolor HTML backgrounds,
                // but the logo and its white padding must remain part of the same image.
                using (var logo = System.Drawing.Image.FromStream(source))
                using (var header = new System.Drawing.Bitmap(1200, 340, System.Drawing.Imaging.PixelFormat.Format24bppRgb))
                {
                    using (var graphics = System.Drawing.Graphics.FromImage(header))
                    {
                        graphics.Clear(System.Drawing.Color.White);
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        var logoHeight = (int)Math.Round(480.0 * logo.Height / logo.Width);
                        graphics.DrawImage(logo, 80, (340 - logoHeight) / 2, 480, logoHeight);
                    }
                    var output = new MemoryStream();
                    header.Save(output, System.Drawing.Imaging.ImageFormat.Png);
                    output.Position = 0;
                    return output;
                }
            }
        }

        public static async Task SendAsync(string fullName, string emailAddress, string password)
        {
            var accessToken = await OutlookOAuthTokenProvider.GetAccessTokenAsync();
            await SendWithAccessTokenAsync(fullName, emailAddress, password, accessToken);
        }

        public static async Task SendWithAccessTokenAsync(string fullName, string emailAddress, string password, string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                throw new ConfigurationErrorsException("The password email OAuth token is not configured.");
            using (var message = CreateMessage(fullName, emailAddress, password))
            using (var mimeMessage = MimeKit.MimeMessage.CreateFromMailMessage(message))
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                // Require STARTTLS; never downgrade to an unencrypted connection.
                client.Timeout = 30000;
                client.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                await client.ConnectAsync(GetSetting("Host"), int.Parse(GetSetting("Port")), MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(new MailKit.Security.SaslMechanismOAuth2(GetSetting("Username"), accessToken));
                await client.SendAsync(mimeMessage);
                try { await client.DisconnectAsync(true); }
                catch (Exception ex) when (ex is IOException || ex is MailKit.ProtocolException
                    || ex is MailKit.Net.Smtp.SmtpCommandException || ex is System.Net.Sockets.SocketException)
                {
                    // DATA was accepted. A failed QUIT must not turn a sent email into a retryable failure.
                    System.Diagnostics.Trace.TraceWarning("BMF password email accepted; SMTP disconnect failed ({0}).", ex.GetType().Name);
                }
            }
        }

        private static string GetSetting(string name)
        {
            return ConfigurationManager.AppSettings["PasswordEmail.Smtp" + name];
        }
    }
}
