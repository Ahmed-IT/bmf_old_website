using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Models.Events
{
    public partial class M_EventRound : BaseEntity
    {
        #region Foreign Keys

        public string ID_EventCategory { get; set; }

        #endregion

        #region Properties

        public string RoundNumber { get; set; }

        public DateTime DateTime_Start { get; set; }

        public DateTime DateTime_End { get; set; }

        public bool AddTo_Calendar { get; set; }

        public bool AddTo_Countdown { get; set; }

        #endregion

        #region Relationships

        public M_EventCategory EventCategory { get; set; }

        #endregion
    }
}
