using BMF.Shared.Entities.UserManagement;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.UserManagement
{
    public partial class M_UserLicenseRenewal_EFCoreBuilder : EFCoreMappingBuilder<M_UserLicenseRenewal>
    {
        public M_UserLicenseRenewal_EFCoreBuilder() { }

        public M_UserLicenseRenewal_EFCoreBuilder(EntityTypeBuilder<M_UserLicenseRenewal> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_UserLicenseRenewal> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.ApprovalStatus).HasMaxLength(120).IsRequired(true);
            entityBuilder.Property(t => t.DateIssued).HasColumnType("DATETIME2(7)").IsRequired(true);
            entityBuilder.Property(t => t.DateOfExpiry).HasColumnType("DATETIME2(7)").IsRequired(true);
            entityBuilder.Property(t => t.CPR_URL).HasMaxLength(1024).IsRequired(false);
            entityBuilder.Property(t => t.Parent_CPR_URL).HasMaxLength(1024).IsRequired(false);
            entityBuilder.Property(t => t.MedicalForm_URL).HasMaxLength(1024).IsRequired(false);
            entityBuilder.Property(t => t.NoObjectionLetter_URL).HasMaxLength(1024).IsRequired(false);
            // Ignored Attributes
            // Foreign Keys
            entityBuilder.Property(t => t.ID_UserLicense).IsRequired(true);
            // Relationships
            entityBuilder.HasOne(g => g.UserLicense).WithMany(s => s.UserLicenseRenewals).HasForeignKey(s => s.ID_UserLicense);
            // Indexes
        }
    }
}
