using BMF.Shared.Models.UserManagement;

namespace BMF.Web.Admin.DBMappings.UserManagement
{
    public partial class M_UserLicense_Mapping : EFMapping<M_UserLicense>
    {
        public M_UserLicense_Mapping()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public override void MapCustomProperties()
        {
            // Attributes
            Property(t => t.ID_User).IsRequired();
            Property(t => t.ID_LicenseType).IsRequired();
            Property(t => t.License_Number).HasMaxLength(50).IsRequired();
            Property(t => t.Expire_Date).HasColumnType("DATETIME2").IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            HasRequired(g => g.User).WithMany(s => s.UserLicense).HasForeignKey(s => s.ID_User);
            HasRequired(g => g.LicenseType).WithMany(s => s.UserLicense).HasForeignKey(s => s.ID_LicenseType);
            HasMany(g => g.UserLicenseRenewal).WithRequired(s => s.UserLicense).HasForeignKey(s => s.ID_UserLicense);
            // Indexes
        }
    }
}
