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
    public class GalleryController : Controller
    {
        BMFEntities1 db = new BMFEntities1();

        // GET: Gallery
        public async Task<ActionResult> Index()
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

            if (query.Count > 0)
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
            if (ID != 0)
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
    }
}