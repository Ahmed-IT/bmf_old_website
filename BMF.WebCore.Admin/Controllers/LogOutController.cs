using BMF.Shared.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMF.WebCore.Admin.Controllers
{
    [AllowAnonymous]
    public class LogOutController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public LogOutController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IActionResult Index()
        {
            _authenticationService.SignOut_AdminUser();
            return Redirect(AuthenticationDefaults.LoginPath_Admin);
        }
    }
}
