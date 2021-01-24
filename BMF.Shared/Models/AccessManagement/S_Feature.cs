using System.Collections.Generic;

namespace BMF.Shared.Models.AccessManagement
{
    public partial class S_Feature : BaseEntity
    {
        #region Foreign Keys
        public long? ID_ParentFeature { get; set; }
        #endregion

        #region Properties
        public string ID_Path { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string RightIcons_URL { get; set; }
        public string RightIcons_Fixed { get; set; }
        public string LeftIcons_Fixed { get; set; }
        public byte Level { get; set; }
        public int Order { get; set; }
        public bool Visibility { get; set; }
        #endregion

        #region Relationships
        public virtual S_Feature ParentFeature { get; set; }
        public virtual ICollection<S_Feature> Children { get; set; }
        public virtual ICollection<S_RoleFeature> AssignedRoles { get; set; }
        #endregion
    }
}
