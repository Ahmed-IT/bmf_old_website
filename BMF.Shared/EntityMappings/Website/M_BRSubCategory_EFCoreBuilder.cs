using BMF.Shared.Entities.Website;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Shared.EntityMappings.Website
{
    public partial class M_BRSubCategory_EFCoreBuilder : EFCoreMappingBuilder<M_BRSubCategory>
    {
        public M_BRSubCategory_EFCoreBuilder() { }

        public M_BRSubCategory_EFCoreBuilder(EntityTypeBuilder<M_BRSubCategory> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_BRSubCategory> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Title).HasMaxLength(60).IsRequired(true);
            // Ignored Attributes
            // Foreign Keys
            entityBuilder.Property(t => t.ID_Category).IsRequired(true);
            // Relationships
            entityBuilder.HasOne(g => g.Category).WithMany(s => s.SubCategories).HasForeignKey(s => s.ID_Category);
            entityBuilder.HasMany(g => g.Bulletins).WithOne(s => s.SubCategory).HasForeignKey(s => s.ID_SubCategory);
            entityBuilder.HasMany(g => g.RulesAndRegs).WithOne(s => s.SubCategory).HasForeignKey(s => s.ID_SubCategory);
            // Indexes
        }
    }
}
