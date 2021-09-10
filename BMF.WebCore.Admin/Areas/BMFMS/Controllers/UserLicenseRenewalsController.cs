using AMK.Plugin.BootstrapModal;
using AMK.Plugin.JQueryDataTable;
using BMF.Shared;
using BMF.Shared.Core.Helpers;
using BMF.Shared.Core.WebFramework;
using BMF.Shared.Entities.UserManagement;
using BMF.Shared.Extensions;
using BMF.Shared.Models.KatrikFile;
using BMF.Shared.Services.Azure;
using BMF.Shared.Services.Data;
using BMF.Shared.Services.Location;
using BMF.Shared.Services.Security;
using BMF.Shared.Services.UserManagement;
using BMF.WebCore.Admin.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BMF.WebCore.Admin.Areas.BMFMS.Controllers
{
    [Area(AreaNames.BmfMS)]
    [Authorize]
    public class UserLicenseRenewalsController : BaseController
    {
        public static string _entityName => "User License Renewal";
        public static string _areaName => AreaNames.BmfMS;
        public static string _controllerName => M_UserLicenseRenewal._controllerName;
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
        private readonly IUserLicenseService _userLicenseService;
        private readonly IRepository<M_UserLicenseRenewal> _repository;

        public UserLicenseRenewalsController(IPermissionService permissionService,
            IUserLicenseService userLicenseService,
            IRepository<M_UserLicenseRenewal> repository)
        {
            _permissionService = permissionService;
            _userLicenseService = userLicenseService;
            _repository = repository;
        }

        #region Views

        // GET: BMFMS/UserLicenseRenewals/All
        public async Task<IActionResult> All()
        {
            if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_UserLicenseRenewal.SystemName))
                return UnauthorizedView();

            return View();
        }

        // GET: BMFMS/UserLicenseRenewals/Pending
        public async Task<IActionResult> Pending()
        {
            if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_UserLicenseRenewal.SystemName))
                return UnauthorizedView();

            return View();
        }

        #endregion

        #region Modal Place Holders

        // GET: BMFMS/UserLicenseRenewals/Add
        public async Task<IActionResult> _Add()
        {
            try
            {
                var Modal = new ModalForm(_entityName,
                    Url.Action("Add", _controllerName, new { area = _areaName }),
                    Url.Action("_FormAdd", _controllerName, new { area = _areaName }),
                    Submit: Submit.Add,
                    Size: Size.Medium
                    );
                return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalForm, Modal);
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_RefreshPage, IsPartialView: true);
                return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, new ModalResponse() { AllowSubmit = false });
            }
        }

        // GET: BMFMS/UserLicenseRenewals/Edit
        public async Task<IActionResult> _Edit()
        {
            try
            {
                var Modal = new ModalForm(_entityName,
                    Url.Action("Edit", _controllerName, new { area = _areaName }),
                    Url.Action("_FormEdit", _controllerName, new { area = _areaName }),
                    Submit: Submit.Edit,
                    Size: Size.Medium,
                    HighlightRecordOnShow: true,
                    HighlightRecordDatatableName: $"DT_{JQDT_Index}"
                    );
                return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalForm, Modal);
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_RefreshPage, IsPartialView: true);
                return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, new ModalResponse() { AllowSubmit = false });
            }
        }

        // GET: BMFMS/UserLicenseRenewals/Delete
        public async Task<IActionResult> _Delete()
        {
            try
            {
                var Modal = new ModalForm(_entityName,
                    Url.Action("Delete", _controllerName, new { area = _areaName }),
                    Url.Action("_FormDelete", _controllerName, new { area = _areaName }),
                    Submit: Submit.Delete,
                    Size: Size.Medium,
                    HighlightRecordOnShow: true,
                    HighlightRecordDatatableName: $"DT_{JQDT_Index}"
                    );
                return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalForm, Modal);
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_RefreshPage, IsPartialView: true);
                return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, new ModalResponse() { AllowSubmit = false });
            }
        }

        #endregion

        #region JQuery Datatable Place Holders

        // GET: BMFMS/UserLicenseRenewals/All
        public async Task<IActionResult> _AllDT()
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_UserLicenseRenewal_ViewAll.SystemName))
                    return UnauthorizedPartialView();

                var jQDataTable = new JQDataTable(JQDT_Index, $"{_entityName} - All", Url.Action("_AllT", _controllerName, new { area = _areaName }))
                {
                    columns = M_UserLicenseRenewal.JQDT_Index()
                };
                return PartialView(AMK.Plugin.JQueryDataTable.Settings.ViewPath_PartialView_DTPlaceholder, jQDataTable);
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_RefreshPage, IsPartialView: true);
                return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, new ModalResponse() { AllowSubmit = false });
            }
        }

        // GET: BMFMS/UserLicenseRenewals/Pending
        public async Task<IActionResult> _PendingDT()
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_UserLicenseRenewal_ViewAll.SystemName))
                    return UnauthorizedPartialView();

                var jQDataTable = new JQDataTable(JQDT_Index, $"{_entityName} - Pending", Url.Action("_PendingT", _controllerName, new { area = _areaName }))
                {
                    columns = M_UserLicenseRenewal.JQDT_Index()
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

        // GET: BMFMS/UserLicenseRenewals
        [HttpPost]
        public async Task<JsonResult> _AllT([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest _DTParams)
        {
            try
            {

                if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_UserLicenseRenewal_ViewAll.SystemName))
                    return UnauthorizedDataTablesJson();

                #region // Load Raw Data
                var RawData = _repository.QTable.Where(w => !w.DeletedOnUtc.HasValue).Include(i => i.UserLicense).Include(i => i.UserLicense.UserProfile).Include(i => i.UserLicense.LicenseType);
                #endregion

                #region // Filter Data
                var FinalizedData = JQDataTable._DynamicFilter(_DTParams, M_UserLicenseRenewal.JQDT_Index(), RawData.AsQueryable());
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

        // GET: BMFMS/UserLicenseRenewals
        [HttpPost]
        public async Task<JsonResult> _PendingT([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest _DTParams)
        {
            try
            {

                if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_UserLicenseRenewal_ViewAll.SystemName))
                    return UnauthorizedDataTablesJson();

                #region // Load Raw Data
                var RawData = _repository.QTable.Where(w => !w.DeletedOnUtc.HasValue && w.ApprovalStatus == Enum_ApprovalStatus.PendingApproval).Include(i => i.UserLicense).Include(i => i.UserLicense.UserProfile).Include(i => i.UserLicense.LicenseType);
                #endregion

                #region // Filter Data
                var FinalizedData = JQDataTable._DynamicFilter(_DTParams, M_UserLicenseRenewal.JQDT_Index(), RawData.AsQueryable());
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

        #region Partial Views

        // GET: BMFMS/UserLicenseRenewals/Add
        public async Task<IActionResult> _FormAdd()
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_UserLicenseRenewal_Add.SystemName))
                    return UnauthorizedModalView();

                M_UserLicenseRenewal AddRecord = new M_UserLicenseRenewal()
                {
                    IsActive = true,
                };
                Set_SelectList(AddRecord);
                return PartialView(AddRecord);
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
            }
            return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, new ModalResponse() { AllowSubmit = false });
        }

        // GET: BMFMS/UserLicenseRenewals/Edit
        public async Task<IActionResult> _FormEdit(long ID)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_UserLicenseRenewal_Edit.SystemName))
                    return UnauthorizedModalView();

                M_UserLicenseRenewal ActualRecord = await _repository.QTable.Where(w => w.ID == ID).Include(i => i.UserLicense).Include(i => i.UserLicense.UserProfile).Include(i => i.UserLicense.LicenseType).SingleOrDefaultAsync();
                if (ActualRecord == null || ActualRecord.DeletedOnUtc.HasValue)
                {
                    this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
                }
                else
                {
                    Set_SelectList(ActualRecord);
                    return PartialView(ActualRecord);
                }
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
            }
            return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, new ModalResponse() { AllowSubmit = false });
        }

        // GET: BMFMS/UserLicenseRenewals/Delete
        public async Task<IActionResult> _FormDelete(long ID)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_UserLicenseRenewal_Delete.SystemName))
                    return UnauthorizedModalView();

                M_UserLicenseRenewal ActualRecord = await _repository.QTable.Where(w => w.ID == ID).Include(i => i.UserLicense).Include(i => i.UserLicense.UserProfile).Include(i => i.UserLicense.LicenseType).SingleOrDefaultAsync();
                if (ActualRecord == null || ActualRecord.DeletedOnUtc.HasValue)
                {
                    this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
                }
                else
                {
                    return PartialView(ActualRecord);
                }
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
            }
            return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, new ModalResponse() { AllowSubmit = false });
        }

        #endregion

        #region API

        // POST: BMFMS/UserLicenseRenewals/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(M_UserLicenseRenewal AddRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_UserLicenseRenewal_Add.SystemName))
                    return UnauthorizedModalView();

                #region Validation
                AddRecord.Validate(ModelState, databaseContext: _repository.QTable);
                if (!ModelState.IsValid)
                {
                    this.SetError(FixedTerms.Message_Error_GeneralAdd, IsPartialView: true);
                }
                #endregion
                else
                {
                    try
                    {
                        DateTime CurrentTimestamp = DateTime.Now;
                        AddRecord.CreatedOnUtc = AddRecord.UpdatedOnUtc = CurrentTimestamp;
                        AddRecord.CreatedBy = AddRecord.UpdatedBy = User.Identity.Name.ToString();
                        #region Database Interaction
                        _repository.QTable.Add(AddRecord);
                        await _repository.SaveChangesAsync();
                        #endregion
                        this.SetSuccess(FixedTerms.Title_Success_Add, DirectInjection: true, IsPartialView: true);
                        return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, _Refresh._RefreshDTResponse(3000));
                    }
                    catch (Exception LogError)
                    {
                        this.SetError(FixedTerms.Message_Error_GeneralAdd, IsPartialView: true);
                    }
                }
                Set_SelectList(AddRecord);
                return PartialView("_FormAdd", AddRecord);
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
            }
            return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, _Refresh._RefreshDTResponse());
        }

        // POST: BMFMS/UserLicenseRenewals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(M_UserLicenseRenewal EditRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_UserLicenseRenewal_Edit.SystemName))
                    return UnauthorizedModalView();

                var ActualRecord = await _repository.QTable.Where(w => w.ID == EditRecord.ID).Include(i => i.UserLicense).Include(i => i.UserLicense.UserProfile).Include(i => i.UserLicense.LicenseType).SingleOrDefaultAsync();
                #region Verification
                if (ActualRecord == null || ActualRecord.DeletedOnUtc.HasValue)
                {
                    this.SetError(FixedTerms.Message_Error_ConcurencyDeleted, IsPartialView: true);
                    goto NoFurtherAction;
                }
                #endregion
                else
                {
                    #region Validation
                    EditRecord.ID_UserLicense = ActualRecord.ID_UserLicense;
                    EditRecord.CPR_URL = ActualRecord.CPR_URL;
                    EditRecord.Parent_CPR_URL = ActualRecord.Parent_CPR_URL;
                    EditRecord.MedicalForm_URL = ActualRecord.MedicalForm_URL;
                    EditRecord.NoObjectionLetter_URL = ActualRecord.NoObjectionLetter_URL;
                    EditRecord.Validate(ModelState, databaseContext: _repository.QTable);
                    if (!ModelState.IsValid)
                    {
                        this.SetError(FixedTerms.Message_Error_GeneralSave, IsPartialView: true);
                    }
                    #endregion
                    else
                    {
                        try
                        {
                            DateTime CurrentTimestamp = DateTime.Now;
                            EditRecord.UpdatedOnUtc = CurrentTimestamp;
                            EditRecord.UpdatedBy = User.Identity.Name.ToString();
                            #region Database Interaction
                            EditRecord.AsDatabaseUpdate(ActualRecord);
                            _repository.Entry(ActualRecord).OriginalValues["RowVersion"] = EditRecord.RowVersion;
                            _repository.Entry(ActualRecord).State = EntityState.Modified;
                            await _repository.SaveChangesAsync();
                            #endregion
                            this.SetSuccess(FixedTerms.Title_Success_Save, DirectInjection: true, IsPartialView: true);
                            goto NoFurtherAction;
                        }
                        #region catch (DbUpdateConcurrencyException ConcurrencyError)
                        //catch (DbUpdateConcurrencyException ConcurrencyError)
                        //{
                        //    byte[] RowVersion;
                        //    var ActualEntry = ConcurrencyError.Entries.Single();
                        //    if (ConcurrencyHandler<S_Role>.ValidateConcurrency<S_Role>(ActualEntry, ModelState, out RowVersion))
                        //    {
                        //        this.SetWarning(FixedTerms.Title_Error_ConcurencyUpdated, FixedTerms.Message_Error_ConcurencyUpdated, IsPartialView: true);
                        //        EditRecord.RowVersion = RowVersion;
                        //        ModelState.Remove("RowVersion");
                        //    }
                        //    else
                        //    {
                        //        this.SetError(FixedTerms.Message_Error_ConcurencyDeleted, IsPartialView: true);
                        //        goto NoFurtherAction;
                        //    }
                        //}
                        #endregion
                        catch (Exception LogError)
                        {
                            this.SetError(FixedTerms.Message_Error_GeneralSave, IsPartialView: true);
                        }
                    }
                }
                Set_SelectList(EditRecord);
                return PartialView("_FormEdit", EditRecord);
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
            }
            NoFurtherAction:
            return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, _Refresh._RefreshDTResponse());
        }

        // POST: BMFMS/UserLicenseRenewals/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(M_UserLicenseRenewal DeleteRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_UserLicenseRenewal_Delete.SystemName))
                    return UnauthorizedModalView();

                try
                {
                    M_UserLicenseRenewal ActualRecord = await _repository.QTable.Where(w => w.ID == DeleteRecord.ID).Include(i => i.UserLicense).Include(i => i.UserLicense.UserProfile).Include(i => i.UserLicense.LicenseType).SingleOrDefaultAsync();
                    if (ActualRecord == null || ActualRecord.DeletedOnUtc.HasValue)
                    {
                        this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
                    }
                    else
                    {
                        DeleteRecord = ActualRecord;
                        DateTime CurrentTimestamp = DateTime.Now;
                        if (!DeleteRecord.RowVersion.SequenceEqual(ActualRecord.RowVersion))
                        {
                            throw new DbUpdateConcurrencyException();
                        }
                        #region Record Processing
                        ActualRecord.DeletedOnUtc = ActualRecord.UpdatedOnUtc = CurrentTimestamp;
                        ActualRecord.DeletedBy = ActualRecord.UpdatedBy = User.Identity.Name.ToString();
                        #endregion
                        #region Database Interaction              
                        _repository.Entry(ActualRecord).State = EntityState.Modified;
                        await _repository.SaveChangesAsync();
                        #endregion
                        this.SetSuccess(FixedTerms.Title_Success_Delete, DirectInjection: true, IsPartialView: true);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    this.SetWarning(FixedTerms.Title_Error_ConcurencyUpdated, FixedTerms.Message_Error_ConcurencyDelete, IsPartialView: true);
                    return PartialView("_FormDelete", DeleteRecord);
                }
                catch (Exception LogError)
                {
                    this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
                    return PartialView("_FormDelete", DeleteRecord);
                }
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
            }
            return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, _Refresh._RefreshDTResponse());
        }

        #endregion

        #region Functions

        public void Set_SelectList(M_UserLicenseRenewal DefineRecord)
        {
            var ID_UserLicense = _userLicenseService.Get_SelectList();
            ViewBag.ID_UserLicense = new SelectList(ID_UserLicense, "Value", "Text", DefineRecord.ID_UserLicense);

            ViewBag.ApprovalStatus = EnumHelper<Enum_ApprovalStatus>.GetSelectList(DefineRecord.ApprovalStatus);
        }

        #endregion
    }
}
