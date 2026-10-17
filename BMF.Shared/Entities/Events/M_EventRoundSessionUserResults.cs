using BMF.Shared.Entities.UserManagement;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Events
{
    public partial class M_EventRoundSessionUserResults : BaseEntity
    {
        #region Foreign Keys

        [ForeignKey(nameof(EventRoundSession))]
        [Display(Name = "Round Session", Prompt = "Round Session")]
        [Required]
        [JsonProperty(PropertyName = nameof(ID_EventRoundSession))]
        public long ID_EventRoundSession { get; set; }

        [ForeignKey(nameof(UserProfile))]
        [Display(Name = "User", Prompt = "User")]
        [Required]
        [JsonProperty(PropertyName = nameof(ID_UserProfile))]
        public long ID_UserProfile { get; set; }

        #endregion

        #region Properties

        [Display(Name = "Points", Prompt = "Points")]
        [Required]
        [JsonProperty(PropertyName = nameof(Points))]
        public int Points { get; set; }

        [Display(Name = "Best Time", Prompt = "Best Time")]
        [JsonProperty(PropertyName = nameof(BestTime))]
        public string BestTime { get; set; }

        #endregion

        #region Relationships

        [Display(Name = "Round Session", Prompt = "Round Session")]
        [InverseProperty(nameof(M_EventRoundSession.UserResults))]
        public M_EventRoundSession EventRoundSession { get; set; }

        [Display(Name = "User", Prompt = "User")]
        [InverseProperty(nameof(M_UserProfile.UserResults))]
        public M_UserProfile UserProfile { get; set; }

        #endregion
    }
}
