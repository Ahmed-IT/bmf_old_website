using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Events
{
    public partial class M_EventSeason : BaseEntity
    {
        #region Properties

        [JsonProperty(PropertyName = nameof(Title))]
        public string Title { get; set; }

        #endregion

        #region Relationships

        [InverseProperty(nameof(M_EventCategory.EventSeason))]
        public ICollection<M_EventCategory> EventCategories { get; set; }

        #endregion
    }
}
