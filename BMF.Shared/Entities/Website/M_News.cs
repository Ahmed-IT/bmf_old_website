using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BMF.Shared.Entities.Website
{
    public partial class M_News : BaseEntity
    {
        #region Properties

        [Display(Name = "Title")]
        [StringLength(255), MaxLength(255), MinLength(1), Required]
        [JsonProperty(PropertyName = nameof(Title))]
        public string Title { get; set; }

        [Display(Name = "Content")]
        [StringLength(3000), MaxLength(3000), MinLength(1), Required]
        [JsonProperty(PropertyName = nameof(Content))]
        public string Content { get; set; }

        [Display(Name = "Image URL")]
        [StringLength(1024), MaxLength(1024), MinLength(1), Required]
        [JsonProperty(PropertyName = nameof(Image_URL))]
        public string Image_URL { get; set; }

        [Display(Name = "News Publication Date & Time")]
        [Required]
        [JsonProperty(PropertyName = nameof(NewsTimestampUtc))]
        public DateTime NewsTimestampUtc { get; set; }

        #endregion
    }
}
