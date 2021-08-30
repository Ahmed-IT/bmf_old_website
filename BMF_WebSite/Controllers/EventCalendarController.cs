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
    public class EventCalendarController : Controller
    {
        BMFEntities1 db = new BMFEntities1();

        // GET: EventCalendar
        public async Task<ActionResult> Index()
        {
            try
            {
                EventCalendarVM vm = new EventCalendarVM();
                var getcalndar = await vm.GetCalendarPage();

                return View(getcalndar);
            }
            catch (Exception ex)
            {

            }

            return View();
        }

        public async Task<ActionResult> EventFullCalendar(int ID)
        {
            try
            {
                var query = await (from r in db.M_EventRound
                                   where r.ID_EventCategory == ID
                                   && r.IsActive == true && r.DeletedBy == null
                                   && r.M_EventSeason.IsActive == true
                                   select new ClsEventRound
                                   {
                                       DateTime_End = r.DateTime_End,
                                       DateTime_Start = r.DateTime_Start,
                                       Round_Number = r.Round_Number,
                                       ID_EventCategory = r.ID_EventCategory,
                                       ID = r.ID,
                                       EventCategory = new ClsEventCategory
                                       {
                                           IsActive = r.M_EventCategory.IsActive,
                                           ID = r.M_EventCategory.ID,
                                           Image_URL = r.M_EventCategory.Image_URL,
                                           Title = r.M_EventCategory.Title
                                       }
                                   }).ToListAsync();

                return View(query);
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("EventCalendar", "EventCalendar");
        }

        public async Task<ActionResult> Calendar()
        {
            try
            {
                
            }
            catch (Exception ex)
            {

            }

            return View();
        }

        public async Task<JsonResult> GetFullCalendar()
        {
            var query = await (from c in db.M_EventCategory
                               join r in db.M_EventRound on c.ID equals r.ID_EventCategory
                               where c.IsActive == true && r.IsActive == true
                               select new
                               {
                                   c.Title,
                                   r.Round_Number,
                                   r.DateTime_Start,
                                   r.DateTime_End
                               }).ToListAsync();

            return Json(query.Select(a => new
            {
                title = a.Title + " Round " + a.Round_Number,
                start = a.DateTime_Start.ToString("yyyy-MM-dd"),
                end = a.DateTime_End.ToString("yyyy-MM-dd")
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}