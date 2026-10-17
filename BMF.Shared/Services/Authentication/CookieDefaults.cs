namespace BMF.Shared.Services.Authentication
{
    public static partial class CookieDefaults
    {
        /// <summary>
        /// Gets the cookie name prefix
        /// </summary>
        public static string Prefix => $"{FixedTerms.Application_Code}";

        /// <summary>
        /// Gets a cookie name of the admin user
        /// </summary>
        public static string AdminUserCookie => ".AdminUser";
        
        /// <summary>
        /// Gets a cookie expiration period of the admin user
        /// </summary>
        public static int AdminUserCookieExpires => 24 * 365;

        /// <summary>
        /// Gets a cookie name of the service provider user
        /// </summary>
        public static string ServiceProviderCookie => ".ServiceProvider";

        /// <summary>
        /// Gets a cookie expiration period of the service provider
        /// </summary>
        public static int ServiceProviderCookieExpires => 24 * 365;

        /// <summary>
        /// Gets a cookie name of the customer user
        /// </summary>
        public static string CustomerCookie => ".Customer";

        /// <summary>
        /// Gets a cookie expiration period of the customer user
        /// </summary>
        public static int CustomerCookieExpires => 24 * 365;

        /// <summary>
        /// Gets a cookie name of the antiforgery
        /// </summary>
        public static string AntiforgeryCookie => ".Antiforgery";

        /// <summary>
        /// Gets a cookie name of the session state
        /// </summary>
        public static string SessionCookie => ".Session";

        /// <summary>
        /// Gets a cookie name of the temp data
        /// </summary>
        public static string TempDataCookie => ".TempData";

        /// <summary>
        /// Gets a cookie name of the authentication
        /// </summary>
        public static string AuthenticationCookie => ".Authentication";
    }
}
