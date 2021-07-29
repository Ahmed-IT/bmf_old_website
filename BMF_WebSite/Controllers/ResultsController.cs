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
            vm.ID = ID;
            vm.Get_SeasonsByCategory(ID);
            return View(vm);
        }

        #region AJAX

        public string getRounds(long ID, long ID_Season)
        {
            ViewModels.ResultDetailsVM vm = new ViewModels.ResultDetailsVM();
            vm.Get_RoundsBySeason(ID, ID_Season);
            return JsonConvert.SerializeObject(vm.Rounds.Select(s => new { s.ID, s.Round_Number }), settings: new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }); ;
        }

        public string getSessions(long ID, long ID_Season, long ID_Round)
        {
            ViewModels.ResultDetailsVM vm = new ViewModels.ResultDetailsVM();
            vm.Get_SessionsByRound(ID, ID_Season, ID_Round);
            return JsonConvert.SerializeObject(vm.Sessions.Select(s => new { s.ID, s.Title }), settings: new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        public string getChampResults(long ID, long ID_Season)
        {
            ViewModels.ResultDetailsVM vm = new ViewModels.ResultDetailsVM();
            vm.Get_ChampResults(ID, ID_Season);
            var final = vm.Results.GroupBy(g => g.ID_UserProfile).Select(s => new { s.FirstOrDefault().M_UserProfile.FullName, TotalPoints = s.Sum(s1 => s1.Points) }).OrderByDescending(o => o.TotalPoints);
            return JsonConvert.SerializeObject(final, settings: new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        public string getRoundSessionResults(long ID, long ID_Season, long ID_Round, long ID_Session)
        {
            ViewModels.ResultDetailsVM vm = new ViewModels.ResultDetailsVM();
            vm.Get_Results(ID, ID_Season, ID_Round, ID_Session);
            var final = vm.Results.GroupBy(g => g.ID_UserProfile).Select(s => new { s.FirstOrDefault().M_UserProfile.FullName, TotalPoints = s.Sum(s1 => s1.Points) }).OrderByDescending(o => o.TotalPoints);
            return JsonConvert.SerializeObject(final, settings: new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        #endregion
    }
}