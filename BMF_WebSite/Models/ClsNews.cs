using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMF_WebSite.Models
{
    public class ClsNews
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image_URL { get; set; }
        public DateTime NewsTimestampUtc { get; set; }
        public bool IsActive { get; set; }
    }
}