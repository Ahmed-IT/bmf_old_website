using BMF_WebSite.Models;
using BMF_WebSite.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;

namespace BMF_WebSite.Controllers
{
    public class HomeController : Controller
    {
        BMFEntities1 db = new BMFEntities1();

        public async Task<ActionResult> Index()
        {
            try
            {
                ViewModels.MainPageVM vm = new ViewModels.MainPageVM();
                vm.GetData();

                return View(vm);
            }
            catch (Exception ex)
            {

            }

            return View();
        }             

        public ActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}