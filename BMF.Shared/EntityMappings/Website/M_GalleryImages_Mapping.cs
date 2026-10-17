using BMF.Shared.Entities.Website;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.Website
{
    public partial class M_GalleryImages_EFCoreBuilder : EFCoreMappingBuilder<M_GalleryImages>
    {
        public M_GalleryImages_EFCoreBuilder() { }

        public M_GalleryImages_EFCoreBuilder(EntityTypeBuilder<M_GalleryImages> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_GalleryImages> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Image_URL).HasMaxLength(1024).IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            entityBuilder.HasOne(g => g.GalleryCategory).WithMany(s => s.GalleryImages).HasForeignKey(s => s.ID_GalleryCategory);
            // Indexes
        }
    }
}