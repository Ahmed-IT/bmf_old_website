using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Website
{
    public partial class M_GalleryImages : BaseEntity
    {
        #region Foreign Keys

        [Display(Name = "Gallery Category")]
        [Required]
        [JsonProperty(PropertyName = nameof(ID_GalleryCategory))]
        public long ID_GalleryCategory { get; set; }

        #endregion

        #region Properties

        [Display(Name = "Image URL")]
        [StringLength(1024), MaxLength(1024), MinLength(1), Required]
        [JsonProperty(PropertyName = nameof(Image_URL))]
        public string Image_URL { get; set; }

        #endregion

        #region Relationships

        [InverseProperty(nameof(M_GalleryCategory.GalleryImages))]
        public M_GalleryCategory GalleryCategory { get; set; }

        #endregion
    }
}
