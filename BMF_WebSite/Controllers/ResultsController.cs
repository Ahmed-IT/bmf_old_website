using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMF_WebSite.Controllers
{
    public class ResultsController : Controller
    {
        // GET: Results
        public ActionResult Index()
        {
            ViewModels.ResultsVM vm = new ViewModels.ResultsVM();
            vm.GetData();

            return View(vm);
        }

        public ActionResult ResultsDetail()
        {
            return View();
        }
    }
}