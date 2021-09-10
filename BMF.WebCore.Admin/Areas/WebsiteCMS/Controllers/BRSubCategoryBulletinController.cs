using AMK.Plugin.BootstrapModal;
using AMK.Plugin.JQueryDataTable;
using BMF.Shared;
using BMF.Shared.Core.WebFramework;
using BMF.Shared.Entities.Website;
using BMF.Shared.Extensions;
using BMF.Shared.Models.KatrikFile;
using BMF.Shared.Services.Azure;
using BMF.Shared.Services.BMF.WebsiteCMS;
using BMF.Shared.Services.Data;
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

namespace BMF.WebCore.Admin.Areas.WebsiteCMS.Controllers
{
    [Area(AreaNames.WebsiteCMS)]
    [Authorize]
    public class BRSubCategoryBulletinController : BaseController
    {
        public static string _entityName => "Bulletin and Rules & regulations Sub-Category - Bulletin";
        public static string _areaName => AreaNames.WebsiteCMS;
        public static string _controllerName => M_BRSubCategoryBulletin._controllerName;
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
        private readonly IRepository<M_BRSubCategoryBulletin> _repository;
        private readonly IAzureStorageManager _azureStorageManager;
        private readonly IBRSubCategoryService _brSubCategoryService;

        public BRSubCategoryBulletinController(IPermissionService permissionService,
            IRepository<M_BRSubCategoryBulletin> repository,
            IAzureStorageManager azureStorageManager,
            IBRSubCategoryService brSubCategoryService)
        {
            _permissionService = permissionService;
            _repository = repository;
            _azureStorageManager = azureStorageManager;
            _brSubCategoryService = brSubCategoryService;
        }

        #region Views

        // GET: WebsiteCMS/BRSubCategoryBulletin
        public async Task<IActionResult> Index()
        {
            if (!_permissionService.Authorize(SystemOperationsProvider.Website_CMS.WebsiteCMS_BRSubCategoryBulletin_ViewAll.SystemName))
                return UnauthorizedView();

            return View();
        }

        #endregion

        #region Modal Place Holders

        // GET: WebsiteCMS/BRSubCategoryBulletin/Add
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

        // GET: WebsiteCMS/BRSubCategoryBulletin/Edit
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

        // GET: WebsiteCMS/BRSubCategoryBulletin/EditPDF
        public async Task<IActionResult> _EditPDF()
        {
            try
            {
                var Modal = new ModalInformational($"Edit {_entityName} PDF",
                    Url.Action("_FormEditPDF", _controllerName, new { area = _areaName }),
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

        // GET: WebsiteCMS/BRSubCategoryBulletin/Delete
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

        // GET: WebsiteCMS/BRSubCategoryBulletin
        public async Task<IActionResult> _IndexDT()
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Website_CMS.WebsiteCMS_BRSubCategoryBulletin_ViewAll.SystemName))
                    return UnauthorizedPartialView();

                var jQDataTable = new JQDataTable(JQDT_Index, $"{_entityName} - List", Url.Action("_IndexT", _controllerName, new { area = _areaName }))
                {
                    columns = M_BRSubCategoryBulletin.JQDT_Index()
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

        // GET: WebsiteCMS/BRSubCategoryBulletin
        [HttpPost]
        public async Task<JsonResult> _IndexT([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest _DTParams)
        {
            try
            {

                if (!_permissionService.Authorize(SystemOperationsProvider.Website_CMS.WebsiteCMS_BRSubCategoryBulletin_ViewAll.SystemName))
                    return UnauthorizedDataTablesJson();

                #region // Load Raw Data
                var RawData = _repository.QTable.Where(w => !w.DeletedOnUtc.HasValue).Include(i => i.SubCategory);
                #endregion

                #region // Filter Data
                var FinalizedData = JQDataTable._DynamicFilter(_DTParams, M_BRSubCategoryBulletin.JQDT_Index(), RawData.AsQueryable());
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

        // GET: WebsiteCMS/BRSubCategoryBulletin/Add
        public async Task<IActionResult> _FormAdd()
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Website_CMS.WebsiteCMS_BRSubCategoryBulletin_Add.SystemName))
                    return UnauthorizedModalView();

                M_BRSubCategoryBulletin AddRecord = new M_BRSubCategoryBulletin()
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

        // GET: WebsiteCMS/BRSubCategoryBulletin/Edit
        public async Task<IActionResult> _FormEdit(long ID)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Website_CMS.WebsiteCMS_BRSubCategoryBulletin_Edit.SystemName))
                    return UnauthorizedModalView();

                M_BRSubCategoryBulletin ActualRecord = await _repository.QTable.FindAsync(ID);
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

        // GET: WebsiteCMS/BRSubCategoryBulletin/EditPDF
        public async Task<IActionResult> _FormEditPDF(long ID)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Website_CMS.WebsiteCMS_BRSubCategoryBulletin_Edit.SystemName))
                    return UnauthorizedModalView();

