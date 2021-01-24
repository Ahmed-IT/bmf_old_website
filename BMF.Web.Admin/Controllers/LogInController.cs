using _Standards.Library.AdminLTETheme;
using _Standards.Library.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BMF.Web.Admin.Controllers
{
    public class LogInController : AdminLTEController
    {
        #region Views

        // GET: LogIn
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region APIs

        [ValidateAntiForgeryToken]
        [HttpPost, ValidateInput(false)]
        // POST: LogIn
        public ActionResult Index(FormBasedUser User, string returnUrl)
        {
            string EncTicket;
            if (AuthenticationManager.Authenticate(User, out EncTicket))
            {
                if (!string.IsNullOrWhiteSpace(EncTicket))
                {
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, EncTicket));
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    SetError("Invalid Username or Password.");
                }
            }
            else
            {
                SetError("Invalid Username or Password..");
            }
            return View();
        }

        #endregion
    }
}