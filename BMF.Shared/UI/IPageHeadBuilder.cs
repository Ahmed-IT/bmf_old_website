namespace BMF.Shared.UI
{
    public partial interface IPageHeadBuilder
    {
        void SetBreadcrumbBackLink(string breadcrumbBackLinkURL, string breadcrumbBackLinkText);
        string GetBreadcrumbBackLinkURL();
        string GetBreadcrumbBackLinkText();

        void SetActiveMenuItemSystemName(string systemName);
        string GetActiveMenuItemSystemName();
    }
}
