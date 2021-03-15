using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.UserManagement
{
    public partial class D_LicenseType : BaseEntity
    {
        #region Properties

        [Display(Name = "Type", Prompt = "Type")]
        [JsonProperty(PropertyName = nameof(Type))]
        [MaxLength(60)]
        [Required]
        public string Type { get; set; }

        [Display(Name = "Title", Prompt = "Title")]
        [JsonProperty(PropertyName = nameof(Title))]
        [MaxLength(120)]
        [Required]
        public string Title { get; set; }

        [JsonProperty(PropertyName = nameof(Image_Front_URL))]
        [MaxLength(1024)]
        [Required]
        public string Image_Front_URL { get; set; }

        [JsonProperty(PropertyName = nameof(Image_Back_URL))]
        [MaxLength(1024)]
        [Required]
        public string Image_Back_URL { get; set; }

        #endregion

        #region Relationships

        //[Display(Name = "User Licenses", Prompt = "User Licenses")]
        //[InverseProperty(nameof(M_UserLicense.LicenseType))]
        //public ICollection<M_UserLicense> UserLicense { get; set; }

        #endregion
    }
}
