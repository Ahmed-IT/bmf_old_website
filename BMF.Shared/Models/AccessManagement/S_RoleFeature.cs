namespace BMF.Shared.Models.AccessManagement
{
    public partial class S_RoleFeature : BaseEntity
    {
        #region Foreign Keys
        public long ID_Role { get; set; }
        public long ID_Feature { get; set; }
        #endregion

        #region Relationships
        public virtual S_Feature S_Feature { get; set; }
        public virtual S_Role S_Role { get; set; }
        #endregion
    }
}
