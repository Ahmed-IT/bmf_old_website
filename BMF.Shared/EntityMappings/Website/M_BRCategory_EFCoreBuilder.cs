using BMF.Shared.Entities.Website;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Shared.EntityMappings.Website
{
    public partial class M_BRCategory_EFCoreBuilder : EFCoreMappingBuilder<M_BRCategory>
    {
        public M_BRCategory_EFCoreBuilder() { }

        public M_BRCategory_EFCoreBuilder(EntityTypeBuilder<M_BRCategory> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_BRCategory> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Title).HasMaxLength(60).IsRequired(true);
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            entityBuilder.HasMany(g => g.SubCategories).WithOne(s => s.Category).HasForeignKey(s => s.ID_Category);
            // Indexes
        }
    }
}
