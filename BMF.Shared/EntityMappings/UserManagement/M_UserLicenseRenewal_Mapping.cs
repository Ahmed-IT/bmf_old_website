//using BMF.Shared.Entities.UserManagement;
//using BMF.Shared.EntityMappings;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace BMF.Web.Admin.DBMappings.UserManagement
//{
//    public partial class M_UserLicenseRenewal_EFCoreBuilder : EFCoreMappingBuilder<M_UserLicenseRenewal>
//    {
//        public M_UserLicenseRenewal_EFCoreBuilder() { }

//        public M_UserLicenseRenewal_EFCoreBuilder(EntityTypeBuilder<M_UserLicenseRenewal> entityBuilder)
//        {
//            MapDefaultProperties(entityBuilder);
//            MapCustomProperties(entityBuilder);
//        }

//        public override void MapCustomProperties(EntityTypeBuilder<M_UserLicenseRenewal> entityBuilder)
//        {
//            // Attributes
//            entityBuilder.Property(t => t.Issue_Type).HasMaxLength(15).IsRequired(true);
//            entityBuilder.Property(t => t.Issue_Date).HasColumnType("DATETIME2(7)").IsRequired(true);
//            // Ignored Attributes
//            // Foreign Keys
//            entityBuilder.Property(t => t.ID_UserLicense).IsRequired(true);
//            // Relationships
//            entityBuilder.HasOne(g => g.UserLicense).WithMany(s => s.UserLicenseRenewal).HasForeignKey(s => s.ID_UserLicense);
//            entityBuilder.HasMany(g => g.MedicForms).WithOne(s => s.UserLicenseRenewal).HasForeignKey(s => s.ID_UserLicenseRenewal);
//            // Indexes
//        }
//    }
//}