                M_BRSubCategoryBulletin ActualRecord = await _repository.QTable.Where(w => w.ID == ID).Include(i => i.SubCategory).SingleOrDefaultAsync();
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

        // GET: WebsiteCMS/BRSubCategoryBulletin/Delete
        public async Task<IActionResult> _FormDelete(long ID)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Website_CMS.WebsiteCMS_BRSubCategoryBulletin_Delete.SystemName))
                    return UnauthorizedModalView();

                M_BRSubCategoryBulletin ActualRecord = await _repository.QTable.Where(w => w.ID == ID).Include(i => i.SubCategory).SingleOrDefaultAsync();
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

        // POST: WebsiteCMS/BRSubCategoryBulletin/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(M_BRSubCategoryBulletin AddRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Website_CMS.WebsiteCMS_BRSubCategoryBulletin_Add.SystemName))
                    return UnauthorizedModalView();

                ModelState.Clear();
                AddRecord.PDF_Url = M_BRSubCategoryBulletin._defaultPDF;
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

        // POST: WebsiteCMS/BRSubCategoryBulletin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(M_BRSubCategoryBulletin EditRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Website_CMS.WebsiteCMS_BRSubCategoryBulletin_Edit.SystemName))
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
                    EditRecord.PDF_Url = ActualRecord.PDF_Url;
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

        // POST: WebsiteCMS/BRSubCategoryBulletin/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(M_BRSubCategoryBulletin DeleteRecord)
        {
            try
            {
                if (!_permissionService.Authorize(SystemOperationsProvider.Website_CMS.WebsiteCMS_BRSubCategoryBulletin_Delete.SystemName))
                    return UnauthorizedModalView();

                try
                {
                    M_BRSubCategoryBulletin ActualRecord = await _repository.QTable.Where(w => w.ID == DeleteRecord.ID).Include(i => i.SubCategory).SingleOrDefaultAsync();
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
                        M_BRSubCategoryBulletin ActualRecord = await _repository.QTable.Where(w => w.ID == ID).SingleOrDefaultAsync();
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
                            DateTime CurrentTimestamp = DateTime.Now;

                            using var memoryStream = new MemoryStream();
                            await ImageUpload.CopyToAsync(memoryStream);
                            memoryStream.Position = 0; // Reset memory stream to upload correctly

                            var uploadResult = _azureStorageManager.UploadFile(AzureStorageContainers.BRSubCategoryBulletinPDF, ImageUpload.FileName, memoryStream, out string imageURL, out string errorMessage);
                            if (!uploadResult)
                            {
                                return Json(new KatrikFileResponse() { error = "Failed to upload. Please try again later." });
                            }
                            else
                            {
                                ActualRecord.PDF_Url = imageURL;
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
                    M_BRSubCategoryBulletin ActualRecord = await _repository.QTable.Where(w => w.ID == ID).SingleOrDefaultAsync();
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
                            DateTime CurrentTimestamp = DateTime.Now;
                            var actualFileName = ActualRecord.PDF_Url.Split('/').LastOrDefault();

                            ActualRecord.PDF_Url = M_BRSubCategoryBulletin._defaultPDF;
                            ActualRecord.CreatedOnUtc = CurrentTimestamp;
                            ActualRecord.CreatedBy = User.Identity.Name.ToString();
                            ActualRecord.UpdatedOnUtc = CurrentTimestamp;
                            ActualRecord.UpdatedBy = User.Identity.Name.ToString();

                            _repository.Entry(ActualRecord).State = EntityState.Modified;

                            var _DeleteStatus = await _azureStorageManager.DeleteFileAsync(AzureStorageContainers.BRSubCategoryBulletinPDF, actualFileName);
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

        public void Set_SelectList(M_BRSubCategoryBulletin DefineRecord)
        {
            var ID_SubCategory = _brSubCategoryService.Get_SelectList();
            ViewBag.ID_SubCategory = new SelectList(ID_SubCategory, "Value", "Text", DefineRecord.ID_SubCategory);
        }

        #endregion
    }
}
