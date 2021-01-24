using System.Collections.Generic;

namespace BMF.Shared.Models.AccessManagement
{
    public partial class S_Role : BaseEntity
    {
        #region Properties
        public string Name { get; set; }
        public string Description { get; set; }
        #endregion

        #region Relationships
        public virtual ICollection<S_RoleFeature> AssignedFeatures { get; set; }
        public virtual ICollection<S_RoleUser> AssignedUsers { get; set; }
        #endregion
    }
}
