using BMF.Shared.Entities.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Entities.UserManagement
{
    public partial class M_MyRaces : BaseEntity
    {
        #region Foreign Keys

        public long ID_User { get; set; }

        public long ID_EventRoundSession { get; set; }

        #endregion

        #region Relationships

        public M_User User { get; set; }

        public M_EventRoundSession EventRoundSession { get; set; }

        #endregion
    }
}
