using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Entities.UserManagement
{
    public partial class M_UserProfile : BaseEntity
    {
        #region Properties

        public string FullName { get; set; }

        public string Nationality { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Mobile_Number { get; set; }

        public string Full_Address { get; set; }
        
        public string Blood_Group { get; set; }

        public string Place_Of_Birth { get; set; }

        public string Photo_URL { get; set; }

        #endregion

        #region Relationships

        public M_User User { get; set; }

        #endregion
    }
}
