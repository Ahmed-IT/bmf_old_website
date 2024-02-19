using BMF_WebSite.Models;
using BMF_WebSite.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using System.Web.Services.Description;

namespace BMF_WebSite.Controllers
{
    public class ServicesController : Controller
    {
        BMFEntities1 db = new BMFEntities1();

        // GET: Services
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CdpApplication()
        {
            return View();
        }

        public ActionResult CompetitionLicense()
        {
            ClsLicForm lic = new ClsLicForm();

            ViewBag.LicenseCategory = Helper.GetLicenseCategory();
            ViewBag.Countries = Helper.GetCountries();
            ViewBag.BloodGroup = Helper.GetBloodGroup();
            ViewBag.LicenseType = Helper.GetLicenseType();

            return View(lic);
        }

        [HttpPost]
        public async Task<ActionResult> CompetitionLicense(ClsLicForm data)
        {
            try
            {
                var supportedTypes = new[] { "png", "PNG", "jpeg", "JPEG", "jpg", "JPG", "pdf", "PDF" };

                if (data.IsUnder18 == true)
                {
                    if (string.IsNullOrEmpty(data.ParentFullName))
                    {
                        ModelState.AddModelError("ParentFullName", "Please ensure to enter a parent name");
                    }

                    if (string.IsNullOrEmpty(data.MobileNumber))
                    {
                        ModelState.AddModelError("ParentCPR", "Please ensure to enter a parent CPR number");
                    }

                    if (data.ParentCPRFile == null)
                    {
                        ModelState.AddModelError("ParentCPRFile", "Please upload parent CPR");
                    }
                    else
                    {

                        var fileExt2 = System.IO.Path.GetExtension(data.ParentCPRFile.FileName).Substring(1);
                        if (!supportedTypes.Contains(fileExt2))
                        {
                            ModelState.AddModelError("ParentCPRFile", "Please use only jpg,jpeg,png,pdf");
                        }
                    }
                }

                if (Helper.CalculateAge(data.DateOfBirth) < 18 && data.IsUnder18 == false)
                {
                    ModelState.AddModelError("IsUnder18", "You are under 18 please check the box and fill parent detail");
                }

                if (Helper.CalculateAge(data.DateOfBirth) > 18 && data.IsUnder18 == true)
                {
                    ModelState.AddModelError("IsUnder18", "You are over 18 please uncheck the box and clear parent detail if entered");
                }

                var errors = ModelState.Values.SelectMany(v => v.Errors);

                var fileExt = System.IO.Path.GetExtension(data.CPRFile.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    ModelState.AddModelError("CPRFile", "Please use only jpg,jpeg,png,pdf");
                }
                //else
                //{
                //    if(data.PhotoFile.ContentLength > 2048)
                //    {
                //        ModelState.AddModelError("PhotoFile", "File must be less than 1MB");
                //    }    
                //}

                var PhotofileExt = System.IO.Path.GetExtension(data.Photo.FileName).Substring(1);
                if (!supportedTypes.Contains(PhotofileExt))
                {
                    ModelState.AddModelError("Photo", "Please use only jpg,jpeg,png,pdf");
                }

                DateTime temp;
                if (!DateTime.TryParse(data.DateOfBirth.ToString(), out temp))
                {
                    ModelState.AddModelError("DateOfBirth", "Please enter a valid date");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        if (data.LicenseRequestType == "New")
                        {
                            //Check if Profile Exist and has a license
                            var CheckProfileExist = db.M_UserProfile.Where(a => a.CPR == data.CPR
                            && a.IsActive == true && a.DeletedOnUtc == null).FirstOrDefault();

                            if (CheckProfileExist == null)
                            {
                                //Create New Profile

                                if (data.Photo != null)
                                {
                                    var fileName = Path.GetFileName(data.Photo.FileName);
                                    StorageManager storageManager = new StorageManager();

                                    data.Photo_Url = await storageManager.UploadPhotoAsync(Helper.AzureStorageContainers.UserProfile, fileName, data.Photo);
                                }

                                if (data.CPRFile != null)
                                {
                                    var fileName = Path.GetFileName(data.CPRFile.FileName);
                                    StorageManager storageManager = new StorageManager();

                                    data.CPR_Url = await storageManager.UploadPhotoAsync(Helper.AzureStorageContainers.LicenseRenewal, fileName, data.CPRFile);
                                }

                                if (data.MedicalFile != null)
                                {
                                    var fileName = Path.GetFileName(data.MedicalFile.FileName);
                                    StorageManager storageManager = new StorageManager();

                                    data.Medical_Url = await storageManager.UploadPhotoAsync(Helper.AzureStorageContainers.LicenseRenewal, fileName, data.MedicalFile);
                                }

                                if (data.IsUnder18 == false)
                                {
                                    var profile = db.M_UserProfile.Add(new M_UserProfile
                                    {
                                        Full_Address = "House#: " + data.HouseNumber + " Road#: " + data.RoadNumber
                                    + " Block#: " + data.BlockNumber + " City: " + data.City,
                                        IsActive = true,
                                        Blood_Group = data.BloodGroup,
                                        CPR = data.CPR,
                                        DateOfBirth = data.DateOfBirth,
                                        FullName = data.FirstName + " " + data.MiddleName + " " + data.LastName,
                                        CreatedOnUtc = DateTime.Now,
                                        ID_Nationality = data.CountryId,
                                        Place_Of_Birth = data.PlaceOfBirth,
                                        Mobile_Number = data.MobileNumber,
                                        EmergencyContact_Name = data.EmergencyName,
                                        EmergencyContact_MobileNumber = data.EmergencyNumber,
                                        Photo_URL = data.Photo_Url
                                    });

                                    await db.SaveChangesAsync();

                                    //Create License Record

                                    var lic = db.M_UserLicense.Add(new M_UserLicense
                                    {
                                        License_Number = "",
                                        IsActive = true,
                                        CreatedOnUtc = DateTime.Now,
                                        ID_LicenseType = data.LicenseCategoryId,
                                        ID_UserProfile = profile.ID,
                                    });

                                    await db.SaveChangesAsync();

                                    //Create License History

                                    var LicHistory = db.M_UserLicenseRenewal.Add(new M_UserLicenseRenewal
                                    {
                                        ApprovalStatus = 0,
                                        CPR_URL = data.CPR_Url,
                                        IsActive = true,
                                        CreatedOnUtc = DateTime.Now,
                                        ID_UserLicense = lic.ID,
                                        MedicalForm_URL = data.Medical_Url,
                                    });

                                    await db.SaveChangesAsync();
                                }
                                else
                                {
                                    var profile = db.M_UserProfile.Add(new M_UserProfile
                                    {
                                        Full_Address = "House#: " + data.HouseNumber + " Road#: " + data.RoadNumber
                                    + " Block#: " + data.BlockNumber + " City: " + data.City,
                                        IsActive = true,
                                        Blood_Group = data.BloodGroup,
                                        CPR = data.CPR,
                                        DateOfBirth = data.DateOfBirth,
                                        FullName = data.FirstName + " " + data.MiddleName + " " + data.LastName,
                                        CreatedOnUtc = DateTime.Now,
                                        ID_Nationality = data.CountryId,
                                        Place_Of_Birth = data.PlaceOfBirth,
                                        Mobile_Number = data.MobileNumber,
                                        EmergencyContact_Name = data.EmergencyName,
                                        EmergencyContact_MobileNumber = data.EmergencyNumber,
                                        Photo_URL = data.Photo_Url,
                                        Parent_CPR = data.ParentCPR,
                                        Parent_FullName = data.ParentFullName
                                    });

                                    await db.SaveChangesAsync();

                                    //Create License Record

                                    var lic = db.M_UserLicense.Add(new M_UserLicense
                                    {
                                        License_Number = "",
                                        IsActive = true,
                                        CreatedOnUtc = DateTime.Now,
                                        ID_LicenseType = data.LicenseCategoryId,
                                        ID_UserProfile = profile.ID,
                                    });

                                    await db.SaveChangesAsync();

                                    //Create License History

                                    var LicHistory = db.M_UserLicenseRenewal.Add(new M_UserLicenseRenewal
                                    {
                                        ApprovalStatus = 0,
                                        CPR_URL = data.CPR_Url,
                                        IsActive = true,
                                        CreatedOnUtc = DateTime.Now,
                                        ID_UserLicense = lic.ID,
                                        MedicalForm_URL = data.Medical_Url,
                                        Parent_CPR_URL = data.ParentCPR_Url,
                                    });

                                    await db.SaveChangesAsync();
                                }

                                string message = EmailHelper.LicenseEmailMessage(data.FirstName + " " + data.LastName);
                                string messageBMF = EmailHelper.LicenseEmailMessageToBMF(new ClsLicForm
                                {
                                    FirstName = data.FirstName,
                                    LastName = data.LastName,
                                    CPR = data.CPR
                                });

                                EmailHelper.SendMail(data.EmailAddress,"", "License Registration Form", message);
                                EmailHelper.SendMail("info@bmf.com.bh", "alaa@bmf.com.bh", "License Registration Form - for " + data.FirstName + " " + data.LastName, messageBMF);

                                return RedirectToAction("LicenseSubmitted", "Services");
                            }
                            else
                            {
                                var checkLicenseExist = db.M_UserProfile.Where(a => a.CPR == data.CPR
                                        && a.IsActive == true && a.DeletedOnUtc == null &&
                                        a.M_UserLicense.Where(b => b.ID_LicenseType == data.LicenseCategoryId).FirstOrDefault() != null).FirstOrDefault();

                                if (checkLicenseExist == null)
                                {
                                    //Add New License

                                    if (data.Photo != null)
                                    {
                                        var fileName = Path.GetFileName(data.Photo.FileName);
                                        StorageManager storageManager = new StorageManager();

                                        data.Photo_Url = await storageManager.UploadPhotoAsync(Helper.AzureStorageContainers.UserProfile, fileName, data.Photo);
                                    }

                                    if (data.CPRFile != null)
                                    {
                                        var fileName = Path.GetFileName(data.CPRFile.FileName);
                                        StorageManager storageManager = new StorageManager();

                                        data.CPR_Url = await storageManager.UploadPhotoAsync(Helper.AzureStorageContainers.LicenseRenewal, fileName, data.CPRFile);
                                    }

                                    if (data.MedicalFile != null)
                                    {
                                        var fileName = Path.GetFileName(data.MedicalFile.FileName);
                                        StorageManager storageManager = new StorageManager();

                                        data.Medical_Url = await storageManager.UploadPhotoAsync(Helper.AzureStorageContainers.LicenseRenewal, fileName, data.MedicalFile);
                                    }

                                    CheckProfileExist.Photo_URL = data.Photo_Url;

                                    await db.SaveChangesAsync();

                                    var lic = db.M_UserLicense.Add(new M_UserLicense
                                    {
                                        License_Number = "",
                                        IsActive = true,
                                        CreatedOnUtc = DateTime.Now,
                                        ID_LicenseType = data.LicenseCategoryId,
                                        ID_UserProfile = CheckProfileExist.ID,
                                    });

                                    await db.SaveChangesAsync();

                                    //Create License History

                                    var LicHistory = db.M_UserLicenseRenewal.Add(new M_UserLicenseRenewal
                                    {
                                        ApprovalStatus = 0,
                                        CPR_URL = data.CPR_Url,
                                        IsActive = true,
                                        CreatedOnUtc = DateTime.Now,
                                        ID_UserLicense = lic.ID,
                                        MedicalForm_URL = data.Medical_Url
                                    });

                                    await db.SaveChangesAsync();

                                    string message = EmailHelper.LicenseEmailMessage(data.FirstName + " " + data.LastName);
                                    string messageBMF = EmailHelper.LicenseEmailMessageToBMF(new ClsLicForm
                                    {
                                        FirstName = data.FirstName,
                                        LastName = data.LastName,
                                        CPR = data.CPR
                                    });

                                    EmailHelper.SendMail(data.EmailAddress,"", "License Registration Form", message);
                                    EmailHelper.SendMail("info@bmf.com.bh", "alaa@bmf.com.bh", "License Registration Form - for " + data.FirstName + " " + data.LastName, messageBMF);

                                    return RedirectToAction("LicenseSubmitted", "Services");
                                }
                                else
                                {
                                    //License Exist send Renew Message
                                    ViewBag.LicenseCategory = Helper.GetLicenseCategory();
                                    ViewBag.Countries = Helper.GetCountries();
                                    ViewBag.BloodGroup = Helper.GetBloodGroup();
                                    ViewBag.LicenseType = Helper.GetLicenseType();

                                    ModelState.AddModelError("LicenseRequestType", "You already have a license, please select renew.");
                                    return View(data);
                                }
                            }
                        }
                        else if (data.LicenseRequestType == "Renew")
                        {
                            var GetLicense = db.M_UserLicense.Where(a => a.IsActive == true &&
                            a.DeletedOnUtc == null && a.M_UserProfile.CPR == data.CPR
                            && a.ID_LicenseType == data.LicenseCategoryId).FirstOrDefault();

                            if (GetLicense != null)
                            {
                                if (data.Photo != null)
                                {
                                    var fileName = Path.GetFileName(data.Photo.FileName);
                                    StorageManager storageManager = new StorageManager();

                                    data.Photo_Url = await storageManager.UploadPhotoAsync(Helper.AzureStorageContainers.UserProfile, fileName, data.Photo);
                                }

                                if (data.CPRFile != null)
                                {
                                    var fileName = Path.GetFileName(data.CPRFile.FileName);
                                    StorageManager storageManager = new StorageManager();

                                    data.CPR_Url = await storageManager.UploadPhotoAsync(Helper.AzureStorageContainers.LicenseRenewal, fileName, data.CPRFile);
                                }

                                if (data.MedicalFile != null)
                                {
                                    var fileName = Path.GetFileName(data.MedicalFile.FileName);
                                    StorageManager storageManager = new StorageManager();

                                    data.Medical_Url = await storageManager.UploadPhotoAsync(Helper.AzureStorageContainers.LicenseRenewal, fileName, data.MedicalFile);
                                }

                                GetLicense.M_UserProfile.Photo_URL = data.Photo_Url;
                                GetLicense.M_UserProfile.Mobile_Number = data.MobileNumber;
                                GetLicense.M_UserProfile.ID_Nationality = data.CountryId;
                                GetLicense.M_UserProfile.Blood_Group = data.BloodGroup;
                                GetLicense.M_UserProfile.CPR = data.CPR;
                                GetLicense.M_UserProfile.DateOfBirth = data.DateOfBirth;
                                GetLicense.M_UserProfile.EmergencyContact_MobileNumber = data.EmergencyNumber;
                                GetLicense.M_UserProfile.EmergencyContact_Name = data.EmergencyName;
                                GetLicense.M_UserProfile.Place_Of_Birth = data.PlaceOfBirth;
                                GetLicense.M_UserProfile.Full_Address = "House#: " + data.HouseNumber + " Road#: " + data.RoadNumber
                                    + " Block#: " + data.BlockNumber + " City: " + data.City;
                                GetLicense.M_UserProfile.FullName = data.FirstName + " " + data.MiddleName + " " + data.LastName;

                                if (data.IsUnder18 == true)
                                {
                                    GetLicense.M_UserProfile.Parent_FullName = data.ParentFullName;
                                    GetLicense.M_UserProfile.Parent_CPR = data.ParentCPR;
                                }

                                await db.SaveChangesAsync();

                                //Create License History

                                if (data.IsUnder18 == false)
                                {
                                    var LicHistory = db.M_UserLicenseRenewal.Add(new M_UserLicenseRenewal
                                    {
                                        ApprovalStatus = 0,
                                        CPR_URL = data.CPR_Url,
                                        IsActive = true,
                                        CreatedOnUtc = DateTime.Now,
                                        ID_UserLicense = GetLicense.ID,
                                        MedicalForm_URL = data.Medical_Url,
                                    });

                                    await db.SaveChangesAsync();

                                }
                                else
                                {
                                    var LicHistory = db.M_UserLicenseRenewal.Add(new M_UserLicenseRenewal
                                    {
                                        ApprovalStatus = 0,
                                        CPR_URL = data.CPR_Url,
                                        IsActive = true,
                                        CreatedOnUtc = DateTime.Now,
                                        ID_UserLicense = GetLicense.ID,
                                        MedicalForm_URL = data.Medical_Url,
                                        Parent_CPR_URL = data.ParentCPR_Url
                                    });

                                    await db.SaveChangesAsync();

                                }

                                string message = EmailHelper.LicenseEmailMessage(data.FirstName + " " + data.LastName);
                                string messageBMF = EmailHelper.LicenseEmailMessageToBMF(new ClsLicForm
                                {
                                    FirstName = data.FirstName,
                                    LastName = data.LastName,
                                    CPR = data.CPR
                                });

                                EmailHelper.SendMail(data.EmailAddress,"", "License Registration Form", message);
                                EmailHelper.SendMail("info@bmf.com.bh", "alaa@bmf.com.bh", "License Registration Form - for " + data.FirstName + " " + data.LastName, messageBMF);

                                return RedirectToAction("LicenseSubmitted", "Services");
                            }
                            else
                            {
                                //License Not Found

                                ViewBag.LicenseCategory = Helper.GetLicenseCategory();
                                ViewBag.Countries = Helper.GetCountries();
                                ViewBag.BloodGroup = Helper.GetBloodGroup();
                                ViewBag.LicenseType = Helper.GetLicenseType();

                                ModelState.AddModelError("LicenseRequestType", "License not found please select new");
                                return View(data);
                            }
                        }
                        else
                        {

                        }


                    }
                    catch (Exception ex)
                    {

                    }
                }

                ViewBag.LicenseCategory = Helper.GetLicenseCategory();
                ViewBag.Countries = Helper.GetCountries();
                ViewBag.BloodGroup = Helper.GetBloodGroup();
                ViewBag.LicenseType = Helper.GetLicenseType();

                return View(data);
            }
            catch (Exception ex)
            {

            }

