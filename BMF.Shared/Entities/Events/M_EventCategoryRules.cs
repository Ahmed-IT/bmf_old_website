using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Events
{
    public partial class M_EventCategoryRules : BaseEntity
    {
        #region Foreign Keys

        [JsonProperty(PropertyName = nameof(ID_EventCategory))]
        public long ID_EventCategory { get; set; }

        #endregion

        #region Properties

        [JsonProperty(PropertyName = nameof(Title))]
        public string Title { get; set; }

        [JsonProperty(PropertyName = nameof(PDF_Url))]
        public string PDF_Url { get; set; }

        #endregion

        #region Relationships

        [InverseProperty(nameof(M_EventCategory.EventCategoryRules))]
        public M_EventCategory EventCategory { get; set; }

        #endregion
    }
}
