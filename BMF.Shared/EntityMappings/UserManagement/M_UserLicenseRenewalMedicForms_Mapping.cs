//using BMF.Shared.Entities.UserManagement;
//using BMF.Shared.EntityMappings;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace BMF.Web.Admin.DBMappings.UserManagement
//{
//    public partial class M_UserLicenseRenewalMedicForms_EFCoreBuilde : EFCoreMappingBuilder<M_UserLicenseRenewalMedicForms>
//    {
//        public M_UserLicenseRenewalMedicForms_EFCoreBuilde() { }

//        public M_UserLicenseRenewalMedicForms_EFCoreBuilde(EntityTypeBuilder<M_UserLicenseRenewalMedicForms> entityBuilder)
//        {
//            MapDefaultProperties(entityBuilder);
//            MapCustomProperties(entityBuilder);
//        }

//        public override void MapCustomProperties(EntityTypeBuilder<M_UserLicenseRenewalMedicForms> entityBuilder)
//        {
//            // Attributes
//            entityBuilder.Property(t => t.PDF_File).HasMaxLength(1024).IsRequired(true);
//            entityBuilder.Property(t => t.Date).HasColumnType("DATETIME2(7)").IsRequired(true);
//            // Ignored Attributes
//            // Foreign Keys
//            entityBuilder.Property(t => t.ID_UserLicenseRenewal).IsRequired(true);
//            // Relationships
//            entityBuilder.HasOne(g => g.UserLicenseRenewal).WithMany(s => s.MedicForms).HasForeignKey(s => s.ID_UserLicenseRenewal);
//            // Indexes
//        }
//    }
//}
