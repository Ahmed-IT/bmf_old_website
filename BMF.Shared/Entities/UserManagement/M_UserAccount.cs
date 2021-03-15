using BMF.Shared.Entities.Events;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.UserManagement
{
    public partial class M_UserAccount : BaseEntity
    {
        #region Properties

        [Display(Name = "Email Address", Prompt = "Email Address")]
        [JsonProperty(PropertyName = nameof(Email_Address))]
        [MaxLength(120)]
        [Required]
        public string Email_Address { get; set; }

        [Display(Name = "Password", Prompt = "Password")]
        [JsonProperty(PropertyName = nameof(Password))]
        [MaxLength(255)]
        [Required]
        public string Password { get; set; }

        #endregion

        #region Relationships

        [Display(Name = "User Profile", Prompt = "User Profile")]
        [InverseProperty(nameof(M_UserProfile.UserAccount))]
        public M_UserProfile UserProfile { get; set; }

        public ICollection<M_UserLicense> UserLicense { get; set; }

        public ICollection<M_EventRoundSessionUserResults> UserResults { get; set; }

        public ICollection<M_MyRaces> MyRaces { get; set; }

        public ICollection<M_MyFavRaces> MyFavRaces { get; set; }

        #endregion
    }
}
