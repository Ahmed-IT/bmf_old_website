using BMF.Shared.Entities.UserManagement;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.UserManagement
{
    public partial class M_UserProfile_EFCoreBuilder : EFCoreMappingBuilder<M_UserProfile>
    {
        public M_UserProfile_EFCoreBuilder() { }

        public M_UserProfile_EFCoreBuilder(EntityTypeBuilder<M_UserProfile> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_UserProfile> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.FullName).HasMaxLength(255).IsRequired(true);
            entityBuilder.Property(t => t.CPR).HasMaxLength(60).IsRequired(false);
            entityBuilder.Property(t => t.DateOfBirth).HasColumnType("DATETIME2(7)").IsRequired(true);
            entityBuilder.Property(t => t.Mobile_Number).HasMaxLength(60).IsRequired(false);
            entityBuilder.Property(t => t.Full_Address).HasMaxLength(1024).IsRequired(false);
            entityBuilder.Property(t => t.Blood_Group).HasMaxLength(5).IsRequired(false);
            entityBuilder.Property(t => t.Place_Of_Birth).HasMaxLength(60).IsRequired(false);
            entityBuilder.Property(t => t.EmergencyContact_Name).HasMaxLength(255).IsRequired(false);
            entityBuilder.Property(t => t.EmergencyContact_MobileNumber).HasMaxLength(60).IsRequired(false);
            entityBuilder.Property(t => t.Parent_FullName).HasMaxLength(255).IsRequired(false);
            entityBuilder.Property(t => t.Parent_CPR).HasMaxLength(60).IsRequired(false);
            entityBuilder.Property(t => t.Photo_URL).HasMaxLength(1024).IsRequired(false);
            // Ignored Attributes
            // Foreign Keys
            entityBuilder.Property(t => t.ID_Nationality).IsRequired(false);
            // Relationships
            entityBuilder.HasOne(g => g.Nationality).WithMany(s => s.Users).HasForeignKey(s => s.ID_Nationality);
            entityBuilder.HasOne(g => g.UserAccount).WithOne(s => s.UserProfile).HasForeignKey<M_UserAccount>(s => s.ID);
            entityBuilder.HasMany(g => g.UserRoundSignIns).WithOne(s => s.UserProfile).HasForeignKey(s => s.ID_UserProfile);
            // Indexes
        }
    }
}
