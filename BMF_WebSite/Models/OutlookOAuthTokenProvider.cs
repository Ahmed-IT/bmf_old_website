using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace BMF_WebSite.Models
{
    public static class OutlookOAuthTokenProvider
    {
        private static readonly HttpClient Client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
        private static readonly SemaphoreSlim RefreshLock = new SemaphoreSlim(1, 1);
        private static string accessToken;
        private static DateTime expiresAtUtc;

        public static async Task<string> GetAccessTokenAsync()
        {
            await RefreshLock.WaitAsync();
            try
            {
                if (!string.IsNullOrEmpty(accessToken) && DateTime.UtcNow < expiresAtUtc)
                    return accessToken;

                var clientId = ConfigurationManager.AppSettings["PasswordEmail.OAuthClientId"];
                var tenant = ConfigurationManager.AppSettings["PasswordEmail.OAuthTenant"] ?? "consumers";
                var tokenPath = Path.Combine(HostingEnvironment.MapPath("~/App_Data")
                    ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data"), "PasswordEmail.tokens.local.config");
                string refreshToken = null;
                if (File.Exists(tokenPath))
                    refreshToken = XDocument.Load(tokenPath).Root.Elements("add")
                        .Where(e => (string)e.Attribute("key") == "PasswordEmail.OAuthRefreshToken")
                        .Select(e => (string)e.Attribute("value")).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(refreshToken))
                    refreshToken = Environment.GetEnvironmentVariable("BMF_MAIL_OAUTH_REFRESH_TOKEN");
                if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(refreshToken))
                    throw new ConfigurationErrorsException("Authorize the BMF Outlook mailbox using tools/setup-password-email-oauth.py.");

                using (var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "client_id", clientId },
                    { "grant_type", "refresh_token" },
                    { "refresh_token", refreshToken },
                    { "scope", "https://outlook.office.com/SMTP.Send offline_access" }
                }))
                using (var response = await Client.PostAsync("https://login.microsoftonline.com/" + Uri.EscapeDataString(tenant) + "/oauth2/v2.0/token", content))
                {
                    // OAuth server responses can contain secrets. Keep diagnostics out of API responses and logs.
                    if (!response.IsSuccessStatusCode)
                        throw new InvalidOperationException("Outlook mailbox authorization must be renewed.");
                    var result = JObject.Parse(await response.Content.ReadAsStringAsync());
                    var nextAccessToken = (string)result["access_token"];
                    var lifetime = (int?)result["expires_in"] ?? 3600;
                    if (string.IsNullOrWhiteSpace(nextAccessToken) || lifetime <= 60)
                        throw new InvalidOperationException("Microsoft returned an invalid OAuth token response.");

                    var rotatedRefreshToken = (string)result["refresh_token"];
                    if (!string.IsNullOrWhiteSpace(rotatedRefreshToken))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(tokenPath));
                        var temporaryPath = tokenPath + ".tmp";
                        new XDocument(new XElement("appSettings", new XElement("add",
                            new XAttribute("key", "PasswordEmail.OAuthRefreshToken"),
                            new XAttribute("value", rotatedRefreshToken)))).Save(temporaryPath);
                        if (File.Exists(tokenPath))
                            File.Replace(temporaryPath, tokenPath, null);
                        else
                            File.Move(temporaryPath, tokenPath);
                    }
                    accessToken = nextAccessToken;
                    expiresAtUtc = DateTime.UtcNow.AddSeconds(lifetime - 60);
                    return accessToken;
                }
            }
            finally
            {
                RefreshLock.Release();
            }
        }
    }
}
