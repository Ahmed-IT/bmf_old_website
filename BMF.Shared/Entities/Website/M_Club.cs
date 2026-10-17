using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BMF.Shared.Entities.Website
{
    public partial class M_Club : BaseEntity
    {
        #region Properties

        [Display(Name = "Title")]
        [StringLength(255), MaxLength(255), MinLength(1), Required]
        [JsonProperty(PropertyName = nameof(Title))]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [JsonProperty(PropertyName = nameof(Description))]
        public string Description { get; set; }

        [Display(Name = "Website Link")]
        [StringLength(1024), MaxLength(1024), MinLength(1), Required]
        [JsonProperty(PropertyName = nameof(Website_Link))]
        public string Website_Link { get; set; }

        [Display(Name = "Image (Logo) URL")]
        [StringLength(1024), MaxLength(1024), MinLength(1), Required]
        [JsonProperty(PropertyName = nameof(Image_URL_Logo))]
        public string Image_URL_Logo { get; set; }

        [Display(Name = "Image (Banner) URL")]
        [StringLength(1024), MaxLength(1024), MinLength(1), Required]
        [JsonProperty(PropertyName = nameof(Image_URL_Banner))]
        public string Image_URL_Banner { get; set; }

        #endregion
    }
}
