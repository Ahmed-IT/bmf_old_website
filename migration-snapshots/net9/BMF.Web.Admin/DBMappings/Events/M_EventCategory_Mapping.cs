
namespace BMF.Web.Admin.DBMappings.Events
{
    public partial class M_EventCategory_Mapping : EFMapping<M_EventCategory>
    {
        public M_EventCategory_Mapping()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public override void MapCustomProperties()
        {
            // Attributes
            Property(t => t.ID_EventSeason).IsRequired();
            Property(t => t.Title).HasMaxLength(60).IsRequired();
            Property(t => t.Image_URL).HasMaxLength(1024).IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            HasRequired(g => g.EventSeason).WithMany(s => s.EventCategories).HasForeignKey(s => s.ID_EventSeason);
            HasMany(g => g.EventRounds).WithRequired(s => s.EventCategory).HasForeignKey(s => s.ID_EventCategory);
            // Indexes
        }
    }
}