            ViewBag.LicenseCategory = Helper.GetLicenseCategory();
            ViewBag.Countries = Helper.GetCountries();
            ViewBag.BloodGroup = Helper.GetBloodGroup();
            ViewBag.LicenseType = Helper.GetLicenseType();

            return View(data);
        }

        public ActionResult MarshalsLicense()
        {
            ClsOfficialForm lic = new ClsOfficialForm();

            ViewBag.Countries = Helper.GetCountries();
            ViewBag.BloodGroup = Helper.GetBloodGroup();
            ViewBag.LicenseType = Helper.GetLicenseType();

            return View(lic);
        }

        public ActionResult CompetitionLicenseRenewal()
        {
            ClsLicRenewForm lic = new ClsLicRenewForm();

            ViewBag.LicenseCategory = Helper.GetLicenseCategory();

            return View(lic);
        }

        [HttpPost]
        public async Task<ActionResult> CompetitionLicenseRenewal(ClsLicRenewForm form)
        {
            ClsLicRenewForm lic = new ClsLicRenewForm();

            ViewBag.LicenseCategory = Helper.GetLicenseCategory();

            if (form.CPR != null && form.LicenseCategoryId != 0)
            {
                var chkExist = db.M_UserLicense.Where(a => a.M_UserProfile.CPR == form.CPR && a.ID_LicenseType == form.LicenseCategoryId).FirstOrDefault();

                if (chkExist != null)
                {
                    var supportedTypes = new[] { "png", "PNG", "jpeg", "JPEG", "jpg", "JPG", "pdf", "PDF" };

                    if (form.IsUnder18 == true)
                    {
                        if (string.IsNullOrEmpty(form.ParentFullName))
                        {
                            ModelState.AddModelError("ParentFullName", "Please ensure to enter a parent name");
                        }

                        if (string.IsNullOrEmpty(form.MobileNumber))
                        {
                            ModelState.AddModelError("ParentCPR", "Please ensure to enter a parent CPR number");
                        }

                        if (form.ParentCPRFile == null)
                        {
                            ModelState.AddModelError("ParentCPRFile", "Please upload parent CPR");
                        }
                        else
                        {

                            var fileExt2 = System.IO.Path.GetExtension(form.ParentCPRFile.FileName).Substring(1);
                            if (!supportedTypes.Contains(fileExt2))
                            {
                                ModelState.AddModelError("ParentCPRFile", "Please use only jpg,jpeg,png,pdf");
                            }
                        }
                    }

                    if (Helper.CalculateAge(chkExist.M_UserProfile.DateOfBirth) < 18 && form.IsUnder18 == false)
                    {
                        ModelState.AddModelError("IsUnder18", "You are under 18 please check the box and fill parent detail");
                    }

                    if (Helper.CalculateAge(chkExist.M_UserProfile.DateOfBirth) > 18 && form.IsUnder18 == true)
                    {
                        ModelState.AddModelError("IsUnder18", "You are over 18 please uncheck the box and clear parent detail if entered");
                    }

                    var errors = ModelState.Values.SelectMany(v => v.Errors);

                    var fileExt = System.IO.Path.GetExtension(form.CPRFile.FileName).Substring(1);
                    if (!supportedTypes.Contains(fileExt))
                    {
                        ModelState.AddModelError("CPRFile", "Please use only jpg,jpeg,png,pdf");
                    }


                    if (form.CPRFile != null)
                    {
                        var fileName = Path.GetFileName(form.CPRFile.FileName);
                        StorageManager storageManager = new StorageManager();

                        form.CPR_Url = await storageManager.UploadPhotoAsync(Helper.AzureStorageContainers.LicenseRenewal, fileName, form.CPRFile);
                    }

                    if (form.MedicalFile != null)
                    {
                        var fileName = Path.GetFileName(form.MedicalFile.FileName);
                        StorageManager storageManager = new StorageManager();

                        form.Medical_Url = await storageManager.UploadPhotoAsync(Helper.AzureStorageContainers.LicenseRenewal, fileName, form.MedicalFile);
                    }

                    if (form.IsUnder18 == true)
                    {
                        if (form.ParentCPRFile != null)
                        {
                            var fileName = Path.GetFileName(form.ParentCPRFile.FileName);
                            StorageManager storageManager = new StorageManager();

                            form.ParentCPR_Url = await storageManager.UploadPhotoAsync(Helper.AzureStorageContainers.LicenseRenewal, fileName, form.ParentCPRFile);
                        }
                    }

                    if (form.IsUnder18 == true)
                    {
                        var LicHistory = db.M_UserLicenseRenewal.Add(new M_UserLicenseRenewal
                        {
                            ApprovalStatus = 0,
                            CPR_URL = form.CPR_Url,
                            IsActive = true,
                            CreatedOnUtc = DateTime.Now,
                            ID_UserLicense = chkExist.ID,
                            MedicalForm_URL = form.Medical_Url,
                            Parent_CPR_URL = form.ParentCPR_Url,
                            UpdatedBy = "",
                            UpdatedOnUtc = DateTime.Now,
                            CreatedBy = "",
                        });
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        var LicHistory = db.M_UserLicenseRenewal.Add(new M_UserLicenseRenewal
                        {
                            ApprovalStatus = 0,
                            CPR_URL = form.CPR_Url,
                            IsActive = true,
                            CreatedOnUtc = DateTime.Now,
                            ID_UserLicense = chkExist.ID,
                            MedicalForm_URL = form.Medical_Url,
                            UpdatedBy = "",
                            UpdatedOnUtc = DateTime.Now,
                            CreatedBy = "",
                        });
                        await db.SaveChangesAsync();
                    }

                    string message = EmailHelper.LicenseRenewEmailMessage(chkExist.M_UserProfile.FullName);
                    string messageBMF = EmailHelper.LicenseRenewEmailMessageToBMF(new ClsLicRenewForm
                    {
                        FullName = chkExist.M_UserProfile.FullName,
                        CPR = chkExist.M_UserProfile.CPR
                    });

                    EmailHelper.SendMail(chkExist.M_UserProfile.M_UserAccount.Email_Address, "", "License Renewal Form", message);
                    EmailHelper.SendMail("info@bmf.com.bh", "alaa@bmf.com.bh", "License Renwal Form - for " + chkExist.M_UserProfile.FullName, messageBMF);
                    //EmailHelper.SendMail("a.it@live.com","", "License Renewal Form - for " + chkExist.M_UserProfile.FullName, messageBMF);

                    return RedirectToAction("LicenseSubmitted", "Services");

                }
                else
                {
                    TempData["Status"] = "CPR or License not found.";
                }
            }
            else
            {
                TempData["Status"] = "Missing Information, please enter your CPR and select license type";
            }

            return View(lic);
        }

