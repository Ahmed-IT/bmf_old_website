using BMF_WebSite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BMF_WebSite.Controllers
{
    public class eSportController : Controller
    {
        BMFEntities1 db = new BMFEntities1();

        // GET: eSport
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegThankYou()
        {
            return View();
        }

        public ActionResult testEmail()
        {
            return View();
        }

        

        public ActionResult Registration()
        {
            eSportRegForm lic = new eSportRegForm();

            ViewBag.Countries = Helper.GetCountries();
            ViewBag.BloodGroup = Helper.GetBloodGroup();
            ViewBag.RacingMode = Helper.GetRacingMode();

            return View(lic);
        }

        [HttpPost]
        public async Task<ActionResult> Registration(eSportRegForm data)
        {
            //try
            //{
            //    if(data.Facebook == null)
            //    {
            //        data.Facebook = "NA";
            //    }

            //    if (data.Instagram == null)
            //    {
            //        data.Instagram = "NA";
            //    }

            //    if (data.OtherMotorsport == null)
            //    {
            //        data.OtherMotorsport = "NA";
            //    }

            //    if (data.MotorsportGame == null)
            //    {
            //        data.MotorsportGame = "NA";
            //    }

            //    if (data.RacingMode == null)
            //    {
            //        data.RacingMode = "NA";
            //    }

            //    var supportedTypes = new[] { "png", "PNG", "jpeg", "JPEG", "jpg", "JPG", "pdf", "PDF" };

            //    if (data.ChkAccept != true)
            //    {
            //        ModelState.AddModelError("ChkAccept", "Please accept the Rules and Regulation");
            //    }

            //    if (data.IsUnder18 == true)
            //    {
            //        if (string.IsNullOrEmpty(data.ParentFullName))
            //        {
            //            ModelState.AddModelError("ParentFullName", "Please ensure to enter a parent name");
            //        }

            //        if (string.IsNullOrEmpty(data.MobileNumber))
            //        {
            //            ModelState.AddModelError("ParentCPR", "Please ensure to enter a parent CPR number");
            //        }

            //        if (data.ParentCPRFile.ContentLength > 1000000)
            //        {
            //            ModelState.AddModelError("ParentCPRFile", "File must be less than 1MB");
            //        }

            //        if (data.ParentCPRFile == null)
            //        {
            //            ModelState.AddModelError("ParentCPRFile", "Please upload parent CPR");
            //        }
            //        else
            //        {

            //            var fileExt2 = System.IO.Path.GetExtension(data.ParentCPRFile.FileName).Substring(1);
            //            if (!supportedTypes.Contains(fileExt2))
            //            {
            //                ModelState.AddModelError("ParentCPRFile", "Please use only jpg,jpeg,png,pdf");
            //            }
            //        }
            //    }

            //    if (Helper.CalculateAge(data.DateOfBirth) < 18 && data.IsUnder18 == false)
            //    {
            //        ModelState.AddModelError("IsUnder18", "You are under 18 please check the box and fill parent detail");
            //    }

            //    if (Helper.CalculateAge(data.DateOfBirth) > 18 && data.IsUnder18 == true)
            //    {
            //        ModelState.AddModelError("IsUnder18", "You are over 18 please uncheck the box and clear parent detail if entered");
            //    }

            //    if (data.CPRFile.ContentLength > 1000000)
            //    {
            //        ModelState.AddModelError("CPRFile", "File must be less than 1MB");
            //    }

            //    if (data.Photo.ContentLength > 1000000)
            //    {
            //        ModelState.AddModelError("Photo", "File must be less than 1MB");
            //    }

            //    var errors = ModelState.Values.SelectMany(v => v.Errors);

            //    var fileExt = System.IO.Path.GetExtension(data.CPRFile.FileName).Substring(1);
            //    if (!supportedTypes.Contains(fileExt))
            //    {
            //        ModelState.AddModelError("CPRFile", "Please use only jpg,jpeg,png,pdf");
            //    }
            //    //else
            //    //{
            //    //    if(data.PhotoFile.ContentLength > 2048)
            //    //    {
            //    //        ModelState.AddModelError("PhotoFile", "File must be less than 1MB");
            //    //    }    
            //    //}

            //    var PhotofileExt = System.IO.Path.GetExtension(data.Photo.FileName).Substring(1);
            //    if (!supportedTypes.Contains(PhotofileExt))
            //    {
            //        ModelState.AddModelError("Photo", "Please use only jpg,jpeg,png,pdf");
            //    }

            //    DateTime temp;
            //    if (!DateTime.TryParse(data.DateOfBirth.ToString(), out temp))
            //    {
            //        ModelState.AddModelError("DateOfBirth", "Please enter a valid date");
            //    }

            //    if (ModelState.IsValid)
            //    {
            //        try
            //        {
            //            var CheckProfileExist = db.M_UserProfile.Where(a => a.CPR == data.CPR
            //                && a.IsActive == true && a.DeletedOnUtc == null).FirstOrDefault();

            //            if (CheckProfileExist == null)
            //            {
            //                //Create New Profile

            //                if (data.Photo != null)
            //                {
            //                    var fileName = Path.GetFileName(data.Photo.FileName);
            //                    StorageManager storageManager = new StorageManager();

            //                    data.Photo_Url = await storageManager.UploadPhotoAsync(Helper.AzureStorageContainers.UserProfile, fileName, data.Photo);
            //                }

            //                if (data.CPRFile != null)
            //                {
            //                    var fileName = Path.GetFileName(data.CPRFile.FileName);
            //                    StorageManager storageManager = new StorageManager();

            //                    data.CPR_Url = await storageManager.UploadPhotoAsync(Helper.AzureStorageContainers.LicenseRenewal, fileName, data.CPRFile);
            //                }

            //                if (data.IsUnder18 == false)
            //                {
            //                    var profile = db.M_UserProfile.Add(new M_UserProfile
            //                    {
            //                        Full_Address = "House#: " + data.HouseNumber + " Road#: " + data.RoadNumber
            //                    + " Block#: " + data.BlockNumber + " City: " + data.City,
            //                        IsActive = true,
            //                        CPR = data.CPR,
            //                        DateOfBirth = data.DateOfBirth,
            //                        FullName = data.FirstName + " " + data.MiddleName + " " + data.LastName,
            //                        CreatedOnUtc = DateTime.Now,
            //                        ID_Nationality = data.CountryId,
            //                        Place_Of_Birth = data.PlaceOfBirth,
            //                        Mobile_Number = data.MobileNumber,
            //                        //Photo_URL = data.Photo_Url,
            //                        SystemRemarks = "PSN: " + data.PSN + " - Instagram: " + data.Instagram + " - Facebook: " + data.Facebook + " - Mode of Racing: " + data.RacingMode + " - Motorsport Game: " + data.MotorsportGame + " - Other Motorsport: " + data.OtherMotorsport
            //                    });

            //                    await db.SaveChangesAsync();

            //                    //Create License Record

            //                    var lic = db.M_UserLicense.Add(new M_UserLicense
            //                    {
            //                        License_Number = "",
            //                        IsActive = true,
            //                        CreatedOnUtc = DateTime.Now,
            //                        ID_LicenseType = 13,
            //                        ID_UserProfile = profile.ID,
            //                    });

            //                    await db.SaveChangesAsync();

            //                    //Create License History

            //                    var LicHistory = db.M_UserLicenseRenewal.Add(new M_UserLicenseRenewal
            //                    {
            //                        ApprovalStatus = 0,
            //                        CPR_URL = data.CPR_Url,
            //                        IsActive = true,
            //                        CreatedOnUtc = DateTime.Now,
            //                        ID_UserLicense = lic.ID,
            //                        //MedicalForm_URL = data.Medical_Url,
            //                        NoObjectionLetter_URL = data.Photo_Url
            //                    });

            //                    await db.SaveChangesAsync();
            //                }
            //                else
            //                {
            //                    var profile = db.M_UserProfile.Add(new M_UserProfile
            //                    {
            //                        Full_Address = "House#: " + data.HouseNumber + " Road#: " + data.RoadNumber
            //                    + " Block#: " + data.BlockNumber + " City: " + data.City,
            //                        IsActive = true,
            //                        CPR = data.CPR,
            //                        DateOfBirth = data.DateOfBirth,
            //                        FullName = data.FirstName + " " + data.MiddleName + " " + data.LastName,
            //                        CreatedOnUtc = DateTime.Now,
            //                        ID_Nationality = data.CountryId,
            //                        Place_Of_Birth = data.PlaceOfBirth,
            //                        Mobile_Number = data.MobileNumber,
            //                        //Photo_URL = data.Photo_Url,
            //                        Parent_CPR = data.ParentCPR,
            //                        Parent_FullName = data.ParentFullName,
            //                        SystemRemarks = "PSN: " + data.PSN + " - Instagram: " + data.Instagram + " - Facebook: " + data.Facebook + " - Mode of Racing: " + data.RacingMode + " - Motorsport Game: " + data.MotorsportGame + " - Other Motorsport: " + data.OtherMotorsport
            //                    });

            //                    await db.SaveChangesAsync();

            //                    //Create License Record

            //                    var lic = db.M_UserLicense.Add(new M_UserLicense
            //                    {
            //                        License_Number = "",
            //                        IsActive = true,
            //                        CreatedOnUtc = DateTime.Now,
            //                        ID_LicenseType = 13,
            //                        ID_UserProfile = profile.ID,
            //                    });

            //                    await db.SaveChangesAsync();

            //                    //Create License History

            //                    var LicHistory = db.M_UserLicenseRenewal.Add(new M_UserLicenseRenewal
            //                    {
            //                        ApprovalStatus = 0,
            //                        CPR_URL = data.CPR_Url,
            //                        IsActive = true,
            //                        CreatedOnUtc = DateTime.Now,
            //                        ID_UserLicense = lic.ID,
            //                        //MedicalForm_URL = data.Medical_Url,
            //                        Parent_CPR_URL = data.ParentCPR_Url,
            //                        NoObjectionLetter_URL = data.Photo_Url
            //                    });

            //                    await db.SaveChangesAsync();
            //                }

            //                string message = EmailHelper.EmailMessage(data.FirstName + " " + data.LastName);

            //                EmailHelper.SendMail(data.EmailAddress, "BEGTC Season 3 Registration", message);

            //                return RedirectToAction("RegThankYou", "eSport");
            //            }
            //            else
            //            {
            //                var checkLicenseExist = db.M_UserProfile.Where(a => a.CPR == data.CPR
            //                        && a.IsActive == true && a.DeletedOnUtc == null &&
            //                        a.M_UserLicense.Where(b => b.ID_LicenseType == 13).FirstOrDefault() != null).FirstOrDefault();

            //                if (checkLicenseExist == null)
            //                {
            //                    //Add New License

            //                    if (data.Photo != null)
            //                    {
            //                        var fileName = Path.GetFileName(data.Photo.FileName);
            //                        StorageManager storageManager = new StorageManager();

            //                        data.Photo_Url = await storageManager.UploadPhotoAsync(Helper.AzureStorageContainers.UserProfile, fileName, data.Photo);
            //                    }

            //                    if (data.CPRFile != null)
            //                    {
            //                        var fileName = Path.GetFileName(data.CPRFile.FileName);
            //                        StorageManager storageManager = new StorageManager();

            //                        data.CPR_Url = await storageManager.UploadPhotoAsync(Helper.AzureStorageContainers.LicenseRenewal, fileName, data.CPRFile);
            //                    }

            //                    CheckProfileExist.Full_Address = "House#: " + data.HouseNumber + " Road#: " + data.RoadNumber
            //                    + " Block#: " + data.BlockNumber + " City: " + data.City;
            //                    CheckProfileExist.IsActive = true;
            //                    CheckProfileExist.CPR = data.CPR;
            //                    CheckProfileExist.DateOfBirth = data.DateOfBirth;
            //                    CheckProfileExist.FullName = data.FirstName + " " + data.MiddleName + " " + data.LastName;
            //                    CheckProfileExist.CreatedOnUtc = DateTime.Now;
            //                    CheckProfileExist.ID_Nationality = data.CountryId;
            //                    CheckProfileExist.Place_Of_Birth = data.PlaceOfBirth;
            //                    CheckProfileExist.Mobile_Number = data.MobileNumber;
            //                    CheckProfileExist.SystemRemarks = "PSN: " + data.PSN + " - Instagram: " + data.Instagram + " - Facebook: " + data.Facebook + " - Mode of Racing: " + data.RacingMode + " - Motorsport Game: " + data.MotorsportGame + " - Other Motorsport: " + data.OtherMotorsport;

            //                    await db.SaveChangesAsync();

            //                    var lic = db.M_UserLicense.Add(new M_UserLicense
            //                    {
            //                        License_Number = "",
            //                        IsActive = true,
            //                        CreatedOnUtc = DateTime.Now,
            //                        ID_LicenseType = 13,
            //                        ID_UserProfile = CheckProfileExist.ID,
            //                    });

            //                    await db.SaveChangesAsync();

            //                    //Create License History

            //                    var LicHistory = db.M_UserLicenseRenewal.Add(new M_UserLicenseRenewal
            //                    {
            //                        ApprovalStatus = 0,
            //                        CPR_URL = data.CPR_Url,
            //                        NoObjectionLetter_URL = data.Photo_Url,
            //                        IsActive = true,
            //                        CreatedOnUtc = DateTime.Now,
            //                        ID_UserLicense = lic.ID,
            //                    });

            //                    await db.SaveChangesAsync();

            //                    string message = EmailHelper.EmailMessage(data.FirstName + " " + data.LastName);

            //                    EmailHelper.SendMail(data.EmailAddress, "BEGTC Season 3 Registration", message);

            //                    return RedirectToAction("RegThankYou", "eSport");
            //                }
            //                else
            //                {
            //                    TempData["Status"] = "Exist";

            //                    //License Exist send Renew Message
            //                    ViewBag.Countries = Helper.GetCountries();
            //                    ViewBag.BloodGroup = Helper.GetBloodGroup();
            //                    ViewBag.RacingMode = Helper.GetRacingMode();

            //                    return View(data);
            //                }
            //            }


            //        }
            //        catch (Exception ex)
            //        {

            //        }
            //    }

            //    ViewBag.Countries = Helper.GetCountries();
            //    ViewBag.BloodGroup = Helper.GetBloodGroup();
            //    ViewBag.RacingMode = Helper.GetRacingMode();

            //    TempData["Status"] = "Error";

            //    return View(data);
            //}
            //catch (Exception ex)
            //{

            //}

            TempData["Status"] = "Error";

            ViewBag.Countries = Helper.GetCountries();
            ViewBag.BloodGroup = Helper.GetBloodGroup();
            ViewBag.RacingMode = Helper.GetRacingMode();

            return View(data);
        }

    }
}