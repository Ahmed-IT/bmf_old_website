using BMF.Shared.Entities.Events;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.Events
{
    public partial class M_EventRoundSessionUserResults_EFCoreBuilder : EFCoreMappingBuilder<M_EventRoundSessionUserResults>
    {
        public M_EventRoundSessionUserResults_EFCoreBuilder() { }

        public M_EventRoundSessionUserResults_EFCoreBuilder(EntityTypeBuilder<M_EventRoundSessionUserResults> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_EventRoundSessionUserResults> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Points).IsRequired(true);
            // Ignored Attributes
            // Foreign Keys
            entityBuilder.Property(t => t.ID_EventRoundSession).IsRequired(true);
            //entityBuilder.Property(t => t.ID_User).IsRequired(true);
            // Relationships
            entityBuilder.HasOne(g => g.EventRoundSession).WithMany(s => s.UserResults).HasForeignKey(s => s.ID_EventRoundSession);
            //entityBuilder.HasOne(g => g.User).WithMany(s => s.UserResults).HasForeignKey(s => s.ID_User);
            // Indexes
        }
    }
}
