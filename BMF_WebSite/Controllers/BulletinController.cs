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
    public class BulletinController : Controller
    {
        BMFEntities1 db = new BMFEntities1();
        // GET: Bulletin
        public async Task<ActionResult> Index()
        {
            var query = await (from c in db.M_BRCategory
                               where c.IsActive == true && c.DeletedOnUtc == null
                               select new ClsBRCategory
                               {
                                   ID = c.ID,
                                   Title = c.Title,
                                   SubCategory = c.M_BRSubCategory.Where(b => b.IsActive == true
                                   && b.DeletedOnUtc == null
                                   && c.M_BRSubCategory.Select(sb => sb.M_BRSubCategoryBulletin).Count() != 0).Select(a => new ClsBRSubCategory
                                   {
                                       ID = a.ID,
                                       ID_Category = a.ID_Category,
                                       Title = a.Title,
                                       Bulletins = a.M_BRSubCategoryBulletin.Where(b => b.IsActive == true
                                   && b.DeletedOnUtc == null).Select(b => new ClsBulletins
                                   {
                                       ID = b.ID,
                                       ID_SubCategory = b.ID_SubCategory,
                                       PDF_Url = b.PDF_Url,
                                       Title = b.Title
                                   }).ToList()
                                   }).ToList()
                               }).Where(x => x.SubCategory.Where(v => v.Bulletins.Count() > 0).Count() > 0).ToListAsync();

            return View(query);
        }
    }
}