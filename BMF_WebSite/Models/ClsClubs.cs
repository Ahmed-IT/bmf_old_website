using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMF_WebSite.Models
{
    public class ClsClubs
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Website_Link { get; set; }
        public string Image_URL { get; set; }
    }
}