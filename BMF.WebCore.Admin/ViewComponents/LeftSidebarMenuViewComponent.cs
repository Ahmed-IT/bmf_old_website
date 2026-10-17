using BMF.Shared.Entities.Security;
using BMF.Shared.Services.Menu;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMF.WebCore.Admin.ViewComponents
{
    [ViewComponent(Name = "LeftSidebarMenu")]
    public class LeftSidebarMenuViewComponent : ViewComponent
    {
        #region Service Injections

        private readonly ISiteMapService _siteMapService;

        public LeftSidebarMenuViewComponent(ISiteMapService siteMapService)
        {
            _siteMapService = siteMapService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }

        #region Functions

        private Task<List<SiteMapNode>> GetItemsAsync()
        {
            return Task.Run(() => _siteMapService.SiteMapCollection());
        }

        #endregion
    }
}
