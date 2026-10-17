using BMF.Shared.Entities.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BMF.Shared.Entities.UserManagement
{
    public partial class M_UserRoundSignIn : BaseEntity
    {
        #region Foreign Keys

        [ForeignKey(nameof(UserProfile))]
        [Display(Name = "User Profile", Prompt = "User Profile")]
        [JsonProperty(PropertyName = nameof(ID_UserProfile))]
        [Required]
        public long ID_UserProfile { get; set; }

        [ForeignKey(nameof(UserLicense))]
        [Display(Name = "User License", Prompt = "User License")]
        [JsonProperty(PropertyName = nameof(ID_UserLicense))]
        [Required]
        public long ID_UserLicense{ get; set; }

        [ForeignKey(nameof(EventRound))]
        [Display(Name = "Event Round", Prompt = "Event Round")]
        [JsonProperty(PropertyName = nameof(ID_EventRound))]
        [Required]
        public long ID_EventRound { get; set; }

        #endregion

        #region Properties

        [Display(Name = "Sign In", Prompt = "Sign In")]
        [Required]
        [JsonProperty(PropertyName = nameof(SignIn))]
        public DateTime SignIn { get; set; }

        #endregion

        #region Relationships

        [Display(Name = "User Profile", Prompt = "User Profile")]
        [InverseProperty(nameof(M_UserProfile.UserRoundSignIns))]
        public M_UserProfile UserProfile { get; set; }

        [Display(Name = "User License", Prompt = "User License")]
        [InverseProperty(nameof(M_UserLicense.UserRoundSignIns))]
        public M_UserLicense UserLicense { get; set; }

        [Display(Name = "Event Round", Prompt = "Event Round")]
        [InverseProperty(nameof(M_EventRound.UserRoundSignIns))]
        public M_EventRound EventRound { get; set; }

        #endregion
    }
}
