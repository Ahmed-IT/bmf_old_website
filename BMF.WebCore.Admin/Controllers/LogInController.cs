using BMF.Shared.Entities.Security;
using BMF.Shared.Extensions;
using BMF.Shared.Services.Authentication;
using BMF.Shared.Services.Security;
using BMF.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BMF.WebCore.Admin.Controllers
{
    [AllowAnonymous]
    public class LogInController : Controller
    {
        private readonly IAdminUserService _adminUserService;
        private readonly IAuthenticationService _authenticationService;

        public LogInController(IAdminUserService adminUserService,
            IAuthenticationService authenticationService)
        {
            _adminUserService = adminUserService;
            _authenticationService = authenticationService;
        }

        #region Views

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ForgotMyPassword()
        {
            return View();
        }

        #endregion

        #region APIs

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LogInViewModel LogInRequest, string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var loginResult = _adminUserService.ValidateAuthentication(LogInRequest.Username, LogInRequest.Password);
                    switch (loginResult.systemUserLoginResultStatus)
                    {
                        case SystemUserLoginResultStatus.Successful:
                            AdminUser adminUser = null;
                            switch (loginResult.systemUserLoginType)
                            {
                                case SystemUserLoginType.username:
                                    adminUser = _adminUserService.Get_ByUsername(LogInRequest.Username);
                                    break;
                                case SystemUserLoginType.email:
                                    adminUser = _adminUserService.Get_ByEmail(LogInRequest.Username);
                                    break;
                                default:
                                    this.SetError("Invalid credentials.", "The credentials provided are invalid.");
                                    break;
                            }
                            _authenticationService.SignIn_AdminUser(adminUser, LogInRequest.RememberMe);

                            if (Url.IsLocalUrl(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return Redirect("/");
                            }
                        case SystemUserLoginResultStatus.AccountNotExist:
                        case SystemUserLoginResultStatus.WrongPassword:
                        case SystemUserLoginResultStatus.NotActive:
                        case SystemUserLoginResultStatus.Deleted:
                        case SystemUserLoginResultStatus.NotRegistered:
                        case SystemUserLoginResultStatus.LockedOut:
                        default:
                            this.SetError("Invalid credentials.", "The credentials provided are invalid.");
                            break;
                    }
                }
            }
            catch (Exception LogError)
            {
                this.SetError("Error Occured.", "Failed to log in. Please try again later");
                //throw;
            }
            return View(LogInRequest);
        }

        #endregion
    }
}
