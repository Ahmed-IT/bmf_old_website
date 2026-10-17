using BMF.Shared.Entities.Events;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.Events
{
    public partial class M_EventRoundSession_EFCoreBuilder : EFCoreMappingBuilder<M_EventRoundSession>
    {
        public M_EventRoundSession_EFCoreBuilder() { }

        public M_EventRoundSession_EFCoreBuilder(EntityTypeBuilder<M_EventRoundSession> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_EventRoundSession> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Title).HasMaxLength(120).IsRequired(true);
            entityBuilder.Property(t => t.Results_PDF_URL).HasMaxLength(1024).IsRequired(true);
            // Ignored Attributes
            // Foreign Keys
            entityBuilder.Property(t => t.ID_EventRound).IsRequired(true);
            // Relationships
            entityBuilder.HasOne(g => g.EventRound).WithMany(s => s.EventRoundSessions).HasForeignKey(s => s.ID_EventRound);
            entityBuilder.HasMany(g => g.UserResults).WithOne(s => s.EventRoundSession).HasForeignKey(s => s.ID_EventRoundSession);
            // Indexes
        }
    }
}
