using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using BMF.Shared.Entities.Security;
using BMF.Shared.Services.Security;

namespace BMF.Shared.Services.Authentication
{
    /// <summary>
    /// Represents service using cookie middleware for the authentication
    /// </summary>
    public partial class CookieAuthenticationService : IAuthenticationService
    {
        #region Fields

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAdminUserService _adminUserService;

        private AdminUser _cachedAdminUser;

        #endregion

        #region Ctor

        public CookieAuthenticationService(IHttpContextAccessor httpContextAccessor,
            IAdminUserService adminUserService)
        {
            _httpContextAccessor = httpContextAccessor;
            _adminUserService = adminUserService;
        }

        #endregion

        #region Methods

        public virtual async void SignIn_AdminUser(AdminUser adminUser, bool isPersistent)
        {
            if (adminUser == null)
                throw new ArgumentNullException(nameof(adminUser));

            //create claims for admin users's username and email
            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(adminUser.Username))
                claims.Add(new Claim(ClaimTypes.GivenName, adminUser.Username, ClaimValueTypes.String, AuthenticationDefaults.ClaimsIssuer));

            if (!string.IsNullOrEmpty(adminUser.Email))
                claims.Add(new Claim(ClaimTypes.Email, adminUser.Email, ClaimValueTypes.Email, AuthenticationDefaults.ClaimsIssuer));

            claims.Add(new Claim(ClaimTypes.Name, adminUser.ID.ToString(), ClaimValueTypes.String, AuthenticationDefaults.ClaimsIssuer));

            //create principal for the current authentication scheme
            var userIdentity = new ClaimsIdentity(claims, AuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            //set value indicating whether session is persisted and the time at which the authentication was issued
            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                IssuedUtc = DateTime.UtcNow
            };

            //sign in
            await _httpContextAccessor.HttpContext.SignInAsync(AuthenticationDefaults.AuthenticationScheme, userPrincipal, authenticationProperties);

            //cache authenticated customer
            _cachedAdminUser = adminUser;
        }
        public virtual async void SignOut_AdminUser()
        {
            //reset cached customer
            _cachedAdminUser = null;

            //and sign out from the current authentication scheme
            await _httpContextAccessor.HttpContext.SignOutAsync(AuthenticationDefaults.AuthenticationScheme);

            _httpContextAccessor.HttpContext.Response.Cookies.Delete($"{CookieDefaults.Prefix}{CookieDefaults.AdminUserCookie}");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete($"{CookieDefaults.Prefix}{CookieDefaults.AuthenticationCookie}");
        }
        public virtual AdminUser GetAuthenticated_AdminUser()
        {
            //whether there is a cached customer
            if (_cachedAdminUser != null)
                return _cachedAdminUser;

            //try to get authenticated user identity
            var authenticateResult = _httpContextAccessor.HttpContext.AuthenticateAsync(AuthenticationDefaults.AuthenticationScheme).Result;
            if (!authenticateResult.Succeeded)
                return null;

            AdminUser adminUser = null;

            //try to get customer by ID
            var IDClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Name
                && claim.Issuer.Equals(AuthenticationDefaults.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));
            if (IDClaim != null)
                adminUser = _adminUserService.Get_ByID(long.Parse(IDClaim.Value));

            //try to get customer by username
            var usernameClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.GivenName
                && claim.Issuer.Equals(AuthenticationDefaults.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));
            if (usernameClaim != null)
                adminUser = _adminUserService.Get_ByUsername(usernameClaim.Value);

            //try to get customer by email
            var emailClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Email
                && claim.Issuer.Equals(AuthenticationDefaults.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));
            if (emailClaim != null)
                adminUser = _adminUserService.Get_ByEmail(emailClaim.Value);


            //whether the found customer is available
            if (adminUser == null || !adminUser.IsActive || adminUser.DeletedOnUtc.HasValue)
                return null;

            //cache authenticated customer
            _cachedAdminUser = adminUser;

            return _cachedAdminUser;
        }

        #endregion
    }
}
