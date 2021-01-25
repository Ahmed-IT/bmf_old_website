using BMF.Shared.Core;
using BMF.Shared.Entities.Location;
using BMF.Shared.Entities.Security;
using BMF.Shared.Entities.Website;
using BMF.Shared.Services.Data;
using BMF.Shared.Services.Menu;
using BMF.Shared.Services.Security;
using BMF.WebCore.Admin.Services.Security;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMF.WebCore.Admin.Services.Menu
{
    public partial class AdminSiteMapService : ISiteMapService
    {
        private readonly IWorkContext _workContext;
        private readonly IRepository<AdminUser> _adminUserRepository;
        private readonly IAdminUserService _adminUserService;
        private readonly IAdminRoleService _adminRoleService;

        public AdminSiteMapService(IWorkContext workContext,
            IRepository<AdminUser> adminUserRepository,
            IAdminUserService adminUserService,
            IAdminRoleService adminRoleService)
        {
            _workContext = workContext;
            _adminUserRepository = adminUserRepository;
            _adminUserService = adminUserService;
            _adminRoleService = adminRoleService;
        }

        public List<SiteMapNode> SiteMapCollection()
        {
            if (_workContext == null)
            {
                return new List<SiteMapNode>();
            }
            else if (_workContext.CurrentAdminUser != null)
            {
                //TODO: remove this into a permission service to be re-usable and have this just pull the sitemap from one place which already checks permissions
                var _authOperations = _adminUserRepository.QTable.Where(w => w.ID == _workContext.CurrentAdminUser.ID)
                        .SelectMany(s => s.AssignedRoles.Select(s => s.Role))
                        .SelectMany(s => s.AssignedOperations.Select(s => s.Operation.SystemName)).Distinct();
                return new List<SiteMapNode>() {
                    new SiteMapNode()
                    {
                        SystemName = nameof(SystemOperationsProvider.Admin.Security),
                        Title = "Security",
                        IconClass = "fas fa-users-cog",
                        IsVisible = _authOperations.Contains(nameof(SystemOperationsProvider.Admin.Security)),
                        ChildNodes = new List<SiteMapNode>()
                        {
                            new SiteMapNode()
                            {
                                SystemName = nameof(SystemOperationsProvider.Admin.SecurityAdminRole),
                                Title = "Admin Roles",
                                RouteValues = new RouteValueDictionary{ {"area", AreaNames.Security } },
                                ControllerName = AdminRole._controllerName,
                                ActionName = "Index",
                                IconClass = "fas fa-tags",
                                IsVisible = _authOperations.Contains(nameof(SystemOperationsProvider.Admin.SecurityAdminRole)),
                            },
                            new SiteMapNode()
                            {
                                SystemName = nameof(SystemOperationsProvider.Admin.SecurityAdminUser),
                                Title = "Admin Users",
                                RouteValues = new RouteValueDictionary{ {"area", AreaNames.Security } },
                                ControllerName = AdminUser._controllerName,
                                ActionName = "Index",
                                IconClass = "fas fa-users",
                                IsVisible = _authOperations.Contains(nameof(SystemOperationsProvider.Admin.SecurityAdminUser)),
                            },
                        }
                    },

                    new SiteMapNode()
                    {
                        SystemName = nameof(SystemOperationsProvider.Admin.SystemSettings),
                        Title = "System Settings",
                        IconClass = "fas fa-cogs",
                        IsVisible = _authOperations.Contains(nameof(SystemOperationsProvider.Admin.SystemSettings)),
                        ChildNodes = new List<SiteMapNode>()
                        {
                            new SiteMapNode()
                            {
                                SystemName = nameof(SystemOperationsProvider.Admin.SystemSettingsCountry),
                                Title = "Countries",
                                RouteValues = new RouteValueDictionary{ {"area", AreaNames.Location } },
                                ControllerName = Country._controllerName,
                                ActionName = "Index",
                                IconClass = "fas fa-flag",
                                IsVisible = _authOperations.Contains(nameof(SystemOperationsProvider.Admin.SystemSettingsCountry)),
                            }
                        }
                    },

                    new SiteMapNode()
                    {
                        SystemName = nameof(SystemOperationsProvider.Website_CMS.WebsiteCMS),
                        Title = "Website CMS",
                        IconClass = "fas fa-globe",
                        IsVisible = _authOperations.Contains(nameof(SystemOperationsProvider.Website_CMS.WebsiteCMS)),
                        ChildNodes = new List<SiteMapNode>()
                        {
                            new SiteMapNode()
                            {
                                SystemName = nameof(SystemOperationsProvider.Website_CMS.WebsiteCMS_News),
                                Title = "News",
                                RouteValues = new RouteValueDictionary{ {"area", AreaNames.WebsiteCMS } },
                                ControllerName = M_News._controllerName,
                                ActionName = "Index",
                                IconClass = "fas fa-newspaper",
                                IsVisible = _authOperations.Contains(nameof(SystemOperationsProvider.Website_CMS.WebsiteCMS_News)),
                            },
                            new SiteMapNode()
                            {
                                SystemName = nameof(SystemOperationsProvider.Website_CMS.WebsiteCMS_Club),
                                Title = "Clubs",
                                RouteValues = new RouteValueDictionary{ {"area", AreaNames.WebsiteCMS } },
                                ControllerName = M_Club._controllerName,
                                ActionName = "Index",
                                IconClass = "fas fa-users",
                                IsVisible = _authOperations.Contains(nameof(SystemOperationsProvider.Website_CMS.WebsiteCMS_Club)),
                            },
                            new SiteMapNode()
                            {
                                SystemName = nameof(SystemOperationsProvider.Website_CMS.WebsiteCMS_QuickLinks),
                                Title = "Quick Links",
                                RouteValues = new RouteValueDictionary{ {"area", AreaNames.WebsiteCMS } },
                                ControllerName = M_QuickLinks._controllerName,
                                ActionName = "Index",
                                IconClass = "fas fa-link",
                                IsVisible = _authOperations.Contains(nameof(SystemOperationsProvider.Website_CMS.WebsiteCMS_QuickLinks)),
                            },
                            new SiteMapNode()
                            {
                                SystemName = nameof(SystemOperationsProvider.Website_CMS.WebsiteCMS_GalleryCategory),
                                Title = "Gallery Categories",
                                RouteValues = new RouteValueDictionary{ {"area", AreaNames.WebsiteCMS } },
                                ControllerName = M_GalleryCategory._controllerName,
                                ActionName = "Index",
                                IconClass = "fas fa-images",
                                IsVisible = _authOperations.Contains(nameof(SystemOperationsProvider.Website_CMS.WebsiteCMS_GalleryCategory)),
                            }
                        }
                    },

                };
            }
            else
            {
                return new List<SiteMapNode>();
            }
        }
    }
}
