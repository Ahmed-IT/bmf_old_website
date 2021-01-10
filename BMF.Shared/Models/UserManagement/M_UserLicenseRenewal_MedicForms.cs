using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Models.UserManagement
{
    public partial class M_UserLicenseRenewal_MedicForms : BaseEntity
    {
        #region Foreign Keys

        public int ID_UserLicenseRenewal { get; set; }

        #endregion

        #region Properties

        public DateTime Date { get; set; }

        public string PDF_File { get; set; }

        #endregion

        #region Relationships

        public M_UserLicenseRenewal UserLicenseRenewal { get; set; }

        #endregion
    }
}
