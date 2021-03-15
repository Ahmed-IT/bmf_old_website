using BMF.Shared.Entities.UserManagement;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Events
{
    public partial class M_EventRoundSession : BaseEntity
    {
        #region Foreign Keys

        [ForeignKey(nameof(EventRound))]
        [Display(Name = "Event Round", Prompt = "Event Round")]
        [Required]
        [JsonProperty(PropertyName = nameof(ID_EventRound))]        
        public long ID_EventRound { get; set; }

        #endregion

        #region Properties

        [Display(Name = "Title", Prompt = "Title")]
        [Required, MaxLength(60)]
        [JsonProperty(PropertyName = nameof(Title))]
        public string Title { get; set; }

        [Display(Name = "PDF URL", Prompt = "PDF URL")]
        [Required, MaxLength(1024)]
        [JsonProperty(PropertyName = nameof(Results_PDF_URL))]
        public string Results_PDF_URL { get; set; }

        #endregion

        #region Relationships

        [InverseProperty(nameof(M_EventRound.EventRoundSessions))]
        public M_EventRound EventRound { get; set; }

        [InverseProperty(nameof(M_EventRoundSessionUserResults.EventRoundSession))]
        public ICollection<M_EventRoundSessionUserResults> UserResults { get; set; }

        [InverseProperty(nameof(M_MyRaces.EventRoundSession))]
        public ICollection<M_MyRaces> User_MyRaces { get; set; }

        [InverseProperty(nameof(M_MyFavRaces.EventRoundSession))]
        public ICollection<M_MyFavRaces> User_MyFavRaces { get; set; }

        #endregion
    }
}
