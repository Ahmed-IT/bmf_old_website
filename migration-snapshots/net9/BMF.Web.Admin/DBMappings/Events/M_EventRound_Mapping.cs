
namespace BMF.Web.Admin.DBMappings.Events
{
    public partial class M_EventRound_Mapping : EFMapping<M_EventRound>
    {
        public M_EventRound_Mapping()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public override void MapCustomProperties()
        {
            // Attributes
            Property(t => t.ID_EventCategory).IsRequired();
            Property(t => t.Round_Number).HasMaxLength(10).IsRequired();
            Property(t => t.DateTime_Start).HasColumnType("DATETIME2").IsRequired();
            Property(t => t.DateTime_End).HasColumnType("DATETIME2").IsRequired();
            Property(t => t.AddTo_Calendar).IsRequired();
            Property(t => t.AddTo_Countdown).IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            HasRequired(g => g.EventCategory).WithMany(s => s.EventRounds).HasForeignKey(s => s.ID_EventCategory);
            HasMany(g => g.EventRoundSessions).WithRequired(s => s.EventRound).HasForeignKey(s => s.ID_EventRound);
            // Indexes
        }
    }
}
