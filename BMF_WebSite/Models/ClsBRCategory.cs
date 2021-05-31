using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMF_WebSite.Models
{
    public class ClsBRCategory
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public List<ClsBRSubCategory> SubCategory { get; set; }
    }
}