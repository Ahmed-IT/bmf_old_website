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
using System.Text;
using System.Threading.Tasks;

namespace BMF.AdminWeb.Core.Areas.Admin.Controllers
{
    [Area(AreaNames.Security)]
    [Authorize]
    public class AdminUserController : BaseController
    {
        public static string _entityName => "Admin User";
        public static string _areaName => AreaNames.Security;
        public static string _controllerName => AdminUser._controllerName;
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
        private readonly IRepository<AdminUser> _repository;
        private readonly IAdminRoleService _adminRoleService;

        public AdminUserController(IPermissionService permissionService,
            IRepository<AdminUser> repository,
            IAdminUserService adminUserService,
            IAdminRoleService adminRoleService)
        {
            _permissionService = permissionService;
            _repository = repository;
            _adminRoleService = adminRoleService;
        }

        #region Views

        // GET: Admin/SecurityAdminUser
        public async Task<IActionResult> Index()
        {
            if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminUser_ViewAll.SystemName))
                return UnauthorizedView();

            return View();
        }

        #endregion

        #region Modal Place Holders

        // GET: Admin/SecurityAdminUser/Add
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

        // GET: Admin/SecurityAdminUser/Edit
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

        // GET: Admin/SecurityAdminUser/EditPassword
        public async Task<IActionResult> _EditPassword()
        {
            try
            {
                var Modal = new ModalForm($"{_entityName} - Password",
                    Url.Action("EditPassword", _controllerName, new { area = _areaName }),
                    Url.Action("_FormEditPassword", _controllerName, new { area = _areaName }),
                    Submit.Edit,
                    Size.Small,
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

        // GET: Admin/SecurityAdminUser/EditRoles
        public async Task<IActionResult> _EditRoles()
        {
            try
            {
                var Modal = new ModalForm($"{_entityName} - Assigned Admin Roles",
                    Url.Action("EditRoles", _controllerName, new { area = _areaName }),
                    Url.Action("_FormEditRoles", _controllerName, new { area = _areaName }),
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

        // GET: Admin/SecurityAdminUser/Delete
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

        // GET: Admin/SecurityAdminUser
        public async Task<IActionResult> _IndexDT()
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminUser_ViewAll.SystemName))
                    return UnauthorizedPartialView();

                var jQDataTable = new JQDataTable(JQDT_Index, $"{_entityName} - List", Url.Action("_IndexT", _controllerName, new { area = _areaName }))
                {
                    columns = AdminUser.JQDT_Index()
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

        // GET: Admin/SecurityAdminUser
        [HttpPost]
        public async Task<JsonResult> _IndexT([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest _DTParams)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminUser_ViewAll.SystemName))
                    return UnauthorizedDataTablesJson();

                #region // Load Raw Data
                var RawData = _repository.QTable.Where(w => !w.DeletedOnUtc.HasValue).ToList();
                #endregion

                #region // Filter Data
                var FinalizedData = JQDataTable._DynamicFilter(_DTParams, AdminUser.JQDT_Index(), RawData.AsQueryable());
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

        // GET: Admin/SecurityAdminUser/Add
        public async Task<IActionResult> _FormAdd()
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminUser_Add.SystemName))
                    return UnauthorizedModalView();

                AdminUser AddRecord = new AdminUser()
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

        // GET: Admin/SecurityAdminUser/Edit
        public async Task<IActionResult> _FormEdit(long ID)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminUser_Edit.SystemName))
                    return UnauthorizedModalView();

                AdminUser ActualRecord = await _repository.QTable.FindAsync(ID);
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

        // GET: Admin/SecurityAdminUser/EditPassword
        public async Task<IActionResult> _FormEditPassword(long ID)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminUser_ResetPassword.SystemName))
                    return UnauthorizedModalView();

                AdminUser ActualRecord = await _repository.QTable.FindAsync(ID);
                if (ActualRecord == null || ActualRecord.DeletedOnUtc.HasValue)
                {
                    this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
                }
                else
                {
                    ActualRecord.Password = "";
                    return PartialView(ActualRecord);
                }
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
            }
            return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, new ModalResponse() { AllowSubmit = false });
        }

        // GET: Admin/SecurityAdminUser/EditRoles
        public async Task<IActionResult> _FormEditRoles(long ID)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminUser_ManageRoles.SystemName))
                    return UnauthorizedModalView();

                AdminUser ActualRecord = await _repository.QTable.FindAsync(ID);
                if (ActualRecord == null || ActualRecord.DeletedOnUtc.HasValue)
                {
                    this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
                }
                else
                {
                    _repository.Entry(ActualRecord)
                        .Collection(s => s.AssignedRoles)
                        .Query()
                        .Where(w => w.IsActive && !w.DeletedOnUtc.HasValue)
                        .Where(w => w.Role.IsActive && !w.Role.DeletedOnUtc.HasValue)
                        .Include(i => i.Role)
                        .ToList();

                    var RolesList = _adminRoleService
                        .GetAll(SelectedItems: ActualRecord.AssignedRoles.Where(w => w.IsActive && !w.DeletedOnUtc.HasValue).Select(s => s.Role.ID).ToList())
                        .Select(s => new AdminUserRole()
                        {
                            ID_Role = s.ID,
                            Role = s
                        }).ToList();

                    ActualRecord.AssignedRoles = RolesList;
                    return PartialView(ActualRecord);
                }
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
            }
            return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, new ModalResponse() { AllowSubmit = false });
        }

        // GET: Admin/SecurityAdminUser/Delete
        public async Task<IActionResult> _FormDelete(long ID)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminUser_Delete.SystemName))
                    return UnauthorizedModalView();

                AdminUser ActualRecord = await _repository.QTable.FindAsync(ID);
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

        // POST: Admin/SecurityAdminUser/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AdminUser AddRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminUser_Add.SystemName))
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
                        DateTime CurrentTimestamp = DateTime.UtcNow;
                        AddRecord.CreatedOnUtc = AddRecord.UpdatedOnUtc = CurrentTimestamp;
                        AddRecord.CreatedBy = AddRecord.UpdatedBy = User.Identity.Name.ToString();
                        #region Database Interaction
                        _repository.QTable.Add(AddRecord);
                        await _repository.SaveChangesAsync();
                        //_databaseContext.AdminUsers.Add(AddRecord);
                        //await _databaseContext.SaveChangesAsync();
                        #endregion

                        byte[] _Password = Encoding.ASCII.GetBytes(AddRecord.Password + AddRecord.ID);
                        _Password = new System.Security.Cryptography.SHA256Managed().ComputeHash(_Password);
                        AddRecord.Password = Convert.ToBase64String(_Password);
                        #region Database Interaction
                        _repository.Entry(AddRecord).State = EntityState.Modified;
                        await _repository.SaveChangesAsync();
                        //_databaseContext.Entry(AddRecord).State = EntityState.Modified;
                        //await _databaseContext.SaveChangesAsync();
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

        // POST: Admin/SecurityAdminUser/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminUser EditRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminUser_Edit.SystemName))
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
                    ModelState.Clear(); // Clear built-in validation
                    EditRecord.Password = ActualRecord.Password;
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
                            DateTime CurrentTimestamp = DateTime.UtcNow;
                            EditRecord.UpdatedOnUtc = CurrentTimestamp;
                            EditRecord.UpdatedBy = User.Identity.Name.ToString();
                            #region Database Interaction
                            EditRecord.AsDatabaseUpdate(ActualRecord);
                            _repository.Entry(ActualRecord).OriginalValues["RowVersion"] = EditRecord.RowVersion;
                            _repository.Entry(ActualRecord).State = EntityState.Modified;
                            await _repository.SaveChangesAsync();
                            //_databaseContext.Entry(ActualRecord).OriginalValues["RowVersion"] = EditRecord.RowVersion;
                            //_databaseContext.Entry(ActualRecord).State = EntityState.Modified;
                            //await _databaseContext.SaveChangesAsync();
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

        // POST: Admin/SecurityAdminUser/EditPassword/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPassword(AdminUser EditRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminUser_ResetPassword.SystemName))
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
                    ModelState.Clear(); // Clear built-in validation
                    ActualRecord.Password = EditRecord.Password;
                    EditRecord = ActualRecord;
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
                            DateTime CurrentTimestamp = DateTime.UtcNow;
                            EditRecord.UpdatedOnUtc = CurrentTimestamp;
                            EditRecord.UpdatedBy = User.Identity.Name.ToString();
                            byte[] _Password = Encoding.ASCII.GetBytes(ActualRecord.Password + ActualRecord.ID);
                            _Password = new System.Security.Cryptography.SHA256Managed().ComputeHash(_Password);
                            ActualRecord.Password = Convert.ToBase64String(_Password);
                            EditRecord.AsDatabaseUpdate(ActualRecord);
                            #region Database Interaction
                            _repository.Entry(ActualRecord).OriginalValues["RowVersion"] = EditRecord.RowVersion;
                            _repository.Entry(ActualRecord).State = EntityState.Modified;
                            await _repository.SaveChangesAsync();
                            //_databaseContext.Entry(ActualRecord).OriginalValues["RowVersion"] = EditRecord.RowVersion;
                            //_databaseContext.Entry(ActualRecord).State = EntityState.Modified;
                            //await _databaseContext.SaveChangesAsync();
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
                return PartialView("_FormEditPassword", EditRecord);
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
            }
        NoFurtherAction:
            return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, _Refresh._RefreshDTResponse());
        }

        // POST: Admin/SecurityAdminUser/EditRoles/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRoles(AdminUser EditRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminUser_ManageRoles.SystemName))
                    return UnauthorizedModalView();

                #region Verify Record
                var ActualRecord = await _repository.QTable.Where(w => w.ID == EditRecord.ID).Include(i => i.AssignedRoles).SingleOrDefaultAsync();
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
                        DateTime CurrentTimestamp = DateTime.UtcNow;
                        EditRecord.GetDatabaseValues(ActualRecord);
                        EditRecord.GetDefaults(ActualRecord);
                        List<long> _Roles = Request.Form.Keys.Contains("Roles") ? HttpContext.Request.Form["Roles"].ToList().Select(s => long.Parse(s)).ToList() : new List<long>();
                        #region Record Processing
                        EditRecord.UpdatedOnUtc = CurrentTimestamp;
                        EditRecord.UpdatedBy = User.Identity.Name.ToString();
                        _Roles.ForEach(set =>
                        {
                            EditRecord.AssignedRoles.Add(new AdminUserRole() { ID_Role = set });
                        });
                        EditRecord.DB_UpdateRoles = true;
                        EditRecord.AsDatabaseUpdate(ActualRecord);
                        #endregion
                        #region Database Interaction
                        _repository.Entry(ActualRecord).OriginalValues["RowVersion"] = EditRecord.RowVersion;
                        _repository.Entry(ActualRecord).State = EntityState.Modified;
                        await _repository.SaveChangesAsync();
                        //_databaseContext.Entry(ActualRecord).OriginalValues["RowVersion"] = EditRecord.RowVersion;
                        //_databaseContext.Entry(ActualRecord).State = EntityState.Modified;
                        //await _databaseContext.SaveChangesAsync();
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
                      .Collection(s => s.AssignedRoles)
                      .Query()
                      .Where(w => w.IsActive && !w.DeletedOnUtc.HasValue)
                      .Where(w => w.Role.IsActive && !w.Role.DeletedOnUtc.HasValue)
                      .Include(i => i.Role)
                      .ToList();

                var RolesList = _adminRoleService
                    .GetAll(SelectedItems: ActualRecord.AssignedRoles.Where(w => w.IsActive && !w.DeletedOnUtc.HasValue).Select(s => s.Role.ID).ToList())
                    .Select(s => new AdminUserRole()
                    {
                        ID_Role = s.ID,
                        Role = s
                    }).ToList();
                ActualRecord.AssignedRoles = RolesList;

                return PartialView("_FormEditRoles", EditRecord);
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
            }
        NoFurtherAction:
            return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, _Refresh._RefreshDTResponse());
        }

        // POST: Admin/SecurityAdminUser/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AdminUser DeleteRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Admin.SecurityAdminUser_Delete.SystemName))
                    return UnauthorizedModalView();

                try
                {
                    AdminUser ActualRecord = await _repository.QTable.FindAsync(DeleteRecord.ID);
                    if (ActualRecord == null || ActualRecord.DeletedOnUtc.HasValue)
                    {
                        this.SetError(FixedTerms.Message_Error_Navigation, IsPartialView: true);
                    }
                    else
                    {
                        DeleteRecord = ActualRecord;
                        DateTime CurrentTimestamp = DateTime.UtcNow;
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
                        //_databaseContext.Entry(ActualRecord).State = EntityState.Modified;
                        //await _databaseContext.SaveChangesAsync();
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
