using BMF.Shared.Entities.Events;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.Events
{
    public partial class M_EventCategory_EFCoreBuilder : EFCoreMappingBuilder<M_EventCategory>
    {
        public M_EventCategory_EFCoreBuilder() { }

        public M_EventCategory_EFCoreBuilder(EntityTypeBuilder<M_EventCategory> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_EventCategory> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Title).HasMaxLength(60).IsRequired(true);
            entityBuilder.Property(t => t.Image_URL).HasMaxLength(1024).IsRequired(false);
            // Ignored Attributes
            // Foreign Keys
            entityBuilder.Property(t => t.ID_EventSeason).IsRequired(true);
            // Relationships
            entityBuilder.HasOne(g => g.EventSeason).WithMany(s => s.EventCategories).HasForeignKey(s => s.ID_EventSeason);
            entityBuilder.HasMany(g => g.EventRounds).WithOne(s => s.EventCategory).HasForeignKey(s => s.ID_EventCategory);
            // Indexes
        }
    }
}
