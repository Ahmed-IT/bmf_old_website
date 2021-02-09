using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Events
{
    public partial class M_EventCategoryBulletins : BaseEntity
    {
        #region Foreign Keys

        [ForeignKey(nameof(EventCategory))]
        [Display(Name = "Event Category", Prompt = "Event Category")]
        [Required]
        [JsonProperty(PropertyName = nameof(ID_EventCategory))]
        public long ID_EventCategory { get; set; }

        #endregion

        #region Properties

        [Display(Name = "Title", Prompt = "Title")]
        [Required, MaxLength(60)]
        [JsonProperty(PropertyName = nameof(Title))]
        public string Title { get; set; }

        [Display(Name = "PDF Url", Prompt = "PDF Url")]
        [Required, MaxLength(1024)]
        [JsonProperty(PropertyName = nameof(PDF_Url))]
        public string PDF_Url { get; set; }

        #endregion

        #region Relationships

        [InverseProperty(nameof(M_EventCategory.EventCategoryBulletins))]
        public M_EventCategory EventCategory { get; set; }

        #endregion
    }
}
