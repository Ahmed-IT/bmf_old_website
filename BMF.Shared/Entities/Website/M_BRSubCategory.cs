using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Website
{
    public partial class M_BRSubCategory : BaseEntity
    {
        #region Foreign Keys

        [ForeignKey(nameof(Category))]
        [Display(Name = "Category", Prompt = "Category")]
        [Required]
        [JsonProperty(PropertyName = nameof(ID_Category))]
        public long ID_Category { get; set; }

        #endregion

        #region Properties

        [Display(Name = "Title", Prompt = "Title")]
        [Required, MaxLength(60)]
        [JsonProperty(PropertyName = nameof(Title))]
        public string Title { get; set; }

        #endregion

        #region Relationships

        [Display(Name = "Category", Prompt = "Category")]
        [InverseProperty(nameof(M_BRCategory.SubCategories))]
        public M_BRCategory Category { get; set; }

        [Display(Name = "Bulletins", Prompt = "Bulletins")]
        [InverseProperty(nameof(M_BRSubCategoryBulletin.SubCategory))]
        public ICollection<M_BRSubCategoryBulletin> Bulletins{ get; set; }

        [Display(Name = "Rules and Regs", Prompt = "Rules and Regs")]
        [InverseProperty(nameof(M_BRSubCategoryRulesRegs.SubCategory))]
        public ICollection<M_BRSubCategoryRulesRegs> RulesAndRegs { get; set; }

        #endregion
    }
}
