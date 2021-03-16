using BMF.Shared.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Shared.EntityMappings.UserManagement
{

    public partial class M_UserRoundSignIn_Mapping : EFCoreMappingBuilder<M_UserRoundSignIn>
    {
        public M_UserRoundSignIn_Mapping() { }

        public M_UserRoundSignIn_Mapping(EntityTypeBuilder<M_UserRoundSignIn> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_UserRoundSignIn> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.SignIn).HasColumnType("DATETIME2(7)").IsRequired(true);
            // Ignored Attributes
            // Foreign Keys
            entityBuilder.Property(t => t.ID_UserProfile).IsRequired(true);
            entityBuilder.Property(t => t.ID_UserLicense).IsRequired(true);
            entityBuilder.Property(t => t.ID_EventRound).IsRequired(true);
            // Relationships
            entityBuilder.HasOne(g => g.UserProfile).WithMany(s => s.UserRoundSignIns).HasForeignKey(s => s.ID_UserProfile).OnDelete(DeleteBehavior.NoAction);
            entityBuilder.HasOne(g => g.UserLicense).WithMany(s => s.UserRoundSignIns).HasForeignKey(s => s.ID_UserLicense);
            entityBuilder.HasOne(g => g.EventRound).WithMany(s => s.UserRoundSignIns).HasForeignKey(s => s.ID_EventRound);
            // Indexes
        }
    }
}
