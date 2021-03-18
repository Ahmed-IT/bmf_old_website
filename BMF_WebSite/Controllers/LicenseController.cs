using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                                   l.IsActive == true
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
                                           l.D_LicenseType.Title,
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
    }
}
