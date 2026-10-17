using BMF.Shared.Entities.UserManagement;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.UserManagement
{
    public partial class M_MyFavRaces_EFCoreBuilder : EFCoreMappingBuilder<M_MyFavRaces>
    {
        public M_MyFavRaces_EFCoreBuilder() { }

        public M_MyFavRaces_EFCoreBuilder(EntityTypeBuilder<M_MyFavRaces> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_MyFavRaces> entityBuilder)
        {
            // Attributes
            // Ignored Attributes
            // Foreign Keys
            entityBuilder.Property(t => t.ID_UserAccount).IsRequired(true);
            entityBuilder.Property(t => t.ID_EventRoundSession).IsRequired(true);
            // Relationships
            entityBuilder.HasOne(g => g.UserAccount).WithMany(s => s.MyFavRaces).HasForeignKey(s => s.ID_UserAccount);
            entityBuilder.HasOne(g => g.EventRoundSession).WithMany(s => s.User_MyFavRaces).HasForeignKey(s => s.ID_EventRoundSession);
            // Indexes
        }
    }
}
