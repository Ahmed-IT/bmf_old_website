using BMF.Shared.Models.UserManagement;

namespace BMF.Web.Admin.DBMappings.UserManagement
{
    public partial class M_User_Mapping : EFMapping<M_User>
    {
        public M_User_Mapping()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public override void MapCustomProperties()
        {
            // Attributes
            Property(t => t.Email_Address).HasMaxLength(120).IsRequired();
            Property(t => t.CPR_Number).HasMaxLength(60).IsRequired();
            Property(t => t.Password).HasMaxLength(225).IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            HasRequired(g => g.UserProfile).WithRequiredPrincipal(s => s.User);
            HasMany(g => g.UserLicense).WithRequired(s => s.User).HasForeignKey(s => s.ID_User);
            HasMany(g => g.UserResults).WithRequired(s => s.User).HasForeignKey(s => s.ID_User);
            HasMany(g => g.MyRaces).WithRequired(s => s.User).HasForeignKey(s => s.ID_User);
            HasMany(g => g.MyFavRaces).WithRequired(s => s.User).HasForeignKey(s => s.ID_User);
            // Indexes
        }
    }
}
