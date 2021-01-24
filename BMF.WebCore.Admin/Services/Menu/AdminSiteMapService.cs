using BMF.Shared.Core;
using BMF.Shared.Entities.Location;
using BMF.Shared.Entities.Security;
using BMF.Shared.Services.Data;
using BMF.Shared.Services.Menu;
using BMF.Shared.Services.Security;
using BMF.WebCore.Admin.Services.Security;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                                RouteValues = new RouteValueDictionary{ {"area", AreaNames.Admin } },
                                ControllerName = AdminRole._controllerName,
                                ActionName = "Index",
                                IconClass = "fas fa-tags",
                                IsVisible = _authOperations.Contains(nameof(SystemOperationsProvider.Admin.SecurityAdminRole)),
                            },
                            new SiteMapNode()
                            {
                                SystemName = nameof(SystemOperationsProvider.Admin.SecurityAdminUser),
                                Title = "Admin Users",
                                RouteValues = new RouteValueDictionary{ {"area", AreaNames.Admin } },
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
                                RouteValues = new RouteValueDictionary{ {"area", AreaNames.Admin } },
                                ControllerName = Country._controllerName,
                                ActionName = "Index",
                                IconClass = "fas fa-flag",
                                IsVisible = _authOperations.Contains(nameof(SystemOperationsProvider.Admin.SystemSettingsCountry)),
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
