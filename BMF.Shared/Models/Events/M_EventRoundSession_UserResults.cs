using BMF.Shared.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Models.Events
{
    public partial class M_EventRoundSession_UserResults : BaseEntity
    {
        #region Foreign Keys

        public int ID_EventRoundSession { get; set; }

        public int ID_User { get; set; }

        #endregion

        #region Properties

        public int Points { get; set; }

        #endregion

        #region Relationships

        public M_EventRoundSession EventRoundSession { get; set; }
        
        public M_User User { get; set; }

        #endregion
    }
}
