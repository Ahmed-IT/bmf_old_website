using BMF.Shared.Models.UserManagement;

namespace BMF.Web.Admin.DBMappings.UserManagement
{
    public partial class D_LicenseType_Mapping : EFMapping<D_LicenseType>
    {
        public D_LicenseType_Mapping()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public override void MapCustomProperties()
        {
            // Attributes
            Property(t => t.Type).HasMaxLength(60).IsRequired();
            Property(t => t.Title).HasMaxLength(120).IsRequired();
            Property(t => t.Image_Front_URL).HasMaxLength(1024).IsRequired();
            Property(t => t.Image_Back_URL).HasMaxLength(1024).IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            HasMany(g => g.UserLicense).WithRequired(s => s.LicenseType).HasForeignKey(s => s.ID_LicenseType);
            // Indexes
        }
    }
}
