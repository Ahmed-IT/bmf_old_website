using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Website
{
    public partial class M_BRCategory : BaseEntity
    {
        #region Foreign Keys

        #endregion

        #region Properties

        [Display(Name = "Title", Prompt = "Title")]
        [Required, MaxLength(60)]
        [JsonProperty(PropertyName = nameof(Title))]
        public string Title { get; set; }
        
        #endregion

        #region Relationships

        [Display(Name = "Sub-Categories", Prompt = "Sub-Categories")]
        [InverseProperty(nameof(M_BRSubCategory.Category))]
        public ICollection<M_BRSubCategory> SubCategories{ get; set; }
        
        #endregion
    }
}
