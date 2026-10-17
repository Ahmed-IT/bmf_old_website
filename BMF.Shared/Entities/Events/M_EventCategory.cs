using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Events
{
    public partial class M_EventCategory : BaseEntity
    {
        #region Foreign Keys

        #endregion

        #region Properties

        [Display(Name = "Title", Prompt = "Title")]
        [Required, MaxLength(60)]
        [JsonProperty(PropertyName = nameof(Title))]
        public string Title { get; set; }

        [Display(Name = "Image URL", Prompt = "Image URL")]
        [Required, MaxLength(1024)]
        [JsonProperty(PropertyName = nameof(Image_URL))]
        public string Image_URL { get; set; }

        #endregion

        #region Relationships

        [Display(Name = "Heats", Prompt = "Heats")]
        [InverseProperty(nameof(M_EventCategoryHeat.EventCategory))]
        public ICollection<M_EventCategoryHeat> EventCategoryHeats { get; set; }

        [Display(Name = "Rounds", Prompt = "Rounds")]
        [InverseProperty(nameof(M_EventRound.EventCategory))]
        public ICollection<M_EventRound> EventRounds { get; set; }

        #endregion
    }
}
