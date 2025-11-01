using BMF_WebSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Http;

namespace BMF_WebSite.Controllers
{
    public class LicenseController : ApiController
    {
        BMFEntities1 db = new BMFEntities1();

        [HttpGet]
        [Route("api/GetMyLicense/{Id}")]
        public async Task<IHttpActionResult> GetMyLicense(int Id)
        {
            try
            {
                var query = await (from l in db.M_UserLicense
                                   where l.M_UserProfile.ID == Id &&
                                   l.IsActive == true && l.DeletedOnUtc == null
                                   && l.D_LicenseType.IsActive == true && 
                                   l.D_LicenseType.DeletedOnUtc == null
                                   select new
                                   {
                                       l.Expire_Date,
                                       l.ID,
                                       l.ID_LicenseType,
                                       l.ID_UserProfile,
                                       l.License_Number,
                                       LicenseType = new
                                       {
                                           l.D_LicenseType.ID,
                                           l.D_LicenseType.Image_Front_URL,
                                           l.D_LicenseType.Image_Back_URL,
                                           Title = l.D_LicenseType.Title != "Official" ? l.D_LicenseType.Title : l.D_MarshalRole.Name,
                                           l.D_LicenseType.Type
                                       },
                                       Profile = new
                                       {
                                           l.M_UserProfile.CPR,
                                           l.M_UserProfile.Photo_URL,
                                           l.M_UserProfile.FullName,
                                           l.M_UserProfile.Blood_Group,
                                           l.M_UserProfile.DateOfBirth
                                       }
                                   }).ToListAsync();

                if (query.Count > 0)
                {
                    return Ok(query);
                }
                else
                {
                    return Content(HttpStatusCode.NoContent, "No Content");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/GetLicensesList/{Id}")]
        public async Task<IHttpActionResult> GetLicensesList(int Id)
        {
            try
            {
                var query = await (from l in db.M_UserLicense
                                   where l.D_LicenseType.ID == Id
                                   && l.IsActive == true && l.DeletedOnUtc == null
                                   && l.D_LicenseType.IsActive == true &&
                                   l.D_LicenseType.DeletedOnUtc == null
                                   select new
                                   {
                                       l.Expire_Date,
                                       l.ID,
                                       l.ID_LicenseType,
                                       l.ID_UserProfile,
                                       l.License_Number,
                                       LicenseType = new
                                       {
                                           l.D_LicenseType.ID,
                                           l.D_LicenseType.Image_Front_URL,
                                           l.D_LicenseType.Image_Back_URL,
                                           Title = l.D_LicenseType.Title != "Official" ? l.D_LicenseType.Title : l.D_MarshalRole.Name,
                                           l.D_LicenseType.Type
                                       },
                                       Profile = new
                                       {
                                           l.M_UserProfile.CPR,
                                           l.M_UserProfile.Photo_URL,
                                           l.M_UserProfile.FullName,
                                           l.M_UserProfile.Blood_Group,
                                           l.M_UserProfile.DateOfBirth
                                       }
                                   }).ToListAsync();

                if (query.Count > 0)
                {
                    return Ok(query);
                }
                else
                {
                    return Content(HttpStatusCode.NoContent, "No Content");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/GetLicensesByCPR/{CPR}")]
        public async Task<IHttpActionResult> GetLicensesByCPR(string CPR)
        {
            try
            {
                var query = await (from l in db.M_UserLicense
                                   where l.M_UserProfile.CPR == CPR &&
                                   l.IsActive == true && l.DeletedOnUtc == null
                                   && l.D_LicenseType.IsActive == true &&
                                   l.D_LicenseType.DeletedOnUtc == null
                                   select new
                                   {
                                       l.Expire_Date,
                                       l.ID,
                                       l.ID_LicenseType,
                                       l.ID_UserProfile,
                                       l.License_Number,
                                       LicenseType = new
                                       {
                                           l.D_LicenseType.ID,
                                           l.D_LicenseType.Image_Front_URL,
                                           l.D_LicenseType.Image_Back_URL,
                                           Title = l.D_LicenseType.Title != "Official" ? l.D_LicenseType.Title : l.D_MarshalRole.Name,
                                           l.D_LicenseType.Type
                                       },
                                       Profile = new
                                       {
                                           l.M_UserProfile.CPR,
                                           l.M_UserProfile.Photo_URL,
                                           l.M_UserProfile.FullName,
                                           l.M_UserProfile.Blood_Group,
                                           l.M_UserProfile.DateOfBirth
                                       }
                                   }).ToListAsync();

                if (query.Count > 0)
                {
                    return Ok(query);
                }
                else
                {
                    return Content(HttpStatusCode.NoContent, "No Content");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/GetExpiredLicensesList/{Id}")]
        public async Task<IHttpActionResult> GetExpiredLicensesList(int Id)
        {
            try
            {
                var query = await (from l in db.M_UserLicense
                                   where l.D_LicenseType.ID == Id
                                   && l.IsActive == true && l.DeletedOnUtc == null
                                   && l.D_LicenseType.IsActive == true &&
                                   l.D_LicenseType.DeletedOnUtc == null
                                   && l.Expire_Date >= DateTime.Now
                                   select new
                                   {
                                       l.Expire_Date,
                                       l.ID,
                                       l.ID_LicenseType,
                                       l.ID_UserProfile,
                                       l.License_Number,
                                       LicenseType = new
                                       {
                                           l.D_LicenseType.ID,
                                           l.D_LicenseType.Image_Front_URL,
                                           l.D_LicenseType.Image_Back_URL,
                                           Title = l.D_LicenseType.Title != "Official" ? l.D_LicenseType.Title : l.D_MarshalRole.Name,
                                           l.D_LicenseType.Type
                                       },
                                       Profile = new
                                       {
                                           l.M_UserProfile.CPR,
                                           l.M_UserProfile.Photo_URL,
                                           l.M_UserProfile.FullName,
                                           l.M_UserProfile.Blood_Group,
                                           l.M_UserProfile.DateOfBirth
                                       }
                                   }).ToListAsync();

                if (query.Count > 0)
                {
                    return Ok(query);
                }
                else
                {
                    return Content(HttpStatusCode.NoContent, "No Content");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/GetLicensesType")]
        public async Task<IHttpActionResult> GetLicensesType()
        {
            try
            {
                var query = await (from l in db.D_LicenseType
                                   where
                                   l.IsActive == true && l.DeletedOnUtc == null
                                   && l.IsActive == true &&
                                   l.DeletedOnUtc == null
                                   select new
                                   {
                                           l.ID,
                                           l.Image_Front_URL,
                                           l.Image_Back_URL,
                                           l.Title,
                                           l.Type
                                   }).ToListAsync();

                if (query.Count > 0)
                {
                    return Ok(query);
                }
                else
                {
                    return Content(HttpStatusCode.NoContent, "No Content");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("api/RenewLicenseList")]
        public async Task<IHttpActionResult> RenewLicenseList(List<string> CPR_List)
        {
            try
            {
                List<clsLicStatus> result = new List<clsLicStatus>();

                if (CPR_List.Count > 0)
                {
                    foreach (var item in CPR_List)
                    {
                        var query = db.M_UserLicense.Where(a => a.M_UserProfile.CPR == item && a.ID_LicenseType == 5).FirstOrDefault();

                        try
                        {

                            if (query != null)
                            {
                                query.Expire_Date = Convert.ToDateTime("31-Mar-2026");
                                await db.SaveChangesAsync();

                                result.Add(new clsLicStatus
                                {
                                    CPR = query.M_UserProfile.CPR,
                                    Status = "Renewed"
                                });
                            }
                            else
                            {
                                result.Add(new clsLicStatus
                                {
                                    CPR = query.M_UserProfile.CPR,
                                    Status = "Not Found"
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            result.Add(new clsLicStatus
                            {
                                CPR = item,
                                Status = "Error"
                            });
                        }
                    }

                    return Ok(result);
                }
                else
                {
                    return Content(HttpStatusCode.NoContent, "No Content");
                }
                

                
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("api/DeActiveMarshalsNotInActiveList")]
        public async Task<IHttpActionResult> DeActiveMarshalsNotInActiveList(List<string> CPR_List)
        {
            try
            {
                List<M_UserLicense> list = new List<M_UserLicense>();
                List<clsLicStatus> result = new List<clsLicStatus>();

                var activeList = CPR_List;
                var query = db.M_UserLicense.Where(a => a.ID_LicenseType == 5).ToList();
                
                list.AddRange(query);
                
                var notInActiveList = list.Where(a => !activeList.Contains(a.M_UserProfile.CPR)).ToList();


                if (notInActiveList.Count > 0)
                {
                    foreach (var item in notInActiveList)
                    {
                        try
                        {
                            var chkMarshal = query.Where(a => a.ID_UserProfile == item.ID_UserProfile).FirstOrDefault();

                            if (chkMarshal != null)
                            {
                                chkMarshal.IsActive = false;
                                chkMarshal.DeletedBy = "Admin";
                                chkMarshal.DeletedOnUtc = DateTime.UtcNow;
                                chkMarshal.UpdatedOnUtc = DateTime.UtcNow;

                                await db.SaveChangesAsync();

                                result.Add(new clsLicStatus
                                {
                                    CPR = item.M_UserProfile.CPR,
                                    Status = "De-Activated"
                                });
                            }
                            else
                            {
                                result.Add(new clsLicStatus
                                {
                                    CPR = item.M_UserProfile.CPR,
                                    Status = "Not Found"
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            result.Add(new clsLicStatus
                            {
                                CPR = item.M_UserProfile.CPR,
                                Status = "Error"
                            });
                        }
                    }                    

                    return Ok(result);
                }
                else
                {
                    return Content(HttpStatusCode.NoContent, "No Content");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }

        [HttpPost]
        [Route("api/ValidateMarshalsList")]
        public async Task<IHttpActionResult> ValidateMarshalsList(List<string> CPR_List)
        {
            try
            {
                List<M_UserLicense> list = new List<M_UserLicense>();
                List<clsLicStatus> result = new List<clsLicStatus>();

                var marshalsList = CPR_List;
                var query = db.M_UserLicense.Where(a => a.ID_LicenseType == 5).ToList();

                list.AddRange(query);

                var InActiveList = list.Where(a => marshalsList.Contains(a.M_UserProfile.CPR)).ToList();


                if (InActiveList.Count > 0)
                {
                    foreach (var item in InActiveList)
                    {
                        try
                        {
                            var chkMarshal = query.Where(a => a.ID_UserProfile == item.ID_UserProfile).FirstOrDefault();

                            if (chkMarshal != null)
                            {
                                result.Add(new clsLicStatus
                                {
                                    CPR = item.M_UserProfile.CPR,
                                    Status = "Exist"
                                });
                            }
                            else
                            {
                                result.Add(new clsLicStatus
                                {
                                    CPR = item.M_UserProfile.CPR,
                                    Status = "Not Found"
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            result.Add(new clsLicStatus
                            {
                                CPR = item.M_UserProfile.CPR,
                                Status = "Error"
                            });
                        }
                    }

                    return Ok(result);
                }
                else
                {
                    return Content(HttpStatusCode.NoContent, "No Content");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }

        [HttpPost]
        [Route("api/DeActiveMarshalsList")]
        public async Task<IHttpActionResult> DeActiveMarshalsList(List<string> CPR_List)
        {
            try
            {
                List<clsLicStatus> result = new List<clsLicStatus>();

                var activeList = CPR_List;
                

                foreach (var item in CPR_List)
                {
                    try
                    {
                        var query = db.M_UserLicense.Where(a => a.ID_LicenseType == 5 && a.M_UserProfile.CPR == item).FirstOrDefault();

                        if (query != null)
                        {
                            query.IsActive = false;
                            query.UpdatedOnUtc = DateTime.UtcNow;

                            await db.SaveChangesAsync();  

                            result.Add(new clsLicStatus
                            {
                                CPR = item,
                                Status = "De-Activated"
                            });
                        }
                        else
                        {
                            result.Add(new clsLicStatus
                            {
                                CPR = item,
                                Status = "Not Found"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Add(new clsLicStatus
                        {
                            CPR = item,
                            Status = "Error"
                        });
                    }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }

        [HttpGet]
        [Route("api/ActivateLicensesByCPR/{CPR}")]
        public async Task<IHttpActionResult> ActivateLicensesByCPR(string CPR)
        {
            try
            {
                var query = await db.M_UserLicense.Where(a => a.M_UserProfile.CPR == CPR).FirstOrDefaultAsync();

                if (query != null)
                {
                    var getmarshal = db.M_UserProfile.Where(a => a.CPR == CPR && a.IsActive == false).FirstOrDefault();

                    if(getmarshal != null)
                    {
                        getmarshal.IsActive = true;

                        await db.SaveChangesAsync();
                    }

                    query.IsActive = true;
                    query.DeletedBy = null;
                    query.DeletedOnUtc = null;

                    await db.SaveChangesAsync();

                    return Content(HttpStatusCode.OK, "Marshal License Activated");
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "Marshal Not Found");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
