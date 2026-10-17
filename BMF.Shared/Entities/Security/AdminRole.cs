using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Security
{
    public partial class AdminRole : BaseEntity
    {
        #region Properties

        [Display(Name = "Name")]
        [StringLength(500), MaxLength(500), MinLength(1), Required]
        [JsonProperty(PropertyName = nameof(Name))]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(1024), MaxLength(1024)]
        [JsonProperty(PropertyName = nameof(Description))]
        public string Description { get; set; }

        [Display(Name = "Is Related to Identity & Access Management?")]
        [Required]
        [JsonProperty(PropertyName = nameof(IsIAMRelated))]
        public bool IsIAMRelated { get; set; }

        #endregion

        #region Foreign Keys

        #endregion

        #region Relationships

        [InverseProperty(nameof(AdminUserRole.Role))]
        public ICollection<AdminUserRole> AssignedUsers { get; set; }

        [InverseProperty(nameof(AdminRoleOperation.Role))]
        public ICollection<AdminRoleOperation> AssignedOperations { get; set; }

        #endregion
    }
}
