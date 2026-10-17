namespace BMF.Shared.UI
{
    public partial class PageHeadBuilder : IPageHeadBuilder
    {
        #region Fields

        private string _breadcrumbBackLinkURL;
        private string _breadcrumbBackLinkText;

        private string _activeAdminMenuSystemName;

        #endregion

        #region Ctor

        #endregion

        #region Methods

        public virtual void SetBreadcrumbBackLink(string breadcrumbBackLinkURL, string breadcrumbBackLinkText)
        {
            _breadcrumbBackLinkURL = breadcrumbBackLinkURL;
            _breadcrumbBackLinkText = breadcrumbBackLinkText;
        }

        public virtual string GetBreadcrumbBackLinkURL()
        {
            return _breadcrumbBackLinkURL;
        }

        public virtual string GetBreadcrumbBackLinkText()
        {
            return _breadcrumbBackLinkText;
        }

        public virtual void SetActiveMenuItemSystemName(string systemName)
        {
            _activeAdminMenuSystemName = systemName;
        }

        public virtual string GetActiveMenuItemSystemName()
        {
            return _activeAdminMenuSystemName;
        }

        #endregion
    }
}
