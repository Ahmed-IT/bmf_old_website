using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Threading.Tasks;

namespace BMF_WebSite.Models.ViewModels
{
    public class MainPageVM
    {
        public class Dashboard
        {
            public List<ClsClubs> Clubs { get; set; }
            public List<ClsEventRound> ComingEvents { get; set; }
            public ClsEventRound Countdown { get; set; }
            public List<ClsNews> News { get; set; }
            public List<ClsGalleryImage> Images { get; set; }
        }

        BMFEntities1 db = new BMFEntities1();

        public async Task<Dashboard> GetMainPage()
        {
            Dashboard dash = new Dashboard();

            try
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
                DateTime dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, tz);

                dash.Clubs = await (from c in db.M_Club
                             where c.IsActive == true
                             && c.DeletedBy == null
                             select new ClsClubs
                             {
                                 Description = c.Description,
                                 ID = c.ID,
                                 Image_URL = c.Image_URL_Banner,
                                 Title = c.Title,
                                 Website_Link = c.Website_Link
                             }).ToListAsync();

                dash.ComingEvents = await (from c in db.M_EventRound
                                    where DbFunctions.TruncateTime(c.DateTime_Start)
                                    >= DbFunctions.TruncateTime(dateTime)
                                    && c.IsActive == true && c.DeletedOnUtc == null
                                    select new ClsEventRound
                                    {
                                        ID = c.ID,
                                        DateTime_End= c.DateTime_End,
                                        DateTime_Start = c.DateTime_Start,
                                        ID_EventCategory = c.ID_EventCategory,
                                        Round_Number = c.Round_Number,
                                        EventCategory = new ClsEventCategory
                                        {
                                            IsActive = c.M_EventCategory.IsActive,
                                            ID = c.M_EventCategory.ID,
                                            Image_URL = c.M_EventCategory.Image_URL,
                                            Title = c.M_EventCategory.Title
                                        }
                                    }).OrderBy(a => a.DateTime_Start).Take(5).ToListAsync();

                dash.Countdown = await (from c in db.M_EventRound
                                           where DbFunctions.TruncateTime(c.DateTime_Start)
                                           > DbFunctions.TruncateTime(dateTime)
                                           && c.IsActive == true && c.DeletedOnUtc == null
                                           select new ClsEventRound
                                           {
                                               ID = c.ID,
                                               DateTime_End = c.DateTime_End,
                                               DateTime_Start = c.DateTime_Start,
                                               ID_EventCategory = c.ID_EventCategory,
                                               Round_Number = c.Round_Number,
                                               EventCategory = new ClsEventCategory
                                               {
                                                   IsActive = c.M_EventCategory.IsActive,
                                                   ID = c.M_EventCategory.ID,
                                                   Image_URL = c.M_EventCategory.Image_URL,
                                                   Title = c.M_EventCategory.Title
                                               }
                                           }).OrderBy(a => a.DateTime_Start).FirstOrDefaultAsync();

                dash.News = await (from c in db.M_News
                                           where DbFunctions.TruncateTime(c.NewsTimestampUtc)
                                           >= DbFunctions.TruncateTime(dateTime)
                                           && c.IsActive == true && c.DeletedOnUtc == null
                                           select new ClsNews
                                           {
                                               ID = c.ID,
                                               IsActive = c.IsActive,
                                               Content = c.Content,
                                               Image_URL = c.Image_URL,
                                               NewsTimestampUtc = c.NewsTimestampUtc,
                                               Title = c.Title                                               
                                           }).OrderBy(a => a.NewsTimestampUtc).Take(5).ToListAsync();

                var rand = new Random();

                dash.Images = await (from c in db.M_GalleryImages
                                   where c.IsActive == true && c.DeletedOnUtc == null
                                   && c.M_GalleryCategory.IsActive == true && c.M_GalleryCategory.DeletedOnUtc == null
                                   select new ClsGalleryImage
                                   {
                                       ID = c.ID,
                                       ID_GalleryCategory = c.ID_GalleryCategory,
                                       Image_URL = c.Image_URL
                                   }).OrderBy(x => Guid.NewGuid()).Take(6).ToListAsync();
            }
            catch (Exception)
            {

            }

            return dash;
        }
    }
}