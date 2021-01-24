using BMF.Shared.Entities.UserManagement;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.UserManagement
{
    public partial class D_LicenseType_EFCoreBuilder : EFCoreMappingBuilder<D_LicenseType>
    {
        public D_LicenseType_EFCoreBuilder() { }

        public D_LicenseType_EFCoreBuilder(EntityTypeBuilder<D_LicenseType> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<D_LicenseType> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Type).HasMaxLength(60).IsRequired(true);
            entityBuilder.Property(t => t.Title).HasMaxLength(120).IsRequired(true);
            entityBuilder.Property(t => t.Image_Front_URL).HasMaxLength(1024).IsRequired(true);
            entityBuilder.Property(t => t.Image_Back_URL).HasMaxLength(1024).IsRequired(true);
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            entityBuilder.HasMany(g => g.UserLicense).WithOne(s => s.LicenseType).HasForeignKey(s => s.ID_LicenseType);
            // Indexes
        }
    }
}
