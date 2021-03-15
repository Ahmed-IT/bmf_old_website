using System;
using System.Collections.Generic;
using System.Text;

namespace BMF_NewApp.Models
{
    public class ClsRound
    {
        public int ID { get; set; }
        public string Round_Number { get; set; }
        public DateTime DateTime_Start { get; set; }
        public DateTime DateTime_End { get; set; }

        public ClsEventCategory EventCategory { get; set; }

        public string EventFullName
        {
            get
            {
                return EventCategory.Title + " Round " + Round_Number.ToString();
            }
        }

        public ClsRound()
        {
            EventCategory = new ClsEventCategory();
        }
    }
}
