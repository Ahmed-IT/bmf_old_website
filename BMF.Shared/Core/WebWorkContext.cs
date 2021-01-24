using BMF.Shared.Entities.Security;
using BMF.Shared.Services.Authentication;
using BMF.Shared.Services.Security;
using Microsoft.AspNetCore.Http;
using System;

namespace BMF.Shared.Core
{
    public partial class WebWorkContext : IWorkContext
    {
        #region Fields

        private SystemUserType? _cachedsystemUserType;
        private LoggedInUserViewModel _cachedLoggedInUserViewModel;
        private AdminUser _cachedAdminUser;

        private readonly IAuthenticationService _authenticationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IAdminUserService _adminUserService;

        #endregion

        #region Properties

        public bool CreateGuestAccount { get; set; }

        public virtual SystemUserType? CurrentSystemUserType
        {
            get
            {
                if (_cachedsystemUserType != null)
                    return _cachedsystemUserType;

                if (CurrentAdminUser != null)
                    _cachedsystemUserType = SystemUserType.Administrator;
                else
                    _cachedsystemUserType = SystemUserType.None;

                return _cachedsystemUserType;
            }
            set
            {
                _cachedsystemUserType = value;
            }
        }

        public virtual LoggedInUserViewModel CurrentLoggedInUserViewModel
        {
            get
            {
                if (_cachedLoggedInUserViewModel != null)
                    return _cachedLoggedInUserViewModel;

                if (CurrentAdminUser != null)
                    _cachedLoggedInUserViewModel = new LoggedInUserViewModel(_cachedAdminUser);
                else
                    _cachedLoggedInUserViewModel = null;

                return _cachedLoggedInUserViewModel;
            }
            set
            {
                _cachedLoggedInUserViewModel = value;
            }
        }

        public virtual AdminUser CurrentAdminUser
        {
            get
            {
                //whether there is a cached value
                if (_cachedAdminUser != null)
                    return _cachedAdminUser;

                AdminUser adminUser = null;

                if (adminUser == null || adminUser.DeletedOnUtc.HasValue || !adminUser.IsActive)
                {
                    //try to get registered user
                    adminUser = _authenticationService.GetAuthenticated_AdminUser();
                }

                if (adminUser == null || adminUser.DeletedOnUtc.HasValue || !adminUser.IsActive)
                {
                    //try to get user from cookie
                    var adminUserCookie = GetAdminUserCookie();
                    if (!string.IsNullOrEmpty(adminUserCookie))
                    {
                        var adminUserByCookie = _adminUserService.Get_ByID(long.Parse(adminUserCookie));
                        if (adminUserByCookie != null)
                            adminUser = adminUserByCookie;
                    }
                }

                if (adminUser != null && !adminUser.DeletedOnUtc.HasValue && adminUser.IsActive)
                {
                    //set customer cookie
                    SetAdminUserCookie(adminUser.ID);

                    //cache the found customer
                    _cachedAdminUser = adminUser;
                    _cachedsystemUserType = SystemUserType.Administrator;
                }

                return _cachedAdminUser;
            }
            set
            {
                SetAdminUserCookie(value.ID);
                _cachedsystemUserType = SystemUserType.Administrator;
                _cachedAdminUser = value;
            }
        }

        #endregion

        #region Ctor

        public WebWorkContext(IAuthenticationService authenticationService,
            IHttpContextAccessor httpContextAccessor,
            IAdminUserService adminUserService)
        {
            _authenticationService = authenticationService;
            _httpContextAccessor = httpContextAccessor;
            _adminUserService = adminUserService;
        }

        #endregion

        #region Methods

        protected virtual string GetAdminUserCookie()
        {
            var cookieName = $"{CookieDefaults.Prefix}{CookieDefaults.AdminUserCookie}";
            return _httpContextAccessor.HttpContext?.Request?.Cookies[cookieName];
        }
        protected virtual void SetAdminUserCookie(long adminUserID)
        {
            if (_httpContextAccessor.HttpContext?.Response == null)
                return;

            //delete current cookie value
            var cookieName = $"{CookieDefaults.Prefix}{CookieDefaults.AdminUserCookie}";
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookieName);

            //get date of cookie expiration
            var cookieExpires = CookieDefaults.AdminUserCookieExpires;
            var cookieExpiresDate = DateTime.Now.AddHours(cookieExpires);

            //if passed guid is empty set cookie as expired
            if (adminUserID == 0)
                cookieExpiresDate = DateTime.Now.AddMonths(-1);

            //set new cookie value
            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = cookieExpiresDate,
                Secure = _httpContextAccessor.HttpContext.Request.IsHttps
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, adminUserID.ToString(), options);
        }

        #endregion
    }
}
