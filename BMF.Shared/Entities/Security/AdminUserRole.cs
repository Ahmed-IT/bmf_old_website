using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Security
{
    public partial class AdminUserRole : BaseEntity
    {
        #region Properties

        #endregion

        #region Foreign Keys
        
        [ForeignKey(nameof(User))]
        [Display(Name = "User")]
        [Required]
        [JsonProperty(PropertyName = nameof(ID_User))]
        public long ID_User { get; set; }

        [ForeignKey(nameof(Role))]
        [Display(Name = "Role")]
        [Required]
        [JsonProperty(PropertyName = nameof(ID_Role))]
        public long ID_Role { get; set; }

        #endregion

        #region Relationships

        public AdminUser User { get; set; }

        public AdminRole Role { get; set; }

        #endregion
    }
}
