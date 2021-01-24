using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BMF.Shared.Entities.Security;

namespace BMF.Shared.EntityMappings.Security
{
    public partial class AdminUserRole_EFCoreBuilder : EFCoreMappingBuilder<AdminUserRole>
    {
        public AdminUserRole_EFCoreBuilder() { }

        public AdminUserRole_EFCoreBuilder(EntityTypeBuilder<AdminUserRole> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<AdminUserRole> entityBuilder)
        {
            // Attributes
            // Ignored Attributes
            // Foreign Keys
            entityBuilder.Property(t => t.ID_Role).IsRequired(true);
            entityBuilder.Property(t => t.ID_User).IsRequired(true);
            // Relationships
            entityBuilder.HasOne(s => s.Role).WithMany(g => g.AssignedUsers).HasForeignKey(s => s.ID_Role);
            entityBuilder.HasOne(s => s.User).WithMany(g => g.AssignedRoles).HasForeignKey(s => s.ID_User);
            // Indexes
            entityBuilder.HasIndex(p => new { p.ID_Role, p.ID_User, p.DeletedOnUtc }).IsUnique(true);
        }
    }
}
