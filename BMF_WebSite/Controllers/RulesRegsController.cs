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
    public class RulesRegsController : ApiController
    {
        BMFEntities1 db = new BMFEntities1();

        [HttpGet]
        [Route("api/GetRulesRegs")]
        public async Task<IHttpActionResult> GetRulesRegs()
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
                                       && b.DeletedOnUtc == null).Select(a => new
                                       {
                                           ID = a.ID,
                                           ID_Category = a.ID_Category,
                                           Title = a.Title,
                                           RulesRegs = a.M_BRSubCategoryRulesRegs.Where(b => b.IsActive == true
                                                       && b.DeletedOnUtc == null).Select(b => new
                                                       {
                                                           ID = b.ID,
                                                           ID_SubCategory = b.ID_SubCategory,
                                                           PDF_Url = b.PDF_Url,
                                                           Title = b.Title
                                                       }).ToList()
                                       }).ToList()
                                   }).Where(x => x.SubCategory.Where(v => v.RulesRegs.Count() > 0).Count() > 0).ToListAsync();

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