        [HttpPost]
        public async Task<ActionResult> MarshalsLicense(ClsOfficialForm data)
        {
            try
            {
                var supportedTypes = new[] { "png", "PNG", "jpeg", "JPEG", "jpg", "JPG", "pdf", "PDF" };

                if (data.Photo.ContentLength > 1000000)
                {
                    ModelState.AddModelError("Photo", "File must be less than 1MB");
                }

                var errors = ModelState.Values.SelectMany(v => v.Errors);

                var PhotofileExt = System.IO.Path.GetExtension(data.Photo.FileName).Substring(1);
                if (!supportedTypes.Contains(PhotofileExt))
                {
                    ModelState.AddModelError("Photo", "Please use only jpg,jpeg,png,pdf");
                }

                DateTime temp;
                if (!DateTime.TryParse(data.DateOfBirth.ToString(), out temp))
                {
                    ModelState.AddModelError("DateOfBirth", "Please enter a valid date");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        if (data.LicenseRequestType == "New")
                        {
                            //Check if Profile Exist
                            var CheckProfileExist = db.M_UserProfile.Where(a => a.CPR == data.CPR
                            && a.IsActive == true && a.DeletedOnUtc == null).FirstOrDefault();

                            if (CheckProfileExist == null)
                            {
                                //Create New Profile

                                if (data.Photo != null)
                                {
                                    var fileName = Path.GetFileName(data.Photo.FileName);
                                    StorageManager storageManager = new StorageManager();

                                    data.Photo_Url = await storageManager.UploadPhotoAsync(Helper.AzureStorageContainers.UserProfile, fileName, data.Photo);
                                }

                                var profile = db.M_UserProfile.Add(new M_UserProfile
                                {
                                    Full_Address = "House#: " + data.HouseNumber + " Road#: " + data.RoadNumber
                                    + " Block#: " + data.BlockNumber + " City: " + data.City,
                                    IsActive = true,
                                    Blood_Group = data.BloodGroup,
                                    CPR = data.CPR,
                                    DateOfBirth = data.DateOfBirth,
                                    FullName = data.FirstName + " " + data.MiddleName + " " + data.LastName,
                                    CreatedOnUtc = DateTime.Now,
                                    ID_Nationality = data.CountryId,
                                    Place_Of_Birth = data.PlaceOfBirth,
                                    Mobile_Number = data.MobileNumber,
                                    EmergencyContact_Name = data.EmergencyName,
                                    EmergencyContact_MobileNumber = data.EmergencyNumber,
                                    Photo_URL = data.Photo_Url,
                                });

                                await db.SaveChangesAsync();

                                //Create License Record

                                var lic = db.M_UserLicense.Add(new M_UserLicense
                                {
                                    License_Number = "",
                                    IsActive = true,
                                    CreatedOnUtc = DateTime.Now,
                                    ID_LicenseType = 5,
                                    ID_UserProfile = profile.ID,
                                });

                                await db.SaveChangesAsync();

                                //Create License History

                                var LicHistory = db.M_UserLicenseRenewal.Add(new M_UserLicenseRenewal
                                {
                                    ApprovalStatus = 0,
                                    IsActive = true,
                                    CreatedOnUtc = DateTime.Now,
                                    ID_UserLicense = lic.ID,
                                });

                                await db.SaveChangesAsync();

                                try
                                {
                                    string message = EmailHelper.LicenseEmailMessage(data.FirstName + " " + data.LastName);
                                    string messageBMF = EmailHelper.LicenseEmailMessageToBMF(new ClsLicForm
                                    {
                                        FirstName = data.FirstName,
                                        LastName = data.LastName,
                                        CPR = data.CPR
                                    });

                                    EmailHelper.SendMail(data.EmailAddress, "", "License Registration Form", message);
                                    EmailHelper.SendMail("info@bmf.com.bh", "alaa@bmf.com.bh", "License Registration Form - for " + data.FirstName + " " + data.LastName, message);

                                }
                                catch (Exception ex)
                                {

                                }

                                return RedirectToAction("MarshalLicenseSubmitted", "Services");

                            }
                            else
                            {
                                ModelState.AddModelError("valMsg", "License Existt, You already have a license.");
                                return View(data);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("valMsg", "Error in Submit, Please fill all required fileds.");
                        }


                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("valMsg", "Error, Please try later.");
                    }
                }
                else
                {

                }

                ViewBag.Countries = Helper.GetCountries();
                ViewBag.BloodGroup = Helper.GetBloodGroup();
                ViewBag.LicenseType = Helper.GetLicenseType();

                return View(data);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("valMsg", "Error, Please try later.");
            }

            ViewBag.Countries = Helper.GetCountries();
            ViewBag.BloodGroup = Helper.GetBloodGroup();
            ViewBag.LicenseType = Helper.GetLicenseType();

            return View(data);
        }

        public ActionResult LicenseSubmitted()
        {
            return View();
        }

        public ActionResult MarshalLicenseSubmitted()
        {
            return View();
        }

        
    }
}