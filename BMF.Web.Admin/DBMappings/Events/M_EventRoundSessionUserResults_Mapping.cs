using BMF.Shared.Models.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.Events
{
    public partial class M_EventRoundSessionUserResults_Mapping : IEntityTypeConfiguration<M_EventRoundSessionUserResults>
    {
        public void Configure(EntityTypeBuilder<M_EventRoundSessionUserResults> builder)
        {
            builder.HasKey(t => t.ID);
            builder.Property(t => t.ID_EventRoundSession).IsRequired();
            builder.Property(t => t.ID_User).IsRequired();
            builder.Property(t => t.Points).IsRequired();

            builder.HasOne(g => g.EventRoundSession)
                   .WithMany(s => s.UserResults)
                   .HasForeignKey(s => s.ID_EventRoundSession);

            builder.HasOne(g => g.User)
                   .WithMany(s => s.UserResults)
                   .HasForeignKey(s => s.ID_User);
        }
    }
}
