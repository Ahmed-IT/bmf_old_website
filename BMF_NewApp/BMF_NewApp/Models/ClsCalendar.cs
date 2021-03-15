using System;
using System.Collections.Generic;
using System.Text;

namespace BMF_NewApp.Models
{
    public class ClsCalendar
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Image_URL { get; set; }
        public List<ClsRound> Rounds { get; set; }
        public ClsRound NextRound { get; set; }

        public ClsCalendar()
        {
            Rounds = new List<ClsRound>();
            NextRound = new ClsRound();
        }
    }
}
