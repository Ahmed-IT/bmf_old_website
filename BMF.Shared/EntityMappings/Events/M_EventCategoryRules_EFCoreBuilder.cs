using BMF.Shared.Entities.Events;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.Events
{
    public partial class M_EventCategoryRules_EFCoreBuilder : EFCoreMappingBuilder<M_EventCategoryRules>
    {
        public M_EventCategoryRules_EFCoreBuilder() { }

        public M_EventCategoryRules_EFCoreBuilder(EntityTypeBuilder<M_EventCategoryRules> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_EventCategoryRules> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Title).HasMaxLength(60).IsRequired(true);
            entityBuilder.Property(t => t.PDF_Url).HasMaxLength(1024).IsRequired(false);
            // Ignored Attributes
            // Foreign Keys
            entityBuilder.Property(t => t.ID_EventCategory).IsRequired(true);
            // Relationships
            entityBuilder.HasOne(g => g.EventCategory).WithMany(s => s.EventCategoryRules).HasForeignKey(s => s.ID_EventCategory);
            // Indexes
        }
    }
}
