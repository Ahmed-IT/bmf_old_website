namespace BMF.Shared.Models.AccessManagement
{
    public partial class S_RoleUser : BaseEntity
    {
        #region Foreign Keys
        public long ID_Role { get; set; }
        public long ID_User { get; set; }
        #endregion

        #region Relationships
        public virtual S_Role S_Role { get; set; }
        public virtual S_User S_User { get; set; }
        #endregion
    }
}
