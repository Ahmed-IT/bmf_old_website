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
            entityBuilder.Property(t => t.Nationality).HasMaxLength(255);
            entityBuilder.Property(t => t.Mobile_Number).HasMaxLength(60).IsRequired(true);
            entityBuilder.Property(t => t.Full_Address).HasMaxLength(1024).IsRequired(true);
            entityBuilder.Property(t => t.Blood_Group).HasMaxLength(5).IsRequired(true);
            entityBuilder.Property(t => t.Place_Of_Birth).HasMaxLength(1024);
            entityBuilder.Property(t => t.Photo_URL).HasMaxLength(1024).IsRequired(true);
            entityBuilder.Property(t => t.DateOfBirth).HasColumnType("DATETIME2(7)").IsRequired(true);
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            entityBuilder.HasOne(g => g.User).WithOne(s => s.UserProfile).HasForeignKey<M_UserProfile>(s => s.ID);
            // Indexes
        }
    }
}
