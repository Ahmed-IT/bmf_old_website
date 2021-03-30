using BMF_WebSite.Models;
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
    public class ProfileController : ApiController
    {
        BMFEntities1 db = new BMFEntities1();

        [HttpGet]
        [Route("api/GetProfile/{CPR}")]
        public async Task<IHttpActionResult> GetProfile(string CPR)
        {
            try
            {
                var query = await (from p in db.M_UserProfile
                                   where p.CPR == CPR &&
                                   p.IsActive == true && p.DeletedOnUtc == null
                                   select new
                                   {
                                       p.ID,
                                       p.FullName,
                                       p.CPR,
                                       p.Blood_Group,
                                       p.DateOfBirth,
                                       p.Full_Address,
                                       p.Mobile_Number,
                                       Nationality = p.Country.Name,
                                       p.Photo_URL,
                                       p.Place_Of_Birth,
                                       Account = new
                                       {
                                           p.M_UserAccount.ID,
                                           p.M_UserAccount.Email_Address
                                       }
                                   }).FirstOrDefaultAsync();

                if (query != null)
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
        [Route("api/UpdateMarshalProfile")]
        public async Task<IHttpActionResult> UpdateMarshalProfile(ClsProfile item)
        {
            try
            {
                var query = await (from p in db.M_UserProfile
                                   where p.CPR == item.CPR
                                   select p).FirstOrDefaultAsync();

                if (query != null)
                {
                    query.Blood_Group = item.Blood_Group;

                    query.ID_Nationality = db.Countries.Where(a => a.Name == item.Nationality).FirstOrDefault() == null ? 168 :
                        db.Countries.Where(a => a.Name == item.Nationality).FirstOrDefault().ID;
                    
                    query.DateOfBirth = item.DateOfBirth;
                    query.FullName = item.FullName;
                    query.Full_Address = item.Full_Address;
                    query.Mobile_Number = item.Mobile_Number;

                    await db.SaveChangesAsync();

                    return Content(HttpStatusCode.OK, "Updated");
                }
                else
                {                   
                    return Content(HttpStatusCode.NotFound, "Not Found");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
