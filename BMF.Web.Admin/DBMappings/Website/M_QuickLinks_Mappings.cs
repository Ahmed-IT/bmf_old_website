using BMF.Shared.Models.Website;

namespace BMF.Web.Admin.DBMappings.Website
{
    public partial class M_QuickLinks_Mappings : EFMapping<M_QuickLinks>
    {
        public M_QuickLinks_Mappings()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public override void MapCustomProperties()
        {
            // Attributes
            Property(t => t.Title).HasMaxLength(255).IsRequired();
            Property(t => t.Website_Link).HasMaxLength(1024).IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            // Indexes
        }
    }
}