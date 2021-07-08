using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BMF_WebSite.Controllers
{
    public class BulletinsController : ApiController
    {
        BMFEntities1 db = new BMFEntities1();

        [HttpGet]
        [Route("api/GetBulletins")]
        public async Task<IHttpActionResult> GetBulletins()
        {
            try
            {
                var query = await (from c in db.M_BRCategory
                                   where c.IsActive == true && c.DeletedOnUtc == null
                                   select new
                                   {
                                       ID = c.ID,
                                       Title = c.Title,
                                       SubCategory = c.M_BRSubCategory.Where(b => b.IsActive == true
                                       && b.DeletedOnUtc == null
                                       && c.M_BRSubCategory.Select(sb => sb.M_BRSubCategoryBulletin).Count() != 0).Select(a => new
                                       {
                                           ID = a.ID,
                                           ID_Category = a.ID_Category,
                                           Title = a.Title,
                                           Bulletins = a.M_BRSubCategoryBulletin.Where(b => b.IsActive == true
                                       && b.DeletedOnUtc == null).Select(b => new
                                       {
                                           ID = b.ID,
                                           ID_SubCategory = b.ID_SubCategory,
                                           PDF_Url = b.PDF_Url,
                                           Title = b.Title
                                       }).ToList()
                                       }).ToList()
                                   }).Where(x => x.SubCategory.Where(v => v.Bulletins.Count() > 0).Count() > 0).ToListAsync();

                if (query.Count > 0)
                {
                    return Ok(query);
                }
                else
                {
                    return Content(HttpStatusCode.NoContent, "No Content");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
