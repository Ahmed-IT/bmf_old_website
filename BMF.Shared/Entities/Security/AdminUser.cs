using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Security
{
    public partial class AdminUser : BaseEntity
    {
        #region Properties

        [Display(Name = "Email")]
        [StringLength(255), MaxLength(255), MinLength(1), Required]
        [JsonProperty(PropertyName = nameof(Email))]
        public string Email { get; set; }

        [Display(Name = "Username")]
        [StringLength(255), MaxLength(255), MinLength(1), Required]
        [JsonProperty(PropertyName = nameof(Username))]
        public string Username { get; set; }

        [Display(Name = "First Name")]
        [StringLength(255), MaxLength(255), MinLength(1), Required]
        [JsonProperty(PropertyName = nameof(FirstName))]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(255), MaxLength(255), MinLength(1), Required]
        [JsonProperty(PropertyName = nameof(LastName))]
        public string LastName { get; set; }

        [Display(Name = "Password")]
        [StringLength(255), MaxLength(255), MinLength(1), Required]
        [JsonProperty(PropertyName = nameof(Password))]
        public string Password { get; set; }

        #endregion

        #region Foreign Keys

        #endregion

        #region Relationships

        [InverseProperty(nameof(AdminUserRole.User))]
        public ICollection<AdminUserRole> AssignedRoles { get; set; }

        #endregion
    }
}
