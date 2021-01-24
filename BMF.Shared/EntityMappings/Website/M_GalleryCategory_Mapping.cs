using BMF.Shared.Models.Website;

namespace BMF.Web.Admin.DBMappings.Website
{
    public partial class M_GalleryCategory_Mapping : EFMapping<M_GalleryCategory>
    {
        public M_GalleryCategory_Mapping()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public override void MapCustomProperties()
        {
            // Attributes
            Property(t => t.Title).HasMaxLength(255).IsRequired();
            Property(t => t.Image_URL).HasMaxLength(1024).IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            HasMany(g => g.GalleryImages).WithRequired(s => s.GalleryCategory).HasForeignKey(s => s.ID_GalleryCategory);
            // Indexes
        }
    }
}