using Microsoft.AspNetCore.Mvc.Rendering;
using BMF.Shared.UI;

namespace BMF.Shared.Extensions
{
    public static class LayoutExtensions
    {
        public static void SetBreadcrumbBackLink(this IHtmlHelper html, string breadcrumbBackLinkURL, string breadcrumbBackLinkText, IPageHeadBuilder pageHeadBuilder)
        {
            pageHeadBuilder.SetBreadcrumbBackLink(breadcrumbBackLinkURL, breadcrumbBackLinkText);
        }

        public static string GetBreadcrumbBackLinkURL(this IHtmlHelper html, IPageHeadBuilder pageHeadBuilder)
        {
            return pageHeadBuilder.GetBreadcrumbBackLinkURL();
        }

        public static string GetBreadcrumbBackLinkText(this IHtmlHelper html, IPageHeadBuilder pageHeadBuilder)
        {
            return pageHeadBuilder.GetBreadcrumbBackLinkText();
        }

        public static void SetActiveMenuItemSystemName(this IHtmlHelper html, string systemName, IPageHeadBuilder pageHeadBuilder)
        {
            pageHeadBuilder.SetActiveMenuItemSystemName(systemName);
        }

        public static string GetActiveMenuItemSystemName(this IHtmlHelper html, IPageHeadBuilder pageHeadBuilder)
        {
            return pageHeadBuilder.GetActiveMenuItemSystemName();
        }
    }
}
