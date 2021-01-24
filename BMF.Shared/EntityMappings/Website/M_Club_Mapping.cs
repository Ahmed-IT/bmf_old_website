using BMF.Shared.Entities.Website;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.Website
{
    public partial class M_Club_EFCoreBuilder : EFCoreMappingBuilder<M_Club>
    {
        public M_Club_EFCoreBuilder() { }

        public M_Club_EFCoreBuilder(EntityTypeBuilder<M_Club> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_Club> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Title).HasMaxLength(255).IsRequired();
            entityBuilder.Property(t => t.Description).HasMaxLength(1024);
            entityBuilder.Property(t => t.Website_Link).HasMaxLength(1024).IsRequired();
            entityBuilder.Property(t => t.Image_URL).HasMaxLength(1024).IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            // Indexes
        }
    }
}