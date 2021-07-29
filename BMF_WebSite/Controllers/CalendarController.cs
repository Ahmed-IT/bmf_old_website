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
    public class CalendarController : ApiController
    {
        BMFEntities1 db = new BMFEntities1();

        [HttpGet]
        [Route("api/GetCalendar")]
        public async Task<IHttpActionResult> GetCalendar()
        {
            try
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
                DateTime dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, tz);
                
                var query = await (from c in db.M_EventCategory
                                   where c.IsActive == true && c.DeletedOnUtc == null
                                   select new
                                   {
                                      c.ID,
                                      c.Image_URL,
                                      c.Title,
                                      NextRound = c.M_EventRound.Where(m => m.IsActive == true &&
                                      DbFunctions.TruncateTime(dateTime)
                                 <= DbFunctions.TruncateTime(m.DateTime_Start)).OrderBy(m => m.DateTime_Start).Select(a => new
                                       {
                                           a.ID,
                                           a.DateTime_Start,
                                           a.DateTime_End,
                                           a.Round_Number,
                                           a.IsActive
                                       }).FirstOrDefault(),
                                      Rounds = c.M_EventRound.Where(m => m.IsActive == true).ToList().Select(a => new
                                      {
                                          a.ID,
                                          a.DateTime_Start,
                                          a.DateTime_End,
                                          a.Round_Number,
                                          a.IsActive
                                      }).OrderBy(m => m.DateTime_Start).ToList()
                                   }).Where(a => a.Rounds.Count > 0).ToListAsync();

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
