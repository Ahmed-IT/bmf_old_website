using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Models.UserManagement
{
    public partial class M_UserLicenseRenewal : BaseEntity
    {
        #region Foreign Keys

        public long ID_UserLicense { get; set; }

        #endregion

        #region Properties

        public string Issue_Type { get; set; }

        public DateTime Issue_Date { get; set; }

        #endregion

        #region Relationships

        public M_UserLicense UserLicense { get; set; }

        public ICollection<M_UserLicenseRenewalMedicForms> MedicForms { get; set; }

        #endregion
    }
}
