using BMF.Shared.Entities.Events;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.Events
{
    public partial class M_EventSeason_EFCoreBuilder : EFCoreMappingBuilder<M_EventSeason>
    {
        public M_EventSeason_EFCoreBuilder() { }

        public M_EventSeason_EFCoreBuilder(EntityTypeBuilder<M_EventSeason> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_EventSeason> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Title).HasMaxLength(60).IsRequired(true);
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            entityBuilder.HasMany(g => g.EventRounds).WithOne(s => s.EventSeason).HasForeignKey(s => s.ID_EventSeason);
            // Indexes
        }
    }
}
