using BMF.Shared.Models.UserManagement;

namespace BMF.Web.Admin.DBMappings.UserManagement
{
    public partial class M_UserLicenseRenewalMedicForms_Mapping : EFMapping<M_UserLicenseRenewalMedicForms>
    {
        public M_UserLicenseRenewalMedicForms_Mapping()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public override void MapCustomProperties()
        {
            // Attributes
            Property(t => t.ID_UserLicenseRenewal).IsRequired();
            Property(t => t.Date).HasColumnType("DATETIME2").IsRequired();
            Property(t => t.PDF_File).HasMaxLength(1024).IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            HasRequired(g => g.UserLicenseRenewal).WithMany(s => s.MedicForms).HasForeignKey(s => s.ID_UserLicenseRenewal);
            // Indexes
        }
    }
}
