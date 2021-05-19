using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMF_WebSite.Models
{
    public class ClsGalleryCategory
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Image_URL { get; set; }
        public List<ClsGalleryImage> GalleryImages { get; set; }
    }
}