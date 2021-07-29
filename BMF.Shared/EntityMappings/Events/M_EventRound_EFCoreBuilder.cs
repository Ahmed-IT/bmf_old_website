using BMF.Shared.Entities.Events;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.Events
{
    public partial class M_EventRound_EFCoreBuilder : EFCoreMappingBuilder<M_EventRound>
    {
        public M_EventRound_EFCoreBuilder() { }

        public M_EventRound_EFCoreBuilder(EntityTypeBuilder<M_EventRound> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_EventRound> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Round_Number).HasMaxLength(10).IsRequired(true);
            entityBuilder.Property(t => t.DateTime_Start).HasColumnType("DATETIME2(7)").IsRequired(true);
            entityBuilder.Property(t => t.DateTime_End).HasColumnType("DATETIME2(7)").IsRequired(true);
            entityBuilder.Property(t => t.AddTo_Calendar).IsRequired(true);
            entityBuilder.Property(t => t.AddTo_Countdown).IsRequired(true);
            // Ignored Attributes
            // Foreign Keys
            entityBuilder.Property(t => t.ID_EventSeason).IsRequired(true);
            entityBuilder.Property(t => t.ID_EventCategory).IsRequired(true);
            // Relationships
            entityBuilder.HasOne(g => g.EventSeason).WithMany(s => s.EventRounds).HasForeignKey(s => s.ID_EventSeason);
            entityBuilder.HasOne(g => g.EventCategory).WithMany(s => s.EventRounds).HasForeignKey(s => s.ID_EventCategory);
            entityBuilder.HasMany(g => g.EventRoundSessions).WithOne(s => s.EventRound).HasForeignKey(s => s.ID_EventRound);
            entityBuilder.HasMany(g => g.UserRoundSignIns).WithOne(s => s.EventRound).HasForeignKey(s => s.ID_EventRound);
            // Indexes
        }
    }
}
