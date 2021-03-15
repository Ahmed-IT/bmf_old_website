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
    public class UserAccountController : ApiController
    {
        BMFEntities db = new BMFEntities();

        [HttpPost]
        [Route("api/AccountAuth")]
        public async Task<IHttpActionResult> AccountAuth(ClsUser user)
        {
            try
            {
                var query = await (from u in db.M_User
                                   where (u.CPR_Number == user.CPR_Number || u.Email_Address == user.CPR_Number)
                                   && u.Password == user.Password && u.IsActive == true
                                   select new
                                   {
                                       u.Email_Address,
                                       u.CPR_Number,
                                       u.ID,
                                       u.IsActive
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
        [Route("api/CreateAccount")]
        public async Task<IHttpActionResult> CreateAccount(ClsUser user)
        {
            try
            {
                var query = await (from u in db.M_UserProfile
                                   where u.CPR == user.CPR_Number
                                   select u).FirstOrDefaultAsync();

                if (query == null)
                {
                    var CheckLicense = await (from p in db.M_User
                                              where p.CPR_Number == user.CPR_Number
                                              select p).FirstOrDefaultAsync();

                    if (CheckLicense != null)
                    {
                        var add = db.M_User.Add(new M_User
                        {
                            Email_Address = user.Email_Address,
                            IsActive = true,
                            CPR_Number = user.CPR_Number,
                            CreatedOnUtc = DateTime.Now,
                            Password = user.Password,
                            UpdatedOnUtc = DateTime.Now
                        });

                        await db.SaveChangesAsync();

                        var updateProfile = await (from p in db.M_UserProfile
                                                  where p.CPR == user.CPR_Number
                                                  select p).FirstOrDefaultAsync();

                        if(updateProfile != null)
                        {
                            updateProfile.ID = add.ID;

                            await db.SaveChangesAsync();
                        }

                        return Content(HttpStatusCode.OK, "Account Created");
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotFound, "Membership Not Found");
                    }
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "Account Exist");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("api/ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ClsUser user)
        {
            try
            {
                var query = await (from u in db.M_User
                                   where u.CPR_Number == user.CPR_Number
                                   select u).FirstOrDefaultAsync();

                if (query != null)
                {
                    if (query.Password == user.OldPassword)
                    {

                        query.Password = user.Password;

                        await db.SaveChangesAsync();

                        return Content(HttpStatusCode.OK, "Password Changed");
                    }
                    else
                    {
                        return Content(HttpStatusCode.OK, "Wrong Old Password");
                    }
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "Account Not Fount");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("api/SendAccountPassword")]
        public async Task<IHttpActionResult> SendAccountPassword(ClsUser user)
        {
            try
            {
                var query = await (from u in db.M_User
                                   where u.CPR_Number == user.CPR_Number || u.Email_Address
                                   == user.CPR_Number
                                   select u).FirstOrDefaultAsync();

                if (query != null)
                {
                    var profile = await (from u in db.M_UserProfile
                                         where u.CPR == query.CPR_Number
                                         select u).FirstOrDefaultAsync();

                    string emailSubject = "Dear " + profile.FullName + "," +
                                            "</br>" +
                                            "We have sent you this email response to your request to get your password for BMC App. " +
                                            "</br></br>" +
                                            "Please find below your password:" +
                                            "</br>" +
                                            query.Password +
                                            "</br></br>" +
                                            "If you need help or you have any other questions, feel free to email info@bmcbahrain.com" +
                                            "</br></br>" +
                                            "Thank You," +
                                            "</br>" +
                                            "BMC Bahrain" +
                                            "</br>" +
                                            "<img src=''>";

                    //var sendMail = EmailHelper.SendMail(query.EmailAddress, "BMC APP - FORGOT PASSWORD", emailSubject);

                    return Content(HttpStatusCode.OK, "Sent");
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "Account Not Found");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
