using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Models.UserManagement
{
    public partial class M_UserLicense : BaseEntity
    {
        #region Foreign Keys

        public long ID_User { get; set; }
        
        public long ID_LicenseType { get; set; }

        #endregion

        #region Properties

        public string License_Number { get; set; }

        public DateTime Expire_Date { get; set; }

        #endregion

        #region Relationships

        public M_User User { get; set; }

        public D_LicenseType LicenseType { get; set; }

        public ICollection<M_UserLicenseRenewal> UserLicenseRenewal { get; set; }

        #endregion
    }
}
