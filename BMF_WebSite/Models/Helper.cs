using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BMF_WebSite.Models
{
    public class Helper
    {
        public static List<ClsBloodGroup> GetBloodGroup()
        {
            List<ClsBloodGroup> bloodGeoup = new List<ClsBloodGroup>();
            bloodGeoup.Add(new ClsBloodGroup
            {
                Text = "A+",
                Value = "A+"
            });

            bloodGeoup.Add(new ClsBloodGroup
            {
                Text = "A-",
                Value = "A-"
            });

            bloodGeoup.Add(new ClsBloodGroup
            {
                Text = "B+",
                Value = "B+"
            });

            bloodGeoup.Add(new ClsBloodGroup
            {
                Text = "B-",
                Value = "B-"
            });

            bloodGeoup.Add(new ClsBloodGroup
            {
                Text = "O+",
                Value = "O+"
            });

            bloodGeoup.Add(new ClsBloodGroup
            {
                Text = "O-",
                Value = "O-"
            });

            bloodGeoup.Add(new ClsBloodGroup
            {
                Text = "AB+",
                Value = "AB+"
            });

            bloodGeoup.Add(new ClsBloodGroup
            {
                Text = "AB-",
                Value = "AB-"
            });

            return bloodGeoup;
        }

        public static List<ClsCountry> GetCountries()
        {
            BMFEntities1 db = new BMFEntities1();

            List<ClsCountry> countries = new List<ClsCountry>();

            var GetNationalities = (from c in db.Countries
                                    where c.IsActive == true &&
                                    c.DeletedOnUtc == null
                                    select new ClsCountry
                                    {
                                        ID = c.ID,
                                        Name = c.Name,
                                    }).OrderBy(a => a.Name).ToList();

            countries.AddRange(GetNationalities);

            return countries;
        }

        public static List<ClsLicType> GetLicenseCategory()
        {
            BMFEntities1 db = new BMFEntities1();

            List<ClsLicType> lic = new List<ClsLicType>();

            var LicenseCategory = (from c in db.D_LicenseType
                                      where c.IsActive == true &&
                                      c.DeletedOnUtc == null && c.Title != "Official"
                                      select new ClsLicType
                                      {
                                          ID = c.ID,
                                          Title = c.Title,
                                          Type = c.Type
                                      }).ToList();

            lic.AddRange(LicenseCategory);

            return lic;
        }

        public static List<ClsLicenseType> GetLicenseType()
        {
            List<ClsLicenseType> type = new List<ClsLicenseType>();
            type.Add(new ClsLicenseType
            {
                Text = "New",
                Value = "New"
            });

            type.Add(new ClsLicenseType
            {
                Text = "Renew",
                Value = "Renew"
            });

            return type;
        }

        public static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }

        public static List<ClsRacingMode> GetRacingMode()
        {
            List<ClsRacingMode> list = new List<ClsRacingMode>();

            list.Add(new ClsRacingMode
            {
                Text = "Steering Wheel",
                Value = "Steering Wheel"
            });

            list.Add(new ClsRacingMode
            {
                Text = "Controller",
                Value = "Controller"
            });

            return list;
        }

        public static class AzureStorageContainers
        {
            public static string NewsImage => $"websitecms-news";

            public static string ClubImage => $"websitecms-club";

            public static string GalleryCategoryImage => $"websitecms-gallery";

            public static string GalleryImageImages => $"websitecms-galleryimages";

            public static string EventCategory => $"events-category";

            public static string EventRoundSession => $"events-roundsession";

            public static string EventCategoryRulePDF => $"events-categoryrules-pdf";

            public static string EventCategoryBulletinPDF => $"events-categorybulletins-pdf";

            public static string LicenseType => $"license-type";

            public static string LicenseRenewal => $"license-renewal";

            public static string UserProfile => $"user-profile";

        }
    }
}