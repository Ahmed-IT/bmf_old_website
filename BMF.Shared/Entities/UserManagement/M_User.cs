using BMF.Shared.Entities.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Entities.UserManagement
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
        
        public ICollection<M_UserLicense> UserLicense { get; set; }

        public ICollection<M_EventRoundSessionUserResults> UserResults { get; set; }

        public ICollection<M_MyRaces> MyRaces { get; set; }

        public ICollection<M_MyFavRaces> MyFavRaces { get; set; }

        #endregion
    }
}
