using _Standards.Library.AdminLTETheme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BMF.Web.Admin.Controllers
{
    public class LogOutController : AdminLTEController
    {
        // GET: LogOut
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("", controllerName: "LogIn");
        }
    }
}