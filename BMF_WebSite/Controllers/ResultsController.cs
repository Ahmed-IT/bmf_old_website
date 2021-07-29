using Newtonsoft.Json;
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

        public ActionResult ResultsDetail(long ID)
        {
            ViewModels.ResultDetailsVM vm = new ViewModels.ResultDetailsVM();
            vm.Get_SeasonsByCategory(ID);
            return View(vm);
        }

        #region AJAX

        public string getRounds(long ID)
        {
            ViewModels.ResultDetailsVM vm = new ViewModels.ResultDetailsVM();
            vm.Get_RoundsBySeason(ID);
            return JsonConvert.SerializeObject(vm.Rounds.Select(s => new { s.ID, s.Round_Number }), settings: new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }); ;
        }

        public string getSessions(long ID)
        {
            ViewModels.ResultDetailsVM vm = new ViewModels.ResultDetailsVM();
            vm.Get_SessionsByRound(ID);
            return JsonConvert.SerializeObject(vm.Sessions.Select(s => new { s.ID, s.Title }), settings: new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        #endregion
    }
}