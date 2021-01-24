using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Events
{
    public partial class M_EventCategory : BaseEntity
    {
        #region Foreign Keys

        [JsonProperty(PropertyName = nameof(ID_EventSeason))]
        public long ID_EventSeason { get; set; }

        #endregion

        #region Properties

        [JsonProperty(PropertyName = nameof(Title))]
        public string Title { get; set; }

        [JsonProperty(PropertyName = nameof(Image_URL))]
        public string Image_URL { get; set; }

        #endregion

        #region Relationships

        [InverseProperty(nameof(M_EventSeason.EventCategories))]
        public M_EventSeason EventSeason { get; set; }

        [InverseProperty(nameof(M_EventCategoryRules.EventCategory))]
        public ICollection<M_EventCategoryRules> EventCategoryRules { get; set; }

        [InverseProperty(nameof(M_EventRound.EventCategory))]
        public ICollection<M_EventRound> EventRounds { get; set; }

        #endregion
    }
}
