using BMF.Shared.Entities.UserManagement;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.UserManagement
{
    public partial class M_MyRaces_EFCoreBuilder : EFCoreMappingBuilder<M_MyRaces>
    {
        public M_MyRaces_EFCoreBuilder() { }

        public M_MyRaces_EFCoreBuilder(EntityTypeBuilder<M_MyRaces> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_MyRaces> entityBuilder)
        {
            // Attributes
            // Ignored Attributes
            // Foreign Keys
            entityBuilder.Property(t => t.ID_User).IsRequired(true);
            entityBuilder.Property(t => t.ID_EventRoundSession).IsRequired(true);
            // Relationships
            entityBuilder.HasOne(g => g.User).WithMany(s => s.MyRaces).HasForeignKey(s => s.ID_User);
            entityBuilder.HasOne(g => g.EventRoundSession).WithMany(s => s.User_MyRaces).HasForeignKey(s => s.ID_EventRoundSession);
            // Indexes
        }
    }
}
