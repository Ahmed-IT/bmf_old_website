using BMF.Shared.Entities.Events;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.Events
{
    public partial class M_EventCategoryHeat_EFCoreBuilder : EFCoreMappingBuilder<M_EventCategoryHeat>
    {
        public M_EventCategoryHeat_EFCoreBuilder() { }

        public M_EventCategoryHeat_EFCoreBuilder(EntityTypeBuilder<M_EventCategoryHeat> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_EventCategoryHeat> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Name).HasMaxLength(60).IsRequired(true);
            // Ignored Attributes
            // Foreign Keys
            entityBuilder.Property(t => t.ID_EventCategory).IsRequired(true);
            // Relationships
            entityBuilder.HasOne(g => g.EventCategory).WithMany(s => s.EventCategoryHeats).HasForeignKey(s => s.ID_EventCategory);
            // Indexes
        }
    }
}
