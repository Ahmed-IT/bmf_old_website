using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Configuration;
using System.Collections.Generic;
using _Standards.Library.Security;
using _Standards.Library.AdminLTETheme;

namespace _Standards.Library.AdminLTETheme
{
    public class _ErrorController : Controller
    {
        public ActionResult Index(string id)
        {
            ViewBag.Code = id;
            switch (id)
            {
                case "404":
                    ViewBag.Title = "Page cannot be found";
                    break;
                case "505":
                case "500":
                    ViewBag.Title = "There seems to be a problem with your request";
                    break;
                default:
                    ViewBag.Title = "Page cannot be found";
                    break;
            }
            ViewBag.Message = "If the problem persists, please report it to support.";
            Response.AddHeader("X-Frame-Options", "DENY");
            return View("~/_Standards/Library/AdminLTETheme/Views/_Error/Index.cshtml");
        }

        public ActionResult IndexPV(string id)
        {
            ViewBag.Code = id;
            switch (id)
            {
                case "404":
                    ViewBag.Title = "Page cannot be found";
                    break;
                case "505":
                case "500":
                    ViewBag.Title = "There seems to be a problem with your request";
                    break;
                default:
                    ViewBag.Title = "Page cannot be found";
                    break;
            }
            ViewBag.Message = "If the problem persists, please report it to support.";
            Response.AddHeader("X-Frame-Options", "DENY");
            return PartialView("~/_Standards/Library/AdminLTETheme/Views/_Error/IndexPV.cshtml");
        }

        public ActionResult _Unauthorized()
        {
            if (AuthenticationManager.IsAuthenticated())
            {
                ViewBag.Title = "Unauthorized Access";
                ViewBag.Message = "You are not authorized to perform this action.";
                Response.AddHeader("X-Frame-Options", "DENY");
                return View("~/_Standards/Library/AdminLTETheme/Views/_Error/_Unauthorized.cshtml");
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }

        public ActionResult _UnauthorizedPV()
        {
            ViewBag.Title = "Unauthorized Access";
            ViewBag.Message = "You are not authorized to perform this action.";
            Response.AddHeader("X-Frame-Options", "DENY");
            return PartialView("~/_Standards/Library/AdminLTETheme/Views/_Error/_UnauthorizedPV.cshtml");
        }
    }
}