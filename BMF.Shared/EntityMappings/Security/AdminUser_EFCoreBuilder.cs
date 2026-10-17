using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BMF.Shared.Entities.Security;

namespace BMF.Shared.EntityMappings.Security
{
    public partial class AdminUser_EFCoreBuilder : EFCoreMappingBuilder<AdminUser>
    {
        public AdminUser_EFCoreBuilder() { }
       
        public AdminUser_EFCoreBuilder(EntityTypeBuilder<AdminUser> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<AdminUser> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Email).HasMaxLength(255).IsRequired(true);
            entityBuilder.Property(t => t.Username).HasMaxLength(255).IsRequired(true);
            entityBuilder.Property(t => t.FirstName).HasMaxLength(255).IsRequired(true);
            entityBuilder.Property(t => t.LastName).HasMaxLength(255).IsRequired(true);
            entityBuilder.Property(t => t.Password).HasMaxLength(255).IsRequired(true);
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            entityBuilder.HasMany(g => g.AssignedRoles).WithOne(s => s.User).HasForeignKey(s => s.ID_User);            
            // Indexes
            entityBuilder.HasIndex(p => new { p.Email, p.DeletedOnUtc }).IsUnique(true);
            entityBuilder.HasIndex(p => new { p.Username, p.DeletedOnUtc }).IsUnique(true);
        }
    }
}