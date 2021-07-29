using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Events
{
    public partial class M_EventSeason : BaseEntity
    {
        #region Properties

        [Display(Name = "Title", Prompt = "Title")]
        [Required, MaxLength(60)]
        [JsonProperty(PropertyName = nameof(Title))]
        public string Title { get; set; }

        #endregion

        #region Relationships

        [InverseProperty(nameof(M_EventRound.EventSeason))]
        public ICollection<M_EventRound> EventRounds { get; set; }

        #endregion
    }
}
