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
    public class NewsController : ApiController
    {
        BMFEntities1 db = new BMFEntities1();

        [HttpGet]
        [Route("api/GetNews")]
        public async Task<IHttpActionResult> GetNews()
        {
            try
            {
                var query = await (from c in db.M_News
                                   where c.IsActive == true
                                   select new
                                   {
                                       c.ID,
                                       c.Image_URL,
                                       c.Title,
                                       c.Content,
                                       c.NewsTimestampUtc
                                       }).OrderBy(a => a.NewsTimestampUtc).Take(10).ToListAsync();

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
