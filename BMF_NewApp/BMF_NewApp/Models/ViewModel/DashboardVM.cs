using System;
using System.Collections.Generic;
using System.Text;

namespace BMF_NewApp.Models.ViewModel
{
    public class DashboardVM
    {
        public ClsProfile Profile { get; set; }
        public ClsRound Countdown { get; set; }
        public List<ClsRound> UpComingEvents { get; set; }
        public List<ClsNews> News { get; set; }

        public DashboardVM()
        {
            Profile = new ClsProfile();
            Countdown = new ClsRound();
            UpComingEvents = new List<ClsRound>();
            News = new List<ClsNews>();
        }
    }
}
