using BMF.Shared.Entities.UserManagement;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.UserManagement
{
    public partial class M_UserAccount_EFCoreBuilder : EFCoreMappingBuilder<M_UserAccount>
    {
        public M_UserAccount_EFCoreBuilder() { }

        public M_UserAccount_EFCoreBuilder(EntityTypeBuilder<M_UserAccount> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_UserAccount> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Email_Address).HasMaxLength(120).IsRequired(true);
            entityBuilder.Property(t => t.Password).HasMaxLength(225).IsRequired(true);
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            entityBuilder.HasOne(g => g.UserProfile).WithOne(s => s.UserAccount).HasForeignKey<M_UserAccount>(s => s.ID);
            entityBuilder.HasMany(g => g.MyRaces).WithOne(s => s.UserAccount).HasForeignKey(s => s.ID_UserAccount);
            entityBuilder.HasMany(g => g.MyFavRaces).WithOne(s => s.UserAccount).HasForeignKey(s => s.ID_UserAccount);
            // Indexes
        }
    }
}
