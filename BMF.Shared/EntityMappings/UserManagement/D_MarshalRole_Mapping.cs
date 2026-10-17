using BMF.Shared.Entities.UserManagement;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Shared.EntityMappings.UserManagement
{
    public partial class D_MarshalRole_Mapping : EFCoreMappingBuilder<D_MarshalRole>
    {
        public D_MarshalRole_Mapping() { }

        public D_MarshalRole_Mapping(EntityTypeBuilder<D_MarshalRole> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<D_MarshalRole> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Name).HasMaxLength(60).IsRequired(true);
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            entityBuilder.HasMany(g => g.UserLicenses).WithOne(s => s.MarshalRole).HasForeignKey(s => s.ID_MarshalRole);
            // Indexes
        }
    }
}
