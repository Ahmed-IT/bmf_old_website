using BMF.Shared.Models.Events;

namespace BMF.Web.Admin.DBMappings.Events
{
    public partial class M_EventRoundSession_Mapping : EFMapping<M_EventRoundSession>
    {
        public M_EventRoundSession_Mapping()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public override void MapCustomProperties()
        {
            // Attributes
            Property(t => t.ID_EventRound).IsRequired();
            Property(t => t.Title).HasMaxLength(120).IsRequired();
            Property(t => t.Results_PDF_URL).HasMaxLength(1024).IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            HasRequired(g => g.EventRound).WithMany(s => s.EventRoundSessions).HasForeignKey(s => s.ID_EventRound);
            HasMany(g => g.UserResults).WithRequired(s => s.EventRoundSession).HasForeignKey(s => s.ID_EventRoundSession);
            // Indexes
        }
    }
}
