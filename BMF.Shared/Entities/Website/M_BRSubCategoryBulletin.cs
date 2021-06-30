using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Website
{
    public partial class M_BRSubCategoryBulletin : BaseEntity
    {
        #region Foreign Keys

        [ForeignKey(nameof(SubCategory))]
        [Display(Name = "Sub-Category", Prompt = "Sub-Category")]
        [Required]
        [JsonProperty(PropertyName = nameof(ID_SubCategory))]
        public long ID_SubCategory { get; set; }

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

        [InverseProperty(nameof(M_BRSubCategory.Bulletins))]
        public M_BRSubCategory SubCategory { get; set; }

        #endregion
    }
}
