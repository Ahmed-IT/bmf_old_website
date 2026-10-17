using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Website
{
    public partial class M_GalleryCategory : BaseEntity
    {
        #region Properties

        [Display(Name = "Title")]
        [StringLength(255), MaxLength(255), MinLength(1), Required]
        [JsonProperty(PropertyName = nameof(Title))]
        public string Title { get; set; }

        [Display(Name = "Image URL")]
        [StringLength(1024), MaxLength(1024), MinLength(1), Required]
        [JsonProperty(PropertyName = nameof(Image_URL))]
        public string Image_URL { get; set; }

        #endregion

        #region Relationships

        [InverseProperty(nameof(M_GalleryImages.GalleryCategory))]
        public ICollection<M_GalleryImages> GalleryImages { get; set; }

        #endregion
    }
}
