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
                                   where p.CPR == CPR
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
    }
}
