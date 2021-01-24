using BMF.Shared.Models.Events;

namespace BMF.Web.Admin.DBMappings.Events
{
    public partial class M_EventSeason_Mapping : EFMapping<M_EventSeason>
    {
        public M_EventSeason_Mapping()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public override void MapCustomProperties()
        {
            // Attributes
            Property(t => t.Title).HasMaxLength(60).IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            HasMany(g => g.EventCategories).WithRequired(s => s.EventSeason).HasForeignKey(s => s.ID_EventSeason);
            // Indexes
        }
    }
}
