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
    }
}
