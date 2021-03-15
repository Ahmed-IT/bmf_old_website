using AMK.Plugin.BootstrapModal;
using AMK.Plugin.JQueryDataTable;
using BMF.Shared;
using BMF.Shared.Core.WebFramework;
using BMF.Shared.Entities.Location;
using BMF.Shared.Entities.UserManagement;
using BMF.Shared.Extensions;
using BMF.Shared.Services.Data;
using BMF.Shared.Services.Security;
using BMF.WebCore.Admin.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMF.WebCore.Admin.Areas.UserManagement.Controllers
{
    [Area(AreaNames.BmfMS)]
    [Authorize]
    public class UsersController : BaseController
    {
        public static string _entityName => "User";
        public static string _areaName => AreaNames.BmfMS;
        public static string _controllerName => M_UserAccount._controllerName;
        public static string JQDT_Index => $"dt{_controllerName}";
        public static class _Refresh
        {
            public static ModalResponse _RefreshDTResponse(int? SelfTimer = null)
            {
                return new ModalResponse(SelfTimer)
                {
                    Button_Targeted = new List<RefreshContainer>() {
                        new RefreshContainer($".btn-dtrefreshsoft-{JQDT_Index}", 0)
                    }
                };
            }
        }

        // TODO: // Eventually we want to use the Service Layer directly and not even the Repository // The service layer should utilize the repository
        private readonly IPermissionService _permissionService;
        private readonly IRepository<M_UserProfile> _repository;

        public UsersController(IPermissionService permissionService,
            IRepository<M_UserProfile> repository)
        {
            _permissionService = permissionService;
            _repository = repository;
        }

        public IActionResult Index()
        {
            var d = _repository.QTable.Select(s => new
            {
                account = new
                {
                    s.UserAccount.ID
                }
            }).FirstOrDefault();

            return View();
        }
    }
}
