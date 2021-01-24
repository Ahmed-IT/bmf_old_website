using BMF.Shared.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Models.UserManagement
{
    public partial class M_User : BaseEntity
    {
        #region Properties

        public string Email_Address { get; set; }

        public string CPR_Number { get; set; }

        public string Password { get; set; }

        #endregion

        #region Relationships

        public M_UserProfile UserProfile { get; set; }
        
        public List<M_UserLicense> UserLicense { get; set; }

        public List<M_EventRoundSessionUserResults> UserResults { get; set; }

        public List<M_MyRaces> MyRaces { get; set; }

        public List<M_MyFavRaces> MyFavRaces { get; set; }

        #endregion
    }
}
