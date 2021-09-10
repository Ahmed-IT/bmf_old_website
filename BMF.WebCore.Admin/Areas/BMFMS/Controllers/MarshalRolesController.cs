using AMK.Plugin.BootstrapModal;
using AMK.Plugin.JQueryDataTable;
using BMF.Shared;
using BMF.Shared.Core.WebFramework;
using BMF.Shared.Entities.Location;
using BMF.Shared.Entities.UserManagement;
using BMF.Shared.Extensions;
using BMF.Shared.Models.KatrikFile;
using BMF.Shared.Services.Azure;
using BMF.Shared.Services.Data;
using BMF.Shared.Services.Security;
using BMF.WebCore.Admin.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class MarshalRolesController : BaseController
    {
        public static string _entityName => "Marshal Roles";
        public static string _areaName => AreaNames.BmfMS;
        public static string _controllerName => D_MarshalRole._controllerName;
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
        private readonly IRepository<D_MarshalRole> _repository;

        public MarshalRolesController(IPermissionService permissionService,
            IRepository<D_MarshalRole> repository)
        {
            _permissionService = permissionService;
            _repository = repository;
        }


        #region Views

        // GET: BMFMS/MarshalRoles
        public async Task<IActionResult> Index()
        {
            if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_MarshalRole_ViewAll.SystemName))
                return UnauthorizedView();

            return View();
        }

        #endregion

        #region Modal Place Holders

        // GET: BMFMS/MarshalRoles/Add
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

        // GET: BMFMS/MarshalRoles/Edit
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

        // GET: BMFMS/MarshalRoles/Delete
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

        // GET: BMFMS/MarshalRoles
        public async Task<IActionResult> _IndexDT()
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_MarshalRole_ViewAll.SystemName))
                    return UnauthorizedPartialView();

                var jQDataTable = new JQDataTable(JQDT_Index, $"{_entityName} - List", Url.Action("_IndexT", _controllerName, new { area = _areaName }))
                {
                    columns = D_MarshalRole.JQDT_Index()
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

        // GET: BMFMS/MarshalRoles
        [HttpPost]
        public async Task<JsonResult> _IndexT([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest _DTParams)
        {
            try
            {

                if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_MarshalRole_ViewAll.SystemName))
                    return UnauthorizedDataTablesJson();

                #region // Load Raw Data
                var RawData = _repository.QTable.Where(w => !w.DeletedOnUtc.HasValue);
                #endregion

                #region // Filter Data
                var FinalizedData = JQDataTable._DynamicFilter(_DTParams, D_MarshalRole.JQDT_Index(), RawData.AsQueryable());
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

        // GET: BMFMS/MarshalRoles/Add
        public async Task<IActionResult> _FormAdd()
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_MarshalRole_Add.SystemName))
                    return UnauthorizedModalView();

                D_MarshalRole AddRecord = new D_MarshalRole()
                {
                    IsActive = true,
                };
                return PartialView(AddRecord);
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
            }
            return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, new ModalResponse() { AllowSubmit = false });
        }

        // GET: BMFMS/MarshalRoles/Edit
        public async Task<IActionResult> _FormEdit(long ID)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_MarshalRole_Edit.SystemName))
                    return UnauthorizedModalView();

                D_MarshalRole ActualRecord = await _repository.QTable.FindAsync(ID);
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

        // GET: BMFMS/MarshalRoles/Delete
        public async Task<IActionResult> _FormDelete(long ID)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_MarshalRole_Delete.SystemName))
                    return UnauthorizedModalView();

                D_MarshalRole ActualRecord = await _repository.QTable.FindAsync(ID);
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

        // POST: BMFMS/MarshalRoles/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(D_MarshalRole AddRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_MarshalRole_Add.SystemName))
                    return UnauthorizedModalView();

                #region Validation
                ModelState.Clear();
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
                return PartialView("_FormAdd", AddRecord);
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
            }
            return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, _Refresh._RefreshDTResponse());
        }

        // POST: BMFMS/MarshalRoles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(D_MarshalRole EditRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_MarshalRole_Edit.SystemName))
                    return UnauthorizedModalView();

                var ActualRecord = await _repository.QTable.FindAsync(EditRecord.ID);
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
                    ModelState.Clear();
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
                return PartialView("_FormEdit", EditRecord);
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
            }
            NoFurtherAction:
            return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, _Refresh._RefreshDTResponse());
        }

        // POST: BMFMS/MarshalRoles/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(D_MarshalRole DeleteRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.BMFMS.BmfMS_MarshalRole_Delete.SystemName))
                    return UnauthorizedModalView();

                try
                {
                    D_MarshalRole ActualRecord = await _repository.QTable.FindAsync(DeleteRecord.ID);
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
    }
}
