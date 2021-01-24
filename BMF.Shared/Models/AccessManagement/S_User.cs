using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Models.AccessManagement
{
    public partial class S_User : BaseEntity
    {
        #region Fields
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
        #endregion

        #region Properties
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        #endregion

        #region Relationships
        public virtual ICollection<S_RoleUser> AssignedRoles { get; set; }
        #endregion
    }
}
