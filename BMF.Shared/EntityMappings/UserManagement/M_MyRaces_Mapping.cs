using BMF.Shared.Models.UserManagement;

namespace BMF.Web.Admin.DBMappings.UserManagement
{
    public partial class M_MyRaces_Mapping : EFMapping<M_MyRaces>
    {
        public M_MyRaces_Mapping()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public override void MapCustomProperties()
        {
            // Attributes
            Property(t => t.ID_User).IsRequired();
            Property(t => t.ID_EventRoundSession).IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            HasRequired(g => g.User).WithMany(s => s.MyRaces).HasForeignKey(s => s.ID_User);
            HasRequired(g => g.EventRoundSession).WithMany(s => s.User_MyRaces).HasForeignKey(s => s.ID_EventRoundSession);
            // Indexes
        }
    }
}
