using BMF.Shared.Entities.Website;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.Website
{
    public partial class M_GalleryCategory_EFCoreBuilder : EFCoreMappingBuilder<M_GalleryCategory>
    {
        public M_GalleryCategory_EFCoreBuilder() { }

        public M_GalleryCategory_EFCoreBuilder(EntityTypeBuilder<M_GalleryCategory> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_GalleryCategory> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Title).HasMaxLength(255).IsRequired(true);
            entityBuilder.Property(t => t.Image_URL).HasMaxLength(1024).IsRequired(true);
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            entityBuilder.HasMany(g => g.GalleryImages).WithOne(s => s.GalleryCategory).HasForeignKey(s => s.ID_GalleryCategory);
            // Indexes
        }
    }
}