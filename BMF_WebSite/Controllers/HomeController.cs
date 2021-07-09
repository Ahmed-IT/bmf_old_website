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

namespace BMF_WebSite.Controllers
{
    public class HomeController : Controller
    {
        BMFEntities1 db = new BMFEntities1();

        // GET: Home
        public async Task<ActionResult> Index()
        {
            try
            {
                MainPageVM vm = new MainPageVM();
                var getdash = await vm.GetMainPage();

                //EmailHelper.SendMail("a.it@live.com","test from bmf", "test subject");

                return View(getdash);
            }
            catch (Exception ex)
            {

            }

            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult BoardMembers()
        {
            return View();
        }

        public ActionResult MissionStatement()
        {
            return View();
        }

        public async Task<ActionResult> RulesRegulations()
        {
            try
            {
                var query = await (from c in db.M_BRCategory
                                  where c.IsActive == true && c.DeletedOnUtc == null
                                   select new ClsBRCategory
                                  {
                                      ID = c.ID,
                                      Title = c.Title,
                                      SubCategory = c.M_BRSubCategory.Where(b => b.IsActive == true
                                      && b.DeletedOnUtc == null).Select(a => new ClsBRSubCategory
                                      {
                                          ID = a.ID,
                                          ID_Category = a.ID_Category,
                                          Title = a.Title,
                                          RulesRegs = a.M_BRSubCategoryRulesRegs.Where(b => b.IsActive == true
                                                      && b.DeletedOnUtc == null).Select(b => new ClsRulesRegs
                                                      {
                                                          ID = b.ID,
                                                          ID_SubCategory = b.ID_SubCategory,
                                                          PDF_Url = b.PDF_Url,
                                                          Title = b.Title
                                                      }).ToList()
                                      }).ToList()
                                  }).Where(x => x.SubCategory.Where(v => v.RulesRegs.Count() > 0).Count() > 0).ToListAsync();

                return View(query);
            }
            catch (Exception ex)
            {

            }

            return View();
        }

        public ActionResult EventRulesRegs(int ID)
        {
            try
            {
               
            }
            catch (Exception ex)
            {

            }

            return View();
        }

        public ActionResult EventPermit()
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
                var supportedTypes = new[] { "png", "PNG", "jpeg", "JPEG", "jpg", "JPG","pdf","PDF" };

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
                        if(data.LicenseRequestType == "New")
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

                                if(data.IsUnder18 == false)
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
                                

                                return RedirectToAction("LicenseSubmitted", "Home");
                            }
                            else
                            {
                                var checkLicenseExist = db.M_UserProfile.Where(a => a.CPR == data.CPR
                                        && a.IsActive == true && a.DeletedOnUtc == null &&
                                        a.M_UserLicense.Where(b => b.ID_LicenseType == data.LicenseCategoryId).FirstOrDefault() != null).FirstOrDefault();

                                if(checkLicenseExist == null)
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
                                        ID_UserProfile = checkLicenseExist.ID,
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

                                    return RedirectToAction("LicenseSubmitted", "Home");
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
                        else if(data.LicenseRequestType == "Renew")
                        {
                            var GetLicense = db.M_UserLicense.Where(a => a.IsActive == true &&
                            a.DeletedOnUtc == null && a.M_UserProfile.CPR == data.CPR
                            && a.ID_LicenseType == data.LicenseCategoryId).FirstOrDefault();

                            if(GetLicense != null)
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

                                if(data.IsUnder18 == false)
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

                                return RedirectToAction("LicenseSubmitted", "Home");
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

        public ActionResult LicenseSubmitted()
        {
            return View();
        }

        public ActionResult CdpApplication()
        {
            return View();
        }

        public async Task<ActionResult> EventCalendar()
        {
            try
            {
                EventCalendarVM vm = new EventCalendarVM();
                var getcalndar = await vm.GetCalendarPage();

                return View(getcalndar);
            }
            catch (Exception ex)
            {

            }

            return View();
        }

        public async Task<ActionResult> EventFullCalendar(int ID)
        {
            try
            {
                var query = await (from r in db.M_EventRound
                                   where r.ID_EventCategory == ID
                                   && r.IsActive == true && r.DeletedBy == null
                                   select new ClsEventRound
                                   {
                                       DateTime_End = r.DateTime_End,
                                       DateTime_Start = r.DateTime_Start,
                                       Round_Number = r.Round_Number,
                                       ID_EventCategory = r.ID_EventCategory,
                                       ID = r.ID,
                                       EventCategory = new ClsEventCategory
                                       {
                                           IsActive = r.M_EventCategory.IsActive,
                                           ID = r.M_EventCategory.ID,
                                           ID_EventSeason = r.M_EventCategory.ID_EventSeason,
                                           Image_URL = r.M_EventCategory.Image_URL,
                                           Title = r.M_EventCategory.Title
                                       }
                                   }).ToListAsync();

                return View(query);
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("EventCalendar", "Home");
        }

        public ActionResult Results()
        {
            return View();
        }

        public async Task<ActionResult> Gallery()
        {
            var query = await (from g in db.M_GalleryCategory
                               where g.DeletedOnUtc == null &&
                               g.IsActive == true
                               select new ClsGalleryCategory
                               {
                                   ID = g.ID,
                                   Image_URL = g.Image_URL,
                                   Title = g.Title
                               }).ToListAsync();

            if(query.Count > 0)
            {
                return View(query);
            }
            else
            {
                return View();
            }            
        }

        public async Task<ActionResult> GalleryImages(long ID)
        {
            if(ID != 0)
            {
                var query = await (from g in db.M_GalleryCategory
                                   where g.ID == ID
                                   select new ClsGalleryCategory
                                   {
                                       ID = g.ID,
                                       Image_URL = g.Image_URL,
                                       Title = g.Title,
                                       GalleryImages = g.M_GalleryImages.Select(i => new ClsGalleryImage
                                       {
                                           ID = i.ID,
                                           ID_GalleryCategory = i.ID_GalleryCategory,
                                           Image_URL = i.Image_URL
                                       }).ToList()
                                   }).FirstOrDefaultAsync();

                return View(query);
            }
            else
            {
                return RedirectToAction("Gallery");
            }

            
        }
        

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public async Task<ActionResult> Bulletins()
        {
            var query = await (from c in db.M_BRCategory
                               where c.IsActive == true && c.DeletedOnUtc == null
                               select new ClsBRCategory
                               {
                                   ID = c.ID,
                                   Title = c.Title,
                                   SubCategory = c.M_BRSubCategory.Where(b => b.IsActive == true
                                   && b.DeletedOnUtc == null
                                   && c.M_BRSubCategory.Select(sb => sb.M_BRSubCategoryBulletin).Count() != 0).Select(a => new ClsBRSubCategory
                                   {
                                       ID = a.ID,
                                       ID_Category = a.ID_Category,
                                       Title = a.Title,
                                       Bulletins = a.M_BRSubCategoryBulletin.Where(b => b.IsActive == true
                                   && b.DeletedOnUtc == null).Select(b => new ClsBulletins
                                   {
                                       ID = b.ID,
                                       ID_SubCategory = b.ID_SubCategory,
                                       PDF_Url = b.PDF_Url,
                                       Title = b.Title
                                   }).ToList()
                                   }).ToList()
                               }).Where(x => x.SubCategory.Where(v => v.Bulletins.Count() > 0).Count() > 0).ToListAsync();

            return View(query);
        }

        public ActionResult NewsDetail()
        {
            return View();
        }

        public ActionResult ResultsDetail()
        {
            return View();
        }

        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult eSport()
        {
            eSportRegForm lic = new eSportRegForm();

            ViewBag.Countries = Helper.GetCountries();
            ViewBag.BloodGroup = Helper.GetBloodGroup();
            ViewBag.RacingMode = Helper.GetRacingMode();

            return View("eSport", lic);
        }

        [HttpPost]
        public async Task<ActionResult> eSport(eSportRegForm data)
        {
            try
            {
                var supportedTypes = new[] { "png", "PNG", "jpeg", "JPEG", "jpg", "JPG", "pdf", "PDF" };

                if(data.ChkAccept != true)
                {
                    ModelState.AddModelError("ChkAccept", "Please accept the Rules and Regulation");
                }

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

                    if (data.ParentCPRFile.ContentLength > 1000000)
                    {
                        ModelState.AddModelError("ParentCPRFile", "File must be less than 1MB");
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

                if (data.CPRFile.ContentLength > 1000000)
                {
                    ModelState.AddModelError("CPRFile", "File must be less than 1MB");
                }

                if (data.Photo.ContentLength > 1000000)
                {
                    ModelState.AddModelError("Photo", "File must be less than 1MB");
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

                            if (data.IsUnder18 == false)
                            {
                                var profile = db.M_UserProfile.Add(new M_UserProfile
                                {
                                    Full_Address = "House#: " + data.HouseNumber + " Road#: " + data.RoadNumber
                                + " Block#: " + data.BlockNumber + " City: " + data.City,
                                    IsActive = true,
                                    CPR = data.CPR,
                                    DateOfBirth = data.DateOfBirth,
                                    FullName = data.FirstName + " " + data.MiddleName + " " + data.LastName,
                                    CreatedOnUtc = DateTime.Now,
                                    ID_Nationality = data.CountryId,
                                    Place_Of_Birth = data.PlaceOfBirth,
                                    Mobile_Number = data.MobileNumber,
                                    //Photo_URL = data.Photo_Url,
                                    SystemRemarks = "PSN: " + data.PSN + " - Instagram: " + data.Instagram + " - Facebook: " + data.Facebook + " - Mode of Racing: " + data.RacingMode + " - Motorsport Game: " + data.MotorsportGame + " - Other Motorsport: " + data.OtherMotorsport
                                });

                                await db.SaveChangesAsync();

                                //Create License Record

                                var lic = db.M_UserLicense.Add(new M_UserLicense
                                {
                                    License_Number = "",
                                    IsActive = true,
                                    CreatedOnUtc = DateTime.Now,
                                    ID_LicenseType = 13,
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
                                    //MedicalForm_URL = data.Medical_Url,
                                    NoObjectionLetter_URL = data.Photo_Url
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
                                    CPR = data.CPR,
                                    DateOfBirth = data.DateOfBirth,
                                    FullName = data.FirstName + " " + data.MiddleName + " " + data.LastName,
                                    CreatedOnUtc = DateTime.Now,
                                    ID_Nationality = data.CountryId,
                                    Place_Of_Birth = data.PlaceOfBirth,
                                    Mobile_Number = data.MobileNumber,
                                    //Photo_URL = data.Photo_Url,
                                    Parent_CPR = data.ParentCPR,
                                    Parent_FullName = data.ParentFullName,
                                    SystemRemarks = "PSN: " + data.PSN + " - Instagram: " + data.Instagram + " - Facebook: " + data.Facebook + " - Mode of Racing: " + data.RacingMode + " - Motorsport Game: " + data.MotorsportGame + " - Other Motorsport: " + data.OtherMotorsport
                                });

                                await db.SaveChangesAsync();

                                //Create License Record

                                var lic = db.M_UserLicense.Add(new M_UserLicense
                                {
                                    License_Number = "",
                                    IsActive = true,
                                    CreatedOnUtc = DateTime.Now,
                                    ID_LicenseType = 13,
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
                                    //MedicalForm_URL = data.Medical_Url,
                                    Parent_CPR_URL = data.ParentCPR_Url,
                                    NoObjectionLetter_URL = data.Photo_Url
                                });

                                await db.SaveChangesAsync();
                            }

                            string message = EmailHelper.EmailMessage(data.FirstName + " " + data.LastName);

                            EmailHelper.SendMail(data.EmailAddress, "BEGTC Season 3 Registration", message);

                            return RedirectToAction("RegThankYou", "Home");
                        }
                        else
                        {
                            var checkLicenseExist = db.M_UserProfile.Where(a => a.CPR == data.CPR
                                    && a.IsActive == true && a.DeletedOnUtc == null &&
                                    a.M_UserLicense.Where(b => b.ID_LicenseType == 13).FirstOrDefault() != null).FirstOrDefault();

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

                                //CheckProfileExist.Photo_URL = data.Photo_Url;

                                await db.SaveChangesAsync();

                                var lic = db.M_UserLicense.Add(new M_UserLicense
                                {
                                    License_Number = "",
                                    IsActive = true,
                                    CreatedOnUtc = DateTime.Now,
                                    ID_LicenseType = 13,
                                    ID_UserProfile = CheckProfileExist.ID,
                                });

                                await db.SaveChangesAsync();

                                //Create License History

                                var LicHistory = db.M_UserLicenseRenewal.Add(new M_UserLicenseRenewal
                                {
                                    ApprovalStatus = 0,
                                    CPR_URL = data.CPR_Url,
                                    NoObjectionLetter_URL = data.Photo_Url,
                                    IsActive = true,
                                    CreatedOnUtc = DateTime.Now,
                                    ID_UserLicense = lic.ID,
                                });

                                await db.SaveChangesAsync();

                                string message = EmailHelper.EmailMessage(data.FirstName + " " + data.LastName);

                                EmailHelper.SendMail(data.EmailAddress, "BEGTC Season 3 Registration", message);

                                return RedirectToAction("RegThankYou", "Home");
                            }
                            else
                            {
                                TempData["Status"] = "Exist";

                                //License Exist send Renew Message
                                ViewBag.Countries = Helper.GetCountries();
                                ViewBag.BloodGroup = Helper.GetBloodGroup();
                                ViewBag.RacingMode = Helper.GetRacingMode();

                                return View(data);
                            }
                        }


                    }
                    catch (Exception ex)
                    {

                    }
                }

                ViewBag.Countries = Helper.GetCountries();
                ViewBag.BloodGroup = Helper.GetBloodGroup();
                ViewBag.RacingMode = Helper.GetRacingMode();

                TempData["Status"] = "Error";

                return View(data);
            }
            catch (Exception ex)
            {

            }

            TempData["Status"] = "Error";

            ViewBag.Countries = Helper.GetCountries();
            ViewBag.BloodGroup = Helper.GetBloodGroup();
            ViewBag.RacingMode = Helper.GetRacingMode();

            return View(data);
        }

        
        public ActionResult RegThankYou()
        {
            return View();
        }

        public ActionResult testValidation()
        {
            eSportRegForm form = new eSportRegForm();

            ViewBag.Countries = Helper.GetCountries();
            ViewBag.BloodGroup = Helper.GetBloodGroup();
            ViewBag.RacingMode = Helper.GetRacingMode();

            return View(form);
        }

        public async Task<JsonResult> GetFullCalendar()
        {
            var query = await (from c in db.M_EventCategory
                         join r in db.M_EventRound on c.ID equals r.ID_EventCategory
                         where c.IsActive == true && r.IsActive == true
                         select new
                         {
                             c.Title,
                             r.Round_Number,
                             r.DateTime_Start,
                             r.DateTime_End
                         }).ToListAsync();

            return Json(query.Select(a => new
            {
                title = a.Title + " Round " + a.Round_Number,
                start = a.DateTime_Start.ToString("yyyy-MM-dd"),
                end = a.DateTime_End.ToString("yyyy-MM-dd")
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}