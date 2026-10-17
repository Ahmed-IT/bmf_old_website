using Microsoft.AspNetCore.Mvc;

namespace BMF.Shared.Core.WebFramework
{
    public abstract class BaseController : Controller
    {
        #region Notifications

        protected JsonResult ErrorJson(string error)
        {
            return Json(new { error });
        }

        protected JsonResult ErrorJson(object errors)
        {
            return Json(new { error = errors });
        }

        #endregion

        #region Security

        protected virtual IActionResult UnauthorizedView()
        {
            return RedirectToAction("Unauthorized", "Error", new { area = "" });
        }

        protected virtual IActionResult UnauthorizedPartialView()
        {
            return RedirectToAction("UnauthorizedPV", "Error", new { area = "" });
        }

        protected virtual IActionResult UnauthorizedModalView()
        {
            return RedirectToAction("UnauthorizedPV", "Error", new { area = "", isModal = true });
        }

        protected JsonResult UnauthorizedDataTablesJson()
        {
            return ErrorJson("Unauthorized");
        }

        protected JsonResult UnauthorizedAJAXResponse()
        {
            return ErrorJson("Unauthorized");
        }

        #endregion
    }
}
