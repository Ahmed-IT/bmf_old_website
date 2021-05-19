using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMF_WebSite.Models
{
    public class ClsGalleryImage
    {
        public long ID { get; set; }
        public long ID_GalleryCategory { get; set; }
        public string Image_URL { get; set; }
    }
}