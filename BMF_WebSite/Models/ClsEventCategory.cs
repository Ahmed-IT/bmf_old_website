using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMF_WebSite.Models
{
    public class ClsEventCategory
    {
        public long ID { get; set; }
        public long ID_EventSeason { get; set; }
        public string Title { get; set; }
        public string Image_URL { get; set; }
        public bool IsActive { get; set; }
        public ClsEventRound NextRound { get; set; }
        public List<ClsEventRound> EventRound { get; set; }
    }
}