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

        [ForeignKey(nameof(UserAccount))]
        [Display(Name = "User", Prompt = "User")]
        [Required]
        [JsonProperty(PropertyName = nameof(ID_UserAccount))]
        public long ID_UserAccount { get; set; }

        #endregion

        #region Properties

        [Display(Name = "Points", Prompt = "Points")]
        [Required]
        [JsonProperty(PropertyName = nameof(Points))]
        public int Points { get; set; }

        #endregion

        #region Relationships

        [InverseProperty(nameof(M_EventRoundSession.UserResults))]
        public M_EventRoundSession EventRoundSession { get; set; }

        [InverseProperty(nameof(M_UserAccount.UserResults))]
        public M_UserAccount UserAccount { get; set; }

        #endregion
    }
}
