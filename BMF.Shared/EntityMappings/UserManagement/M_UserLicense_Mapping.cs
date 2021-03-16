using BMF.Shared.Entities.UserManagement;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.UserManagement
{
    public partial class M_UserLicense_EFCoreBuilder : EFCoreMappingBuilder<M_UserLicense>
    {
        public M_UserLicense_EFCoreBuilder() { }

        public M_UserLicense_EFCoreBuilder(EntityTypeBuilder<M_UserLicense> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_UserLicense> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.License_Number).HasMaxLength(50).IsRequired(true);
            entityBuilder.Property(t => t.Expire_Date).HasColumnType("DATETIME2(7)").IsRequired(true);
            // Ignored Attributes
            // Foreign Keys
            entityBuilder.Property(t => t.ID_UserProfile).IsRequired(true);
            entityBuilder.Property(t => t.ID_LicenseType).IsRequired(true);
            // Relationships
            entityBuilder.HasOne(g => g.UserProfile).WithMany(s => s.UserLicenses).HasForeignKey(s => s.ID_UserProfile);
            entityBuilder.HasOne(g => g.LicenseType).WithMany(s => s.UserLicenses).HasForeignKey(s => s.ID_LicenseType);
            entityBuilder.HasMany(g => g.UserRoundSignIns).WithOne(s => s.UserLicense).HasForeignKey(s => s.ID_UserLicense);
            entityBuilder.HasMany(g => g.UserLicenseRenewal).WithOne(s => s.UserLicense).HasForeignKey(s => s.ID_UserLicense);
            // Indexes
        }
    }
}
