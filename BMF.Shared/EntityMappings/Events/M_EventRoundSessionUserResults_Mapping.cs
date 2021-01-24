using BMF.Shared.Models.Events;

namespace BMF.Web.Admin.DBMappings.Events
{
    public partial class M_EventRoundSessionUserResults_Mapping : EFMapping<M_EventRoundSessionUserResults>
    {
        public M_EventRoundSessionUserResults_Mapping()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public override void MapCustomProperties()
        {
            // Attributes
            Property(t => t.ID_EventRoundSession).IsRequired();
            Property(t => t.ID_User).IsRequired();
            Property(t => t.Points).IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            HasRequired(g => g.EventRoundSession).WithMany(s => s.UserResults).HasForeignKey(s => s.ID_EventRoundSession);
            HasRequired(g => g.User).WithMany(s => s.UserResults).HasForeignKey(s => s.ID_User);
            // Indexes
        }
    }
}
