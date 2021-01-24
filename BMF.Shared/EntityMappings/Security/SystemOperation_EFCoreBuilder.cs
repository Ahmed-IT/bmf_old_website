using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BMF.Shared.Entities.Security;

namespace BMF.Shared.EntityMappings.Security
{
    public partial class SystemOperation_EFCoreBuilder : EFCoreMappingBuilder<SystemOperation>
    {
        public SystemOperation_EFCoreBuilder() { }
       
        public SystemOperation_EFCoreBuilder(EntityTypeBuilder<SystemOperation> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<SystemOperation> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.SystemName).HasMaxLength(500).IsRequired(true);
            // Ignored Attributes
            entityBuilder.Ignore(c => c.CreatedOnUtc);
            entityBuilder.Ignore(c => c.CreatedBy);
            entityBuilder.Ignore(c => c.UpdatedOnUtc);
            entityBuilder.Ignore(c => c.UpdatedBy);
            entityBuilder.Ignore(c => c.DeletedOnUtc);
            entityBuilder.Ignore(c => c.DeletedBy);
            // Foreign Keys
            entityBuilder.Property(t => t.ID_Parent).IsRequired(false);
            // Relationships
            entityBuilder.HasOne(s => s.Parent).WithMany(g => g.Children).HasForeignKey(s => s.ID_Parent);
            entityBuilder.HasMany(g => g.AssignedAdminRoles).WithOne(s => s.Operation).HasForeignKey(s => s.ID_Operation);
            // Indexes
            entityBuilder.HasIndex(p => new { p.SystemName }).IsUnique(true);
        }
    }
}
