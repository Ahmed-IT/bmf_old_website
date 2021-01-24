using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Location
{
    public partial class Country : BaseEntity
    {
        #region Properties

        [Display(Name = "Name")]
        [StringLength(255), MaxLength(255), MinLength(1), Required]
        [JsonProperty(PropertyName = nameof(Name))]
        public string Name { get; set; }

        [Display(Name = "2-Digit AlphaCode")]
        [StringLength(2), MaxLength(2), MinLength(2), Required]
        [JsonProperty(PropertyName = nameof(AlphaCode_2))]
        public string AlphaCode_2 { get; set; }

        [Display(Name = "3-Digit AlphaCode")]
        [StringLength(3), MaxLength(3), MinLength(3), Required]
        [JsonProperty(PropertyName = nameof(AlphaCode_3))]
        public string AlphaCode_3 { get; set; }

        [Display(Name = "Numeric Code")]
        [JsonProperty(PropertyName = nameof(NumericCode))]
        public int NumericCode { get; set; }

        [Display(Name = "Demonym")]
        [StringLength(50), MaxLength(50)]
        [JsonProperty(PropertyName = nameof(Demonym))]
        public string Demonym { get; set; }

        [Display(Name = "Region")]
        [StringLength(50), MaxLength(50)]
        [JsonProperty(PropertyName = nameof(Region))]
        public string Region { get; set; }

        [Display(Name = "Sub-region")]
        [StringLength(50), MaxLength(50)]
        [JsonProperty(PropertyName = nameof(SubRegion))]
        public string SubRegion { get; set; }

        [Display(Name = "Calling Code")]
        [StringLength(50), MaxLength(50)]
        [JsonProperty(PropertyName = nameof(CallingCode))]
        public string CallingCode { get; set; }

        #endregion

        #region Foreign Keys

        #endregion

        #region Relationships

        #endregion
    }
}
