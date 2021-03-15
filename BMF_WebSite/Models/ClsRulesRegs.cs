using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMF_WebSite.Models
{
    public class ClsRulesRegs
    {
        public long ID { get; set; }
        public long ID_EventCategory { get; set; }
        public string Title { get; set; }
        public string PDF_Url { get; set; }
        public bool IsActive { get; set; }
    }
}