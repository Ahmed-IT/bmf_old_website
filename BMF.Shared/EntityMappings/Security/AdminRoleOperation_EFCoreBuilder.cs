using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BMF.Shared.Entities.Security;

namespace BMF.Shared.EntityMappings.Security
{
    public partial class AdminRoleOperation_EFCoreBuilder : EFCoreMappingBuilder<AdminRoleOperation>
    {
        public AdminRoleOperation_EFCoreBuilder() { }

        public AdminRoleOperation_EFCoreBuilder(EntityTypeBuilder<AdminRoleOperation> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<AdminRoleOperation> entityBuilder)
        {
            // Attributes
            // Ignored Attributes
            // Foreign Keys
            entityBuilder.Property(t => t.ID_Role).IsRequired(true);
            entityBuilder.Property(t => t.ID_Operation).IsRequired(true);
            // Relationships
            entityBuilder.HasOne(s => s.Role).WithMany(g => g.AssignedOperations).HasForeignKey(s => s.ID_Role);
            entityBuilder.HasOne(s => s.Operation).WithMany(g => g.AssignedAdminRoles).HasForeignKey(s => s.ID_Operation);
            // Indexes
            entityBuilder.HasIndex(p => new { p.ID_Role, p.ID_Operation, p.DeletedOnUtc }).IsUnique(true);
        }
    }
}
