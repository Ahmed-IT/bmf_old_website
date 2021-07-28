using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMF_WebSite.ViewModels
{
    public class ResultsVM
    {
        BMFEntities1 db = new BMFEntities1();

        public List<M_EventRoundSessionUserResults> Results { get; set; }
        public List<M_EventSeason> Season { get; set; }

        public void GetData()
        {
            try
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
                DateTime dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, tz);

                Results = db.M_EventRoundSessionUserResults.Where(a =>
                a.IsActive == true && a.DeletedOnUtc == null).ToList();

                Season = db.M_EventSeason.Where(a =>
                a.IsActive == true && a.DeletedOnUtc == null).ToList();

            }
            catch (Exception ex)
            {

            }
        }
    }
}