using BMF.Shared.Entities.Website;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.Website
{
    public partial class M_QuickLinks_EFCoreBuilder : EFCoreMappingBuilder<M_QuickLinks>
    {
        public M_QuickLinks_EFCoreBuilder() { }

        public M_QuickLinks_EFCoreBuilder(EntityTypeBuilder<M_QuickLinks> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_QuickLinks> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Title).HasMaxLength(255).IsRequired(true);
            entityBuilder.Property(t => t.Website_Link).HasMaxLength(1024).IsRequired(true);
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            // Indexes
        }
    }
}