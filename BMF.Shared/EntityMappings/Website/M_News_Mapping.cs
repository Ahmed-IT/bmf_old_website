using BMF.Shared.Models.Website;

namespace BMF.Web.Admin.DBMappings.Website
{
    public partial class M_News_Mapping : EFMapping<M_News>
    {
        public M_News_Mapping()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public override void MapCustomProperties()
        {
            // Attributes
            Property(t => t.Title).HasMaxLength(255).IsRequired();
            Property(t => t.Content).HasMaxLength(3000).IsRequired();
            Property(t => t.Image_URL).HasMaxLength(1024).IsRequired();
            Property(t => t.NewsTimestampUtc).HasColumnType("DATETIME2");
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            // Indexes
        }
    }
}