using BMF.Shared.Models.AccessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMF.Web.Admin.DBMappings.AccessManagement
{
    public partial class S_User_Mapping : EFMapping<S_User>
    {
        public S_User_Mapping()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public override void MapCustomProperties()
        {
            // Attributes
            Property(t => t.Email).HasMaxLength(120).IsRequired();
            Property(t => t.FirstName).HasMaxLength(60).IsRequired();
            Property(t => t.LastName).HasMaxLength(60).IsRequired();
            Property(t => t.Username).HasMaxLength(60).IsRequired();
            Property(t => t.Password).HasMaxLength(120).IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            HasMany(g => g.AssignedRoles).WithRequired(s => s.S_User).HasForeignKey(s => s.ID_User);
            // Indexes
        }
    }
}
