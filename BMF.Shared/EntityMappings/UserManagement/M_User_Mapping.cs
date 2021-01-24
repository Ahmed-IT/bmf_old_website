using BMF.Shared.Entities.UserManagement;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.UserManagement
{
    public partial class M_User_EFCoreBuilder : EFCoreMappingBuilder<M_User>
    {
        public M_User_EFCoreBuilder() { }

        public M_User_EFCoreBuilder(EntityTypeBuilder<M_User> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_User> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Email_Address).HasMaxLength(120).IsRequired(true);
            entityBuilder.Property(t => t.CPR_Number).HasMaxLength(60).IsRequired(true);
            entityBuilder.Property(t => t.Password).HasMaxLength(225).IsRequired(true);
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            entityBuilder.HasOne(g => g.UserProfile).WithOne(s => s.User).HasForeignKey<M_UserProfile>(s => s.ID);
            entityBuilder.HasMany(g => g.UserLicense).WithOne(s => s.User).HasForeignKey(s => s.ID_User);
            entityBuilder.HasMany(g => g.UserResults).WithOne(s => s.User).HasForeignKey(s => s.ID_User);
            entityBuilder.HasMany(g => g.MyRaces).WithOne(s => s.User).HasForeignKey(s => s.ID_User);
            entityBuilder.HasMany(g => g.MyFavRaces).WithOne(s => s.User).HasForeignKey(s => s.ID_User);
            // Indexes
        }
    }
}
