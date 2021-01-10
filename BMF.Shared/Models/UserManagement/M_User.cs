using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Models.UserManagement
{
    public partial class M_User : BaseEntity
    {
        #region Properties

        public string EmailAddress { get; set; }

        public string CPRNumber { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        #endregion

        #region Relationships

        public M_UserProfile UserProfile { get; set; }

        #endregion
    }
}
