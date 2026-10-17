using Microsoft.AspNetCore.Http;

namespace BMF.Shared.Services.Authentication
{
    /// <summary>
    /// Represents default values related to authentication services
    /// </summary>
    public static partial class AuthenticationDefaults
    {

        /// <summary>
        /// The default value used for authentication scheme
        /// </summary>
        public static string AuthenticationScheme => "Authentication";

        /// <summary>
        /// The default value used for external authentication scheme
        /// </summary>
        public static string ExternalAuthenticationScheme => "ExternalAuthentication";

        /// <summary>
        /// The issuer that should be used for any claims that are created
        /// </summary>
        public static string ClaimsIssuer => $"{FixedTerms.Application_Code}";

        /// <summary>
        /// The default value for the login path
        /// </summary>
        public static PathString LoginPath_Admin => new PathString("/LogIn");

        /// <summary>
        /// The default value used for the logout path
        /// </summary>
        public static PathString LogoutPath_Admin => new PathString("/LogOut");

        /// <summary>
        /// The default value for the access denied path
        /// </summary>
        public static PathString AccessDeniedPath => new PathString("/Error/Unauthorized");

        /// <summary>
        /// The default value of the return URL parameter
        /// </summary>
        public static string ReturnUrlParameter => string.Empty;
    }
}
