using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BMF.Shared.Entities.Security;

namespace BMF.Shared.EntityMappings.Security
{
    public partial class AdminRole_EFCoreBuilder : EFCoreMappingBuilder<AdminRole>
    {
        public AdminRole_EFCoreBuilder() { }
     
        public AdminRole_EFCoreBuilder(EntityTypeBuilder<AdminRole> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<AdminRole> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Name).HasMaxLength(255).IsRequired(true);
            entityBuilder.Property(t => t.Description).HasMaxLength(1024).IsRequired(false);
            entityBuilder.Property(t => t.IsIAMRelated).IsRequired(true).HasDefaultValue(false);
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            entityBuilder.HasMany(g => g.AssignedOperations).WithOne(s => s.Role).HasForeignKey(s => s.ID_Role);
            entityBuilder.HasMany(g => g.AssignedUsers).WithOne(s => s.Role).HasForeignKey(s => s.ID_Role);
            // Indexes
            entityBuilder.HasIndex(p => new { p.Name, p.DeletedOnUtc }).IsUnique(true);
        }
    }
}
