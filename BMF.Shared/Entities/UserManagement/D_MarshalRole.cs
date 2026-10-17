using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.UserManagement
{
    public partial class D_MarshalRole : BaseEntity
    {
        #region Properties

        [Display(Name = "Name", Prompt = "Name")]
        [JsonProperty(PropertyName = nameof(Name))]
        [MaxLength(60)]
        [Required]
        public string Name { get; set; }

        #endregion

        #region Relationships

        [Display(Name = "User Licenses", Prompt = "User Licenses")]
        [InverseProperty(nameof(M_UserLicense.MarshalRole))]
        public ICollection<M_UserLicense> UserLicenses { get; set; }

        #endregion
    }
}
