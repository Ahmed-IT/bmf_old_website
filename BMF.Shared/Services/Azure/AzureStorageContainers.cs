using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Services.Azure
{
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
