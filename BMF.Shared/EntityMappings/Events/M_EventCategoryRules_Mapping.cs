using BMF.Shared.Models.Events;

namespace BMF.Web.Admin.DBMappings.Events
{
    public partial class M_EventCategoryRules_Mapping : EFMapping<M_EventCategoryRules>
    {
        public M_EventCategoryRules_Mapping()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public override void MapCustomProperties()
        {
            // Attributes
            Property(t => t.ID_EventCategory).IsRequired();
            Property(t => t.Title).HasMaxLength(60).IsRequired();
            Property(t => t.PDF_Url).HasMaxLength(1024).IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            HasRequired(g => g.EventCategory).WithMany(s => s.EventCategoryRules).HasForeignKey(s => s.ID_EventCategory);
            // Indexes
        }
    }
}
