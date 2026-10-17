namespace BMF.Shared.Entities.Security
{
    public partial class LoggedInUserViewModel
    {
        #region Properties

        public string FullName { get; set; }

        #endregion

        #region Ctor

        public LoggedInUserViewModel(AdminUser adminUser)
        {
            FullName = adminUser.FullName;
        }

        #endregion
    }
}
