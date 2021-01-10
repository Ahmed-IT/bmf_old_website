using BMF.Shared.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Models.UserManagement
{
    public partial class M_MyRaces : BaseEntity
    {
        #region Foreign Keys

        public int ID_User { get; set; }

        public int ID_EventRoundSession { get; set; }

        #endregion

        #region Relationships

        public M_User User { get; set; }

        public M_EventRoundSession EventRoundSession { get; set; }

        #endregion
    }
}
