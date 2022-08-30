using BMF_WebSite.Models;
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
    public class DashboardController : ApiController
    {
        BMFEntities1 db = new BMFEntities1();

        [HttpGet]
        [Route("api/GetDashboard/{CPR}")]
        public async Task<IHttpActionResult> GetDashboard(string CPR)
        {
            try
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
                DateTime dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, tz);

                var Dashboard = new
                {
                    Profile = await (from p in db.M_UserProfile
                               where p.CPR == CPR
                               select new
                               {
                                   p.CPR,
                                   p.FullName,
                                   p.ID,
                                   p.Photo_URL,
                                   Account = new
                                   {
                                       p.M_UserAccount.Email_Address,
                                       p.M_UserAccount.ID,
                                   }
                               }).FirstOrDefaultAsync(),
                    Countdown = (from r in db.M_EventRound
                                       where DbFunctions.TruncateTime(dateTime)
                                       < DbFunctions.TruncateTime(r.DateTime_Start) &&
                                       r.AddTo_Calendar == true && r.AddTo_Countdown == true
                                       && r.IsActive == true
                                       select new
                                       {
                                           r.ID,
                                           r.Round_Number,
                                           r.DateTime_Start,
                                           r.DateTime_End,
                                           EventCategory = new
                                           {
                                               r.M_EventCategory.ID,
                                               r.M_EventCategory.Image_URL,
                                               r.M_EventCategory.Title
                                           }
                                       }).OrderBy(a => a.DateTime_Start).FirstOrDefault(),
                    UpComingEvents = await (from r in db.M_EventRound
                                            where DbFunctions.TruncateTime(r.DateTime_Start) > DbFunctions.TruncateTime(dateTime) &&
                                       r.AddTo_Calendar == true && r.AddTo_Countdown == true
                                       && r.IsActive == true
                                            select new
                                            {
                                                r.ID,
                                                r.Round_Number,
                                                r.DateTime_Start,
                                                r.DateTime_End,
                                                EventCategory = new
                                                {
                                                    r.M_EventCategory.ID,
                                                    r.M_EventCategory.Image_URL,
                                                    r.M_EventCategory.Title
                                                }
                                            }).OrderBy(a => a.DateTime_Start).Take(3).ToListAsync(),
                    News = await (from r in db.M_News
                                  where r.IsActive == true && r.DeletedOnUtc == null
                                  select new
                                  {
                                      r.ID,
                                      r.Content,
                                      r.Image_URL,
                                      r.NewsTimestampUtc,
                                      r.Title,
                                  }).OrderByDescending(a => a.NewsTimestampUtc).Take(3).ToListAsync()

                };

                if (Dashboard != null)
                {
                    if (Dashboard.Countdown != null)
                    {
                        return Ok(new
                        {
                            Dashboard.Profile,
                            Countdown = Dashboard.Countdown,
                            Dashboard.UpComingEvents,
                            Dashboard.News
                        });
                    }
                    else
                    {
                        List<object> round = new List<object>();

                        round.Add(new
                        {
                            ID = 1,
                            Round_Number = "",
                            DateTime_Start = dateTime,
                            DateTime_End = dateTime,
                            EventCategory = new M_EventCategory
                            {
                                ID = 1,
                                Image_URL = "",
                                Title = "No Coming Event"
                            }
                        });

                        return Ok(new
                        {
                            Dashboard.Profile,
                            Countdown = new
                            {
                                ID = 1,
                                Round_Number = "",
                                DateTime_Start = dateTime,
                                DateTime_End = dateTime,
                                EventCategory = new
                                {
                                    ID = 1,
                                    Image_URL = "",
                                    Title = "No Coming Event"
                                }
                            },
                            Dashboard.UpComingEvents,
                            Dashboard.News
                        });
                    }
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

        [HttpGet]
        [Route("api/GetDashboard")]
        public async Task<IHttpActionResult> GetDashboard()
        {
            try
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
                DateTime dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, tz);

                var Dashboard = new
                {
                    Countdown = await (from r in db.M_EventRound
                                 where DbFunctions.TruncateTime(dateTime)
                                 <= DbFunctions.TruncateTime(r.DateTime_Start) &&
                                 r.AddTo_Calendar == true && r.AddTo_Countdown == true
                                 && r.IsActive == true
                                 select new
                                 {
                                     r.ID,
                                     r.Round_Number,
                                     r.DateTime_Start,
                                     r.DateTime_End,
                                     EventCategory = new
                                     {
                                         r.M_EventCategory.ID,
                                         r.M_EventCategory.Image_URL,
                                         r.M_EventCategory.Title
                                     }
                                 }).OrderBy(a => a.DateTime_Start).FirstOrDefaultAsync(),
                    UpComingEvents = await (from r in db.M_EventRound
                                      where DbFunctions.TruncateTime(r.DateTime_Start) > DbFunctions.TruncateTime(dateTime) &&
                                 r.AddTo_Calendar == true && r.AddTo_Countdown == true
                                 && r.IsActive == true
                                      select new
                                      {
                                          r.ID,
                                          r.Round_Number,
                                          r.DateTime_Start,
                                          r.DateTime_End,
                                          EventCategory = new
                                          {
                                              r.M_EventCategory.ID,
                                              r.M_EventCategory.Image_URL,
                                              r.M_EventCategory.Title
                                          }
                                      }).OrderBy(a => a.DateTime_Start).Take(3).ToListAsync(),
                    News = await (from r in db.M_News
                                  where r.IsActive == true && r.DeletedOnUtc == null
                                  select new
                                  {
                                      r.ID,
                                      r.Content,
                                      r.Image_URL,
                                      r.NewsTimestampUtc,
                                      r.Title,
                                  }).OrderBy(a => a.NewsTimestampUtc).Take(3).ToListAsync()

                };

                if (Dashboard != null)
                {
                    return Ok(Dashboard);
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
