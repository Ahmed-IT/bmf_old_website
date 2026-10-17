using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Events
{
    public partial class M_EventCategoryHeat : BaseEntity
    {
        #region Foreign Keys

        [ForeignKey(nameof(EventCategory))]
        [Display(Name = "Event Category", Prompt = "Event Category")]
        [Required]
        [JsonProperty(PropertyName = nameof(ID_EventCategory))]
        public long ID_EventCategory { get; set; }

        #endregion

        #region Properties

        [Display(Name = "Heat Name", Prompt = "Heat Name")]
        [Required, MaxLength(60)]
        [JsonProperty(PropertyName = nameof(Name))]
        public string Name { get; set; }

        #endregion

        #region Relationships

        [InverseProperty(nameof(M_EventCategory.EventCategoryHeats))]
        public M_EventCategory EventCategory { get; set; }

        #endregion
    }
}
