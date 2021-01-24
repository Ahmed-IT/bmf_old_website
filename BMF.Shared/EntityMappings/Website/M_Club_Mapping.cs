using BMF.Shared.Models.Website;

namespace BMF.Web.Admin.DBMappings.Website
{
    public partial class M_Club_Mapping : EFMapping<M_Club>
    {
        public M_Club_Mapping()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public override void MapCustomProperties()
        {
            // Attributes
            Property(t => t.Title).HasMaxLength(255).IsRequired();
            Property(t => t.Description).HasMaxLength(1024);
            Property(t => t.Website_Link).HasMaxLength(1024).IsRequired();
            Property(t => t.Image_URL).HasMaxLength(1024).IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            // Indexes
        }
    }
}