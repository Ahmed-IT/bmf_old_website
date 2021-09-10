using AMK.Plugin.BootstrapModal;
using AMK.Plugin.JQueryDataTable;
using BMF.Shared;
using BMF.Shared.Core.WebFramework;
using BMF.Shared.Entities.Security;
using BMF.Shared.Extensions;
using BMF.Shared.Services.Data;
using BMF.Shared.Services.Security;
using BMF.WebCore.Admin;
using BMF.WebCore.Admin.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMF.AdminWeb.Core.Areas.Admin.Controllers
{
    [Area(AreaNames.Security)]
    [Authorize]
    public class AdminRoleController : BaseController
    {
        public static string _entityName => "Admin Role";
        public static string _areaName => AreaNames.Security;
        public static string _controllerName => AdminRole._controllerName;
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
        private readonly IRepository<AdminRole> _repository;
        private readonly ISystemOperationService _systemOperationService;
        private readonly IAdminUserService _adminUserService;

        public AdminRoleController(IPermissionService permissionService,
            IRepository<AdminRole> repository,
            IAdminUserService adminUserService,
            ISystemOperationService systemOperationService)
        {
            _permissionService = permissionService;
            _repository = repository;
            _adminUserService = adminUserService;
            _systemOperationService = systemOperationService;
        }

        #region Views

        // GET: Admin/SecurityAdminRole
        public async Task<IActionResult> Index()
        {
            if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminRole_ViewAll.SystemName))
                return UnauthorizedView();

            return View();
        }

        #endregion

        #region Modal Place Holders

        // GET: Admin/SecurityAdminRole/Add
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

        // GET: Admin/SecurityAdminRole/Edit
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

        // GET: Admin/SecurityAdminRole/EditOperations
        public async Task<IActionResult> _EditOperations()
        {
            try
            {
                var Modal = new ModalForm($"{_entityName} - Assigned System Operations",
                    Url.Action("EditOperations", _controllerName, new { area = _areaName }),
                    Url.Action("_FormEditOperations", _controllerName, new { area = _areaName }),
                    Submit.Edit,
                    Size.Large,
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

        // GET: Admin/SecurityAdminRole/EditUsers
        public async Task<IActionResult> _EditUsers()
        {
            try
            {
                var Modal = new ModalForm($"{_entityName} - Assigned Admin Users",
                    Url.Action("EditUsers", _controllerName, new { area = _areaName }),
                    Url.Action("_FormEditUsers", _controllerName, new { area = _areaName }),
                    Submit.Edit,
                    Size.Large,
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

        // GET: Admin/SecurityAdminRole/Delete
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

        // GET: Admin/SecurityAdminRole
        public async Task<IActionResult> _IndexDT()
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminRole_ViewAll.SystemName))
                    return UnauthorizedPartialView();

                var jQDataTable = new JQDataTable(JQDT_Index, $"{_entityName} - List", Url.Action("_IndexT", _controllerName, new { area = _areaName }))
                {
                    columns = AdminRole.JQDT_Index()
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

        // GET: Admin/SecurityAdminRole
        [HttpPost]
        public async Task<JsonResult> _IndexT([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest _DTParams)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminRole_ViewAll.SystemName))
                    return UnauthorizedDataTablesJson();

                #region // Load Raw Data
                var RawData = _repository.QTable.Where(w => !w.DeletedOnUtc.HasValue);
                #endregion

                #region // Filter Data
                var FinalizedData = JQDataTable._DynamicFilter(_DTParams, AdminRole.JQDT_Index(), RawData.AsQueryable());
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
                return Json(new DataTablesResponse(LogError.Message));
            }
        }

        #endregion

        #region Partial Views

        // GET: Admin/SecurityAdminRole/Add
        public async Task<IActionResult> _FormAdd()
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminRole_Add.SystemName))
                    return UnauthorizedModalView();

                AdminRole AddRecord = new AdminRole()
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

        // GET: Admin/SecurityAdminRole/Edit
        public async Task<IActionResult> _FormEdit(long ID)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminRole_Edit.SystemName))
                    return UnauthorizedModalView();

                AdminRole ActualRecord = await _repository.QTable.FindAsync(ID);
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

        // GET: Admin/SecurityAdminRole/EditOperations
        public async Task<IActionResult> _FormEditOperations(long ID)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminRole_ManageOperations.SystemName))
                    return UnauthorizedModalView();

                AdminRole ActualRecord = await _repository.QTable.FindAsync(ID);
                if (ActualRecord == null || ActualRecord.DeletedOnUtc.HasValue)
                {
                    this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
                }
                else
                {
                    _repository.Entry(ActualRecord)
                        .Collection(s => s.AssignedOperations)
                        .Query()
                        .Where(w => w.IsActive && !w.DeletedOnUtc.HasValue)
                        .Include(i => i.Operation)
                        .ToList();

                    var OperationsList = _systemOperationService
                        .GetAll_Nested(SelectedItems: ActualRecord.AssignedOperations.Where(w => w.IsActive && !w.DeletedOnUtc.HasValue).Select(s => s.Operation.ID).ToList())
                        .Select(s => new AdminRoleOperation()
                        {
                            ID_Operation = s.ID,
                            Operation = s
                        }).ToList();

                    ActualRecord.AssignedOperations = OperationsList;
                    return PartialView(ActualRecord);
                }
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
            }
            return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, new ModalResponse() { AllowSubmit = false });
        }

        // GET: Admin/SecurityAdminRole/EditUsers
        public async Task<IActionResult> _FormEditUsers(long ID)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminRole_ManageUsers.SystemName))
                    return UnauthorizedModalView();

                AdminRole ActualRecord = await _repository.QTable.FindAsync(ID);
                if (ActualRecord == null || ActualRecord.DeletedOnUtc.HasValue)
                {
                    this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
                }
                else
                {
                    _repository.Entry(ActualRecord)
                        .Collection(s => s.AssignedUsers)
                        .Query()
                        .Where(w => w.IsActive && !w.DeletedOnUtc.HasValue)
                        .Where(w => w.User.IsActive && !w.User.DeletedOnUtc.HasValue)
                        .Include(i => i.User)
                        .ToList();

                    var UsersList = _adminUserService
                        .GetAll(SelectedItems: ActualRecord.AssignedUsers.Where(w => w.IsActive && !w.DeletedOnUtc.HasValue).Select(s => s.User.ID).ToList())
                        .Select(s => new AdminUserRole()
                        {
                            ID_User = s.ID,
                            User = s
                        }).ToList();

                    ActualRecord.AssignedUsers = UsersList;
                    return PartialView(ActualRecord);
                }
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
            }
            return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, new ModalResponse() { AllowSubmit = false });
        }

        // GET: Admin/SecurityAdminRole/Delete
        public async Task<IActionResult> _FormDelete(long ID)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminRole_Delete.SystemName))
                    return UnauthorizedModalView();

                AdminRole ActualRecord = await _repository.QTable.FindAsync(ID);
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

        // POST: Admin/SecurityAdminRole/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AdminRole AddRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminRole_Add.SystemName))
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
                return PartialView("_FormAdd", AddRecord);
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
            }
            return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, _Refresh._RefreshDTResponse());
        }

        // POST: Admin/SecurityAdminRole/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminRole EditRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminRole_Edit.SystemName))
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
                    EditRecord.IsIAMRelated = ActualRecord.IsIAMRelated;
                    #region Validation
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

        // POST: Admin/SecurityAdminRole/EditOperations/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOperations(AdminRole EditRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminRole_ManageOperations.SystemName))
                    return UnauthorizedModalView();

                #region Verify Record
                var ActualRecord = await _repository.QTable.Where(w => w.ID == EditRecord.ID).Include(i => i.AssignedOperations).SingleOrDefaultAsync();
                if (ActualRecord == null || ActualRecord.DeletedOnUtc.HasValue)
                {
                    this.SetError(FixedTerms.Message_Error_ConcurencyDeleted, IsPartialView: true);
                    goto NoFurtherAction;
                }
                #endregion
                else
                {
                    try
                    {
                        DateTime CurrentTimestamp = DateTime.Now;
                        EditRecord.GetDatabaseValues(ActualRecord);
                        EditRecord.GetDefaults(ActualRecord);
                        List<long> _Operations = Request.Form.Keys.Contains("Operations") ? HttpContext.Request.Form["Operations"].ToString().Split(",").ToList().Select(s => long.Parse(s)).ToList() : new List<long>();
                        #region Record Processing
                        EditRecord.UpdatedOnUtc = CurrentTimestamp;
                        EditRecord.UpdatedBy = User.Identity.Name.ToString();
                        _Operations.ForEach(set =>
                        {
                            EditRecord.AssignedOperations.Add(new AdminRoleOperation() { ID_Operation = set });
                        });
                        EditRecord.DB_UpdateOperations = true;
                        EditRecord.AsDatabaseUpdate(ActualRecord);
                        #endregion
                        #region Database Interaction
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
                    //    byte[] _RowVersion;
                    //    var ActualEntry = ConcurrencyError.Entries.Single();
                    //    if (ConcurrencyHandler<S_Role>.ValidateConcurrency<S_Role>(ActualEntry, ModelState, out _RowVersion))
                    //    {
                    //        SetWarning(FixedTerms.Title_Error_ConcurencyUpdated, FixedTerms.Message_Error_ConcurencyUpdated);
                    //        EditRecord.RowVersion = _RowVersion;
                    //    }
                    //    else
                    //    {
                    //        SetError(FixedTerms.Message_Error_ConcurencyDeleted, DirectInjection: false);
                    //        return RedirectToAction("Index");
                    //    }
                    //}
                    #endregion
                    catch (Exception LogError)
                    {
                        this.SetError(FixedTerms.Message_Error_GeneralSave, IsPartialView: true);
                    }
                }

                var OperationsList = _systemOperationService
                    .GetAll_Nested(SelectedItems: ActualRecord.AssignedOperations.Where(w => w.IsActive && !w.DeletedOnUtc.HasValue).Select(s => s.Operation.ID).ToList())
                    .Select(s => new AdminRoleOperation()
                    {
                        ID_Operation = s.ID,
                        Operation = s
                    }).ToList();
                EditRecord.AssignedOperations = OperationsList;
                return PartialView("_FormEditOperations", EditRecord);
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
            }
        NoFurtherAction:
            return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, _Refresh._RefreshDTResponse());
        }

        // POST: Admin/SecurityAdminRole/EditUsers/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUsers(AdminRole EditRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminRole_ManageUsers.SystemName))
                    return UnauthorizedModalView();

                #region Verify Record
                var ActualRecord = await _repository.QTable.Where(w => w.ID == EditRecord.ID).Include(i => i.AssignedUsers).SingleOrDefaultAsync();
                if (ActualRecord == null || ActualRecord.DeletedOnUtc.HasValue)
                {
                    this.SetError(FixedTerms.Message_Error_ConcurencyDeleted, IsPartialView: true);
                    goto NoFurtherAction;
                }
                #endregion
                else
                {
                    try
                    {
                        DateTime CurrentTimestamp = DateTime.Now;
                        EditRecord.GetDatabaseValues(ActualRecord);
                        EditRecord.GetDefaults(ActualRecord);
                        List<long> _Users = Request.Form.Keys.Contains("Users") ? HttpContext.Request.Form["Users"].ToList().Select(s => long.Parse(s)).ToList() : new List<long>();
                        #region Record Processing
                        EditRecord.UpdatedOnUtc = CurrentTimestamp;
                        EditRecord.UpdatedBy = User.Identity.Name.ToString();
                        _Users.ForEach(set =>
                        {
                            EditRecord.AssignedUsers.Add(new AdminUserRole() { ID_User = set });
                        });
                        EditRecord.DB_UpdateUsers = true;
                        EditRecord.AsDatabaseUpdate(ActualRecord);
                        #endregion
                        #region Database Interaction
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
                    //    byte[] _RowVersion;
                    //    var ActualEntry = ConcurrencyError.Entries.Single();
                    //    if (ConcurrencyHandler<S_Role>.ValidateConcurrency<S_Role>(ActualEntry, ModelState, out _RowVersion))
                    //    {
                    //        SetWarning(FixedTerms.Title_Error_ConcurencyUpdated, FixedTerms.Message_Error_ConcurencyUpdated);
                    //        EditRecord.RowVersion = _RowVersion;
                    //    }
                    //    else
                    //    {
                    //        SetError(FixedTerms.Message_Error_ConcurencyDeleted, DirectInjection: false);
                    //        return RedirectToAction("Index");
                    //    }
                    //}
                    #endregion
                    catch (Exception LogError)
                    {
                        this.SetError(FixedTerms.Message_Error_GeneralSave, IsPartialView: true);
                    }
                }

                _repository.Entry(ActualRecord)
                  .Collection(s => s.AssignedUsers)
                  .Query()
                  .Where(w => w.IsActive && !w.DeletedOnUtc.HasValue)
                  .Where(w => w.User.IsActive && !w.User.DeletedOnUtc.HasValue)
                  .Include(i => i.User)
                  .ToList();

                var UsersList = _adminUserService
                    .GetAll(SelectedItems: ActualRecord.AssignedUsers.Where(w => w.IsActive && !w.DeletedOnUtc.HasValue).Select(s => s.User.ID).ToList())
                    .Select(s => new AdminUserRole()
                    {
                        ID_User = s.ID,
                        User = s
                    }).ToList();
                ActualRecord.AssignedUsers = UsersList;

                return PartialView("_FormEditUsers", EditRecord);
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
            }
        NoFurtherAction:
            return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, _Refresh._RefreshDTResponse());
        }

        // POST: Admin/SecurityAdminRole/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AdminRole DeleteRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminRole_Delete.SystemName))
                    return UnauthorizedModalView();

                try
                {
                    AdminRole ActualRecord = await _repository.QTable.FindAsync(DeleteRecord.ID);
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
