using BMF.Shared.Models.UserManagement;

namespace BMF.Web.Admin.DBMappings.UserManagement
{
    public partial class M_UserProfile_Mapping : EFMapping<M_UserProfile>
    {
        public M_UserProfile_Mapping()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public override void MapCustomProperties()
        {
            // Attributes
            Property(t => t.FullName).HasMaxLength(255).IsRequired();
            Property(t => t.Nationality).HasMaxLength(255);
            Property(t => t.DateOfBirth).HasColumnType("DATETIME2").IsRequired();
            Property(t => t.Mobile_Number).HasMaxLength(60).IsRequired();
            Property(t => t.Full_Address).HasMaxLength(1024).IsRequired();
            Property(t => t.Blood_Group).HasMaxLength(5).IsRequired();
            Property(t => t.Place_Of_Birth).HasMaxLength(1024);
            Property(t => t.Photo_URL).HasMaxLength(1024).IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            HasRequired(g => g.User).WithRequiredDependent(s => s.UserProfile);//.Map(m => m.MapKey("ID"));
            // Indexes
        }
    }
}
