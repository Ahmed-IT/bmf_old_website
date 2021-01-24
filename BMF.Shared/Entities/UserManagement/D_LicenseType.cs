using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Entities.UserManagement
{
    public partial class D_LicenseType : BaseEntity
    {
        #region Properties

        public string Type { get; set; }

        public string Title { get; set; }

        public string Image_Front_URL { get; set; }
        
        public string Image_Back_URL { get; set; }

        #endregion

        #region Relationships

        public ICollection<M_UserLicense> UserLicense { get; set; }

        #endregion
    }
}
