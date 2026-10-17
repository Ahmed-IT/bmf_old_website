using BMF.Shared.Entities.Website;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Shared.EntityMappings.Website
{
    public partial class M_BRSubCategoryRulesRegs_EFCoreBuilder : EFCoreMappingBuilder<M_BRSubCategoryRulesRegs>
    {
        public M_BRSubCategoryRulesRegs_EFCoreBuilder() { }

        public M_BRSubCategoryRulesRegs_EFCoreBuilder(EntityTypeBuilder<M_BRSubCategoryRulesRegs> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_BRSubCategoryRulesRegs> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Title).HasMaxLength(60).IsRequired(true);
            entityBuilder.Property(t => t.PDF_Url).HasMaxLength(1024).IsRequired(false);
            // Ignored Attributes
            // Foreign Keys
            entityBuilder.Property(t => t.ID_SubCategory).IsRequired(true);
            // Relationships
            entityBuilder.HasOne(g => g.SubCategory).WithMany(s => s.RulesAndRegs).HasForeignKey(s => s.ID_SubCategory);
            // Indexes
        }
    }
}
