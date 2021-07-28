using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BMF_WebSite.ViewModels
{
    public class MainPageVM
    {
        BMFEntities1 db = new BMFEntities1();

        public List<M_Club> Clubs { get; set; }
        public List<M_EventRound> ComingEvents { get; set; }
        public M_EventRound Countdown { get; set; }
        public List<M_News> News { get; set; }
        public List<M_GalleryImages> Images { get; set; }

        public void GetData()
        {
            try
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
                DateTime dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, tz);

                Countdown = db.M_EventRound.Where(a =>
                DbFunctions.TruncateTime(a.DateTime_Start)
                >= DbFunctions.TruncateTime(dateTime) &&
                a.IsActive == true && a.DeletedOnUtc == null).OrderBy(a => a.DateTime_Start).FirstOrDefault();

                ComingEvents = db.M_EventRound.Where(a =>
                DbFunctions.TruncateTime(a.DateTime_Start)
                 > DbFunctions.TruncateTime(dateTime) &&
                a.IsActive == true && a.DeletedOnUtc == null).OrderBy(a => a.DateTime_Start).Take(5).ToList();

                Clubs = db.M_Club.Where(a =>
                a.IsActive == true && a.DeletedOnUtc == null).ToList();

                News = db.M_News.Where(a =>
                DbFunctions.TruncateTime(a.NewsTimestampUtc)
                >= DbFunctions.TruncateTime(dateTime) &&
                a.IsActive == true && a.DeletedOnUtc == null).OrderBy(a => a.NewsTimestampUtc).Take(5).ToList();

                Images = db.M_GalleryImages.Where(a =>
                a.IsActive == true && a.DeletedOnUtc == null
                                   && a.M_GalleryCategory.IsActive == true 
                                   && a.M_GalleryCategory.DeletedOnUtc == null).OrderBy(x => Guid.NewGuid()).Take(6).ToList();

            }
            catch (Exception ex)
            {

            }
        }
    }
}