using BMF.Shared.Models.UserManagement;

namespace BMF.Web.Admin.DBMappings.UserManagement
{
    public partial class M_UserLicenseRenewal_Mapping : EFMapping<M_UserLicenseRenewal>
    {
        public M_UserLicenseRenewal_Mapping()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public override void MapCustomProperties()
        {
            // Attributes
            Property(t => t.ID_UserLicense).IsRequired();
            Property(t => t.Issue_Type).HasMaxLength(15).IsRequired();
            Property(t => t.Issue_Date).HasColumnType("DATETIME2").IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            HasRequired(g => g.UserLicense).WithMany(s => s.UserLicenseRenewal).HasForeignKey(s => s.ID_UserLicense);
            HasMany(g => g.MedicForms).WithRequired(s => s.UserLicenseRenewal).HasForeignKey(s => s.ID_UserLicenseRenewal);
            // Indexes
        }
    }
}
