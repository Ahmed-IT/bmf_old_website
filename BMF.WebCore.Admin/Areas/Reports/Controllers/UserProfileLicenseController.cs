using AMK.Plugin.BootstrapModal;
using AMK.Plugin.JQueryDataTable;
using BMF.Shared;
using BMF.Shared.Core.WebFramework;
using BMF.Shared.Entities.Location;
using BMF.Shared.Entities.UserManagement;
using BMF.Shared.Extensions;
using BMF.Shared.Services.Data;
using BMF.Shared.Services.Security;
using BMF.WebCore.Admin.Areas.Reports.ViewModels;
using BMF.WebCore.Admin.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMF.WebCore.Admin.Areas.Reports.Controllers
{
    [Area(AreaNames.Reports)]
    [Authorize]
    public class UserProfileLicenseController : BaseController
    {
        public static string _areaName => AreaNames.Reports;
        public static string _controllerName => M_UserProfile._controllerName_Report_ProfileLicense;
        public static string JQDT_Index => $"dt{_controllerName}";

        // TODO: // Eventually we want to use the Service Layer directly and not even the Repository // The service layer should utilize the repository
        private readonly IPermissionService _permissionService;
        private readonly BMFDbContext _dbContext;

        public UserProfileLicenseController(IPermissionService permissionService,
            BMFDbContext dbContext)
        {
            _permissionService = permissionService;
            _dbContext = dbContext;
        }

        #region Views

        // GET: Reports/UserProfileLicense
        public async Task<IActionResult> Index()
        {
            if (!_permissionService.Authorize(SystemOperationsProvider.Report.Reports_UserProfiles.SystemName))
                return UnauthorizedView();

            return View();
        }

        #endregion

        #region JQuery Datatable Place Holders

        // GET: Reports/UserProfileLicense
        public async Task<IActionResult> _IndexDT()
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Report.Reports_UserProfiles.SystemName))
                    return UnauthorizedPartialView();

                var jQDataTable = new JQDataTable(JQDT_Index, $"{_controllerName}-List", Url.Action("_IndexT", _controllerName, new { area = _areaName }))
                {
                    columns = SingleUserProfileLicenseVM.JQDT_Index()
                };
                return PartialView(AMK.Plugin.JQueryDataTable.Settings.ViewPath_PartialView_DTPlaceholder, jQDataTable);
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_RefreshPage, IsPartialView: true);
                return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, new ModalResponse() { AllowSubmit = false });
            }
        }

        #endregion

        #region JQuery APIs

        // GET: Reports/UserProfileLicense
        [HttpPost]
        public async Task<JsonResult> _IndexT([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest _DTParams)
        {
            try
            {

                if (!_permissionService.Authorize(SystemOperationsProvider.Report.Reports_UserProfiles.SystemName))
                    return UnauthorizedDataTablesJson();

                #region // Load Raw Data
                var RawData = returnExpression();
                #endregion

                #region // Filter Data
                var FinalizedData = JQDataTable._DynamicFilter(_DTParams, SingleUserProfileLicenseVM.JQDT_Index(), RawData);
                #endregion

                #region // Order Data
                FinalizedData = JQDataTable._DynamicSort(_DTParams, FinalizedData);
                #endregion

                var DTResponse = new DataTablesResponse(_DTParams.Draw, FinalizedData.Skip(_DTParams.Start).Take(_DTParams.Length), FinalizedData.Count(), RawData.Count());
                var _DTResponse = Json(DTResponse);
                return _DTResponse;
            }
            catch (Exception LogError)
            {
                return Json(new DataTablesResponse(LogError.Message + LogError.StackTrace));
            }
        }

        #endregion

        #region Functions

        public IQueryable<SingleUserProfileLicenseVM> returnExpression()
        {
            return (from UProfile in _dbContext.Set<M_UserProfile>()
                    join ULicense in _dbContext.Set<M_UserLicense>() on UProfile.ID equals ULicense.ID_UserProfile
                    where UProfile.IsActive
                    && !UProfile.DeletedOnUtc.HasValue
                    && !ULicense.DeletedOnUtc.HasValue
                    select new SingleUserProfileLicenseVM()
                    {
                        ID = UProfile.ID,
                        CPR = UProfile.CPR,
                        Email_Address = UProfile.UserAccount.Email_Address,
                        FullName = UProfile.FullName,
                        Nationality = UProfile.Nationality.Demonym,
                        DateOfBirth = UProfile.DateOfBirth,
                        Display_DateOfBirth = UProfile.DateOfBirth.Day + "-" + UProfile.DateOfBirth.Month+ "-" + UProfile.DateOfBirth.Year,
                        Mobile_Number = UProfile.Mobile_Number,
                        Full_Address = UProfile.Full_Address,
                        Blood_Group = UProfile.Blood_Group,
                        Place_Of_Birth = UProfile.Place_Of_Birth,
                        EmergencyContact_MobileNumber = UProfile.EmergencyContact_MobileNumber,
                        EmergencyContact_Name = UProfile.EmergencyContact_Name,
                        Parent_CPR = UProfile.Parent_CPR,
                        Parent_FullName = UProfile.Parent_FullName,
                        License_Number = ULicense.License_Number,
                        Expire_Date = ULicense.Expire_Date,
                        Display_Expire_Date = ULicense.Expire_Date.Day + "-" + ULicense.Expire_Date.Month+ "-" + ULicense.Expire_Date.Year,
                        LicenseType = ULicense.LicenseType.Title,
                        MarshalRole = ULicense.MarshalRole.Name,
                    });
        }

        #endregion
    }
}
