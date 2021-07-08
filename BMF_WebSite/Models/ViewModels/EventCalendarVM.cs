using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BMF_WebSite.Models.ViewModels
{
    public class EventCalendarVM
    {
        public class Calendar
        {
            public List<ClsEventCategory> EventsCategory { get; set; }
        }

        BMFEntities1 db = new BMFEntities1();

        public async Task<Calendar> GetCalendarPage()
        {
            Calendar cal = new Calendar();

            try
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
                DateTime dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, tz);

                cal.EventsCategory = await (from c in db.M_EventCategory
                                    where c.IsActive == true
                                    && c.DeletedBy == null
                                    && c.M_EventSeason.IsActive == true
                                    && c.M_EventRound.Where(b => b.IsActive == true && b.DeletedOnUtc == null).ToList().Count > 0
                                    select new ClsEventCategory
                                    {
                                        ID = c.ID,
                                        Image_URL = c.Image_URL,
                                        Title = c.Title,
                                        IsActive = c.IsActive,
                                        ID_EventSeason = c.ID_EventSeason,
                                        NextRound = c.M_EventRound.Where(a => a.DateTime_Start >= dateTime && a.IsActive == true && a.DeletedOnUtc == null).OrderBy(a => a.DateTime_Start).Select(a => new ClsEventRound
                                        {
                                            DateTime_End = a.DateTime_End,
                                            DateTime_Start = a.DateTime_Start,
                                            ID = a.ID,
                                            ID_EventCategory = a.ID_EventCategory,
                                            Round_Number = a.Round_Number
                                        }).FirstOrDefault()
                                    }).ToListAsync();

            }
            catch (Exception)
            {

            }

            return cal;
        }
    }
}