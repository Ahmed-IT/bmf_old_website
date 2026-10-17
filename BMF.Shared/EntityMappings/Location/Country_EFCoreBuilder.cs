using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BMF.Shared.Entities.Location;

namespace BMF.Shared.EntityMappings.Location
{
    public partial class Country_EFCoreBuilder : EFCoreMappingBuilder<Country>
    {
        public Country_EFCoreBuilder() { }

        public Country_EFCoreBuilder(EntityTypeBuilder<Country> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<Country> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Name).HasMaxLength(255).IsRequired(true);
            entityBuilder.Property(t => t.AlphaCode_2).HasMaxLength(2).IsRequired(true);
            entityBuilder.Property(t => t.AlphaCode_3).HasMaxLength(3).IsRequired(true);
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            // Indexes
            entityBuilder.HasIndex(p => new { p.Name }).IsUnique(true);
        }
    }
}
