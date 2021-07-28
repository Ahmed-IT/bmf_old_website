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
    public class NewsHubController : Controller
    {
        BMFEntities1 db = new BMFEntities1();

        // GET: NewsHub
        public async Task<ActionResult> Index()
        {
            var query = await (from n in db.M_News
                               where n.IsActive == true && n.DeletedOnUtc == null
                               select new ClsNews
                               {
                                   IsActive = n.IsActive,
                                   Content = n.Content,
                                   ID = n.ID,
                                   Image_URL = n.Image_URL,
                                   NewsTimestampUtc = n.NewsTimestampUtc,
                                   Title = n.Title
                               }).ToListAsync();

            return View(query);
        }

        public async Task<ActionResult> NewsDetail(int ID)
        {
            var query = await (from n in db.M_News
                               where n.IsActive == true && n.DeletedOnUtc == null
                               && n.ID == ID
                               select new ClsNews
                               {
                                   IsActive = n.IsActive,
                                   Content = n.Content,
                                   ID = n.ID,
                                   Image_URL = n.Image_URL,
                                   NewsTimestampUtc = n.NewsTimestampUtc,
                                   Title = n.Title
                               }).FirstOrDefaultAsync();

            return View(query);
        }

    }
}