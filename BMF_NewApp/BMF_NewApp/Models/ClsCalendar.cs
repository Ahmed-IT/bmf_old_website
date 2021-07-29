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

        public string NextRoundTitle
        {
            get
            {
                if(NextRound == null)
                {
                    return "NO COMING\nROUND";
                }
                else
                {
                    return NextRound.Round_Number;
                }
                
            }
        }

        public ClsCalendar()
        {
            Rounds = new List<ClsRound>();
            NextRound = new ClsRound();
        }
    }
}
