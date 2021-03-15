using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMF_WebSite.Models
{
    public class ClsEventRound
    {
        public long ID { get; set; }
        public long ID_EventCategory { get; set; }
        public string Round_Number { get; set; }
        public DateTime DateTime_Start { get; set; }
        public DateTime DateTime_End { get; set; }
        public ClsEventCategory EventCategory { get; set; }
    }
}