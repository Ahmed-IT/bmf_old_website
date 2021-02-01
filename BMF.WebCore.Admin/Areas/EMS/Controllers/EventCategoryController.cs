using AMK.Plugin.BootstrapModal;
using AMK.Plugin.JQueryDataTable;
using BMF.Shared;
using BMF.Shared.Core.WebFramework;
using BMF.Shared.Entities.Events;
using BMF.Shared.Extensions;
using BMF.Shared.Models.KatrikFile;
using BMF.Shared.Services.Azure;
using BMF.Shared.Services.Data;
using BMF.Shared.Services.Events;
using BMF.Shared.Services.Security;
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

namespace BMF.WebCore.Admin.Areas.EMS.Controllers
{
    [Area(AreaNames.EMS)]
    [Authorize]
    public class EventCategoryController : BaseController
    {
        public static string _entityName => "Event Category";
        public static string _areaName => AreaNames.EMS;
        public static string _controllerName => M_EventCategory._controllerName;
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
        private readonly IRepository<M_EventCategory> _repository;
        private readonly IAzureStorageManager _azureStorageManager;
        private readonly IEventSeasonService _eventSeasonService;

        public EventCategoryController(IPermissionService permissionService,
            IRepository<M_EventCategory> repository,
            IAzureStorageManager azureStorageManager,
            IEventSeasonService eventSeasonService)
        {
            _permissionService = permissionService;
            _repository = repository;
            _azureStorageManager = azureStorageManager;
            _eventSeasonService = eventSeasonService;
        }

        #region Views

        // GET: EMS/Categorys
        public async Task<IActionResult> Index()
        {
            if (!_permissionService.Authorize(SystemOperationsProvider.EventMS.EMS_EventCategory_ViewAll.SystemName))
                return UnauthorizedView();

            return View();
        }

        #endregion

        #region Modal Place Holders

        // GET: EMS/Categorys/Add
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

        // GET: EMS/Categorys/Edit
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

        // GET: EMS/Categorys/EditImage
        public async Task<IActionResult> _EditImage()
        {
            try
            {
                var Modal = new ModalInformational($"Edit {_entityName} Image",
                    Url.Action("_FormEditImage", _controllerName, new { area = _areaName }),
                    Size: Size.Medium,
                    HighlightRecordOnShow: true,
                    HighlightRecordDatatableName: $"DT_{JQDT_Index}"
                    );
                return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalInformational, Modal);
            }
            catch (Exception LogError)
            {
                this.SetError(FixedTerms.Message_Error_RefreshPage, IsPartialView: true);
                return PartialView(AMK.Plugin.BootstrapModal.Settings.ViewPath_PartialView_ModalResponse, new ModalResponse() { AllowSubmit = false });
            }
        }

        // GET: EMS/Categorys/Delete
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

        // GET: EMS/Categorys
        public async Task<IActionResult> _IndexDT()
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.EventMS.EMS_EventCategory_ViewAll.SystemName))
                    return UnauthorizedPartialView();

                var jQDataTable = new JQDataTable(JQDT_Index, $"{_entityName} - List", Url.Action("_IndexT", _controllerName, new { area = _areaName }))
                {
                    columns = M_EventCategory.JQDT_Index()
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

        // GET: EMS/Categorys
        [HttpPost]
        public async Task<JsonResult> _IndexT([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest _DTParams)
        {
            try
            {

                if (!_permissionService.Authorize(SystemOperationsProvider.EventMS.EMS_EventCategory_ViewAll.SystemName))
                    return UnauthorizedDataTablesJson();

                #region // Load Raw Data
                var RawData = _repository.QTable.Where(w => !w.DeletedOnUtc.HasValue).Include(i => i.EventSeason).ToList();
                #endregion

                #region // Filter Data
                var FinalizedData = JQDataTable._DynamicFilter(_DTParams, M_EventCategory.JQDT_Index(), RawData.AsQueryable());
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

        // GET: EMS/Categorys/Add
        public async Task<IActionResult> _FormAdd()
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.EventMS.EMS_EventCategory_Add.SystemName))
                    return UnauthorizedModalView();

                M_EventCategory AddRecord = new M_EventCategory()
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

        // GET: EMS/Categorys/Edit
        public async Task<IActionResult> _FormEdit(long ID)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.EventMS.EMS_EventCategory_Edit.SystemName))
                    return UnauthorizedModalView();

                M_EventCategory ActualRecord = await _repository.QTable.FindAsync(ID);
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


        // GET: EMS/Categorys/EditImage
        public async Task<IActionResult> _FormEditImage(long ID)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.EventMS.EMS_EventCategory_Edit.SystemName))
                    return UnauthorizedModalView();

                M_EventCategory ActualRecord = await _repository.QTable.Where(w => w.ID == ID).Include(i => i.EventSeason).SingleOrDefaultAsync();
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

        // GET: EMS/Categorys/Delete
        public async Task<IActionResult> _FormDelete(long ID)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.EventMS.EMS_EventCategory_Delete.SystemName))
                    return UnauthorizedModalView();

                M_EventCategory ActualRecord = await _repository.QTable.Where(w => w.ID == ID).Include(i => i.EventSeason).SingleOrDefaultAsync();
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

        // POST: EMS/Categorys/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(M_EventCategory AddRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.EventMS.EMS_EventCategory_Add.SystemName))
                    return UnauthorizedModalView();

                ModelState.Clear();
                AddRecord.Image_URL = M_EventCategory._defaultImage;
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

        // POST: EMS/Categorys/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(M_EventCategory EditRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.EventMS.EMS_EventCategory_Edit.SystemName))
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
                    ModelState.Clear();
                    EditRecord.Image_URL = ActualRecord.Image_URL;
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

        // POST: EMS/Categorys/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(M_EventCategory DeleteRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.EventMS.EMS_EventCategory_Delete.SystemName))
                    return UnauthorizedModalView();

                try
                {
                    M_EventCategory ActualRecord = await _repository.QTable.Where(w => w.ID == DeleteRecord.ID).Include(i => i.EventSeason).SingleOrDefaultAsync();
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

        #region API - File Upload

        [HttpPost]
        public async Task<ActionResult> UploadImage(long ID, IFormFile ImageUpload)
        {
            try
            {
                if (ID == 0)
                {
                    return Json(new KatrikFileResponse() { error = "Failed to upload. Invalid selection." });
                }
                else
                {
                    if (ImageUpload == null || ImageUpload.Length == 0)
                    {
                        return Json(new KatrikFileResponse() { error = "Failed to upload. No uploaded image provided." });
                    }
                    else
                    {
                        M_EventCategory ActualRecord = await _repository.QTable.Where(w => w.ID == ID).SingleOrDefaultAsync();
                        if (ActualRecord == null)
                        {
                            return Json(new KatrikFileResponse() { error = FixedTerms.Message_Error_Navigation });
                        }
                        else if (ActualRecord.DeletedOnUtc.HasValue)
                        {
                            return Json(new KatrikFileResponse() { error = FixedTerms.Message_Error_ConcurencyDeleted });
                        }
                        else
                        {
                            DateTime CurrentTimestamp = DateTime.UtcNow;

                            using var memoryStream = new MemoryStream();
                            await ImageUpload.CopyToAsync(memoryStream);
                            memoryStream.Position = 0; // Reset memory stream to upload correctly

                            var uploadResult = _azureStorageManager.UploadFile(AzureStorageContainers.EventCategory, ImageUpload.FileName, memoryStream, out string imageURL, out string errorMessage);
                            if (!uploadResult)
                            {
                                return Json(new KatrikFileResponse() { error = "Failed to upload. Please try again later." });
                            }
                            else
                            {
                                ActualRecord.Image_URL = imageURL;
                                ActualRecord.CreatedOnUtc = CurrentTimestamp;
                                ActualRecord.CreatedBy = User.Identity.Name.ToString();
                                ActualRecord.UpdatedOnUtc = CurrentTimestamp;
                                ActualRecord.UpdatedBy = User.Identity.Name.ToString();

                                _repository.Entry(ActualRecord).State = EntityState.Modified;
                                await _repository.SaveChangesAsync();

                                return Json(new KatrikFileResponse()
                                {
                                    error = null,
                                    initialPreview = new List<string>() { $"{imageURL}" },
                                    initialPreviewConfig = new List<KatrikFilePreviewConfig>()
                                    {
                                        new KatrikFilePreviewConfig()
                                        {
                                            caption = "", //caption = $"{ActualRecord.Name} - {specSection.ToString()} - {AddRecord.ID}",
                                            filename = "", //filename = $"{ActualRecord.Name} - {specSection.ToString()} {AddRecord.ID}",
                                            downloadUrl = $"{imageURL}",
                                            url  = $"{Url.Action("DeleteImage", new { ActualRecord.ID })}",
                                            key = ActualRecord.ID,
                                        }
                                    },
                                    append = true
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception LogError)
            {
                return Json(new KatrikFileResponse() { error = "Failed to upload. Please contact system administrator." });
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteImage(long ID, long key)
        {
            try
            {
                if (ID == 0)
                {
                    return Json(new KatrikFileResponse() { error = "Failed to delete. Invalid selection." });
                }
                else
                {
                    M_EventCategory ActualRecord = await _repository.QTable.Where(w => w.ID == ID).SingleOrDefaultAsync();
                    if (ActualRecord == null)
                    {
                        return Json(new KatrikFileResponse() { error = FixedTerms.Message_Error_Navigation });
                    }
                    else if (ActualRecord.DeletedOnUtc.HasValue)
                    {
                        return Json(new KatrikFileResponse() { error = FixedTerms.Message_Error_ConcurencyDeleted });
                    }
                    else
                    {
                        if (key == 0)
                        {
                            return Json(new KatrikFileResponse() { error = "Failed to delete. Invalid image selected." });
                        }
                        else
                        {
                            DateTime CurrentTimestamp = DateTime.UtcNow;
                            var actualFileName = ActualRecord.Image_URL.Split('/').LastOrDefault();

                            ActualRecord.Image_URL = M_EventCategory._defaultImage;
                            ActualRecord.CreatedOnUtc = CurrentTimestamp;
                            ActualRecord.CreatedBy = User.Identity.Name.ToString();
                            ActualRecord.UpdatedOnUtc = CurrentTimestamp;
                            ActualRecord.UpdatedBy = User.Identity.Name.ToString();

                            _repository.Entry(ActualRecord).State = EntityState.Modified;

                            var _DeleteStatus = await _azureStorageManager.DeleteFileAsync(AzureStorageContainers.EventCategory, actualFileName);
                            if (_DeleteStatus)
                            {
                                await _repository.SaveChangesAsync();
                                return Json("");
                            }
                            else
                            {
                                return Json(new KatrikFileResponse() { error = "Failed to delete. Please try again later." });
                            }
                        }
                    }
                }
            }
            catch (Exception LogError)
            {
                return Json(new KatrikFileResponse() { error = "Failed to delete. Please contact system administrator." });
            }
        }

        #endregion

        #region Functions

        public void Set_SelectList(M_EventCategory DefineRecord)
        {
            var ID_EventSeason = _eventSeasonService.Get_SelectList();
            ViewBag.ID_EventSeason = new SelectList(ID_EventSeason, "Value", "Text", DefineRecord.ID_EventSeason);
        }

        #endregion
    }
}