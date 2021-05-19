using BMF_WebSite.Models;
using BMF_WebSite.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace BMF_WebSite.Controllers
{
    public class HomeController : Controller
    {
        BMFEntities1 db = new BMFEntities1();

        // GET: Home
        public async Task<ActionResult> Index()
        {
            try
            {
                MainPageVM vm = new MainPageVM();
                var getdash = await vm.GetMainPage();

                return View(getdash);
            }
            catch (Exception ex)
            {

            }

            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult BoardMembers()
        {
            return View();
        }

        public ActionResult MissionStatement()
        {
            return View();
        }

        public ActionResult RulesRegulations()
        {
            try
            {
                var query = (from c in db.M_EventCategory
                             where c.IsActive == true
                             && c.DeletedBy == null &&
                             c.M_EventCategoryRules.Count != 0
                             select new ClsEventCategory
                             {
                                 IsActive = c.IsActive,
                                 ID = c.ID,
                                 ID_EventSeason = c.ID_EventSeason,
                                 Image_URL = c.Image_URL,
                                 Title = c.Title,
                                 RulesRegs = c.M_EventCategoryRules.Where(a => a.IsActive == true && a.DeletedBy == null).Select(a => new ClsRulesRegs
                                 {
                                     IsActive = a.IsActive,
                                     ID = a.ID,
                                     ID_EventCategory = a.ID_EventCategory,
                                     PDF_Url = a.PDF_Url,
                                     Title = a.Title
                                 }).ToList()
                             }).ToList();

                return View(query);
            }
            catch (Exception ex)
            {

            }

            return View();
        }

        public ActionResult EventRulesRegs(int ID)
        {
            try
            {
                var query = (from c in db.M_EventCategory
                             where c.ID == ID && c.IsActive == true
                             && c.DeletedBy == null &&
                             c.M_EventCategoryRules.Count != 0
                             select new ClsEventCategory
                             {
                                 IsActive = c.IsActive,
                                 ID = c.ID,
                                 ID_EventSeason = c.ID_EventSeason,
                                 Image_URL = c.Image_URL,
                                 Title = c.Title,
                                 RulesRegs = c.M_EventCategoryRules.Where(a => a.IsActive == true && a.DeletedBy == null).Select(a => new ClsRulesRegs
                                 {
                                     IsActive = a.IsActive,
                                     ID = a.ID,
                                     ID_EventCategory = a.ID_EventCategory,
                                     PDF_Url = a.PDF_Url,
                                     Title = a.Title
                                 }).ToList()
                             }).FirstOrDefault();

                return View(query);
            }
            catch (Exception ex)
            {

            }

            return View();
        }

        public ActionResult EventPermit()
        {
            return View();
        }

        public ActionResult CompetitionLicense()
        {
            return View();
        }

        public ActionResult CdpApplication()
        {
            return View();
        }

        public async Task<ActionResult> EventCalendar()
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
                                           ID_EventSeason = r.M_EventCategory.ID_EventSeason,
                                           Image_URL = r.M_EventCategory.Image_URL,
                                           Title = r.M_EventCategory.Title
                                       }
                                   }).ToListAsync();

                return View(query);
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("EventCalendar", "Home");
        }

        public ActionResult Results()
        {
            return View();
        }

        public async Task<ActionResult> Gallery()
        {
            var query = await (from g in db.M_GalleryCategory
                               where g.DeletedOnUtc == null &&
                               g.IsActive == true
                               select new ClsGalleryCategory
                               {
                                   ID = g.ID,
                                   Image_URL = g.Image_URL,
                                   Title = g.Title
                               }).ToListAsync();

            if(query.Count > 0)
            {
                return View(query);
            }
            else
            {
                return View();
            }            
        }

        public async Task<ActionResult> GalleryImages(long ID)
        {
            if(ID != 0)
            {
                var query = await (from g in db.M_GalleryCategory
                                   where g.ID == ID
                                   select new ClsGalleryCategory
                                   {
                                       ID = g.ID,
                                       Image_URL = g.Image_URL,
                                       Title = g.Title,
                                       GalleryImages = g.M_GalleryImages.Select(i => new ClsGalleryImage
                                       {
                                           ID = i.ID,
                                           ID_GalleryCategory = i.ID_GalleryCategory,
                                           Image_URL = i.Image_URL
                                       }).ToList()
                                   }).FirstOrDefaultAsync();

                return View(query);
            }
            else
            {
                return RedirectToAction("Gallery");
            }

            
        }
        

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public async Task<ActionResult> Bulletins()
        {
            var query = await(from c in db.M_EventCategory
                         where c.IsActive == true && c.DeletedOnUtc == null
                         && c.M_EventCategoryBulletins.Count > 0
                         select new ClsEventCategory
                         {
                             ID = c.ID,
                             Image_URL = c.Image_URL,
                             Title = c.Title,
                             Bulletins = c.M_EventCategoryBulletins.Where(a => 
                             a.IsActive == true && a.DeletedOnUtc == null).Select(a => new ClsBulletins
                             {
                                 ID = a.ID,
                                 ID_EventCategory = a.ID_EventCategory,
                                 PDF_Url = a.PDF_Url,
                                 Title = a.Title
                             }).ToList()
                         }).ToListAsync();

            return View(query);
        }

        public ActionResult NewsDetail()
        {
            return View();
        }

        public ActionResult ResultsDetail()
        {
            return View();
        }

        public ActionResult Calendar()
        {
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