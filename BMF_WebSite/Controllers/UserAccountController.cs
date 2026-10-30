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
        BMFEntities1 db = new BMFEntities1();

        [HttpPost]
        [Route("api/AccountAuth")]
        public async Task<IHttpActionResult> AccountAuth(ClsUser user)
        {
            try
            {
                if(user != null)
                {
                    if (user.Email_Address != null && user.Password != null)
                    {
                        var query = await (from u in db.M_UserAccount
                                           where (u.Email_Address == user.Email_Address || u.M_UserProfile.CPR == user.Email_Address)
                                           && u.Password == user.Password && u.IsActive == true
                                           && u.DeletedOnUtc == null
                                           select new
                                           {
                                               u.Email_Address,
                                               u.ID,
                                               CPR_Number = u.M_UserProfile.CPR,
                                               u.IsActive
                                           }).FirstOrDefaultAsync();

                        if (query != null)
                        {
                            return Ok(query);
                        }
                        else
                        {
                            return Content(HttpStatusCode.NotFound, "Not Found");
                        }
                    }
                    else
                    {
                        return Content(HttpStatusCode.InternalServerError, "No Parameters");
                    }
                }
                else
                {
                    return Content(HttpStatusCode.InternalServerError, "No Parameters");
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

                if (query != null)
                {
                    var CheckAccount = await (from p in db.M_UserAccount
                                              where p.Email_Address == user.Email_Address
                                              select p).FirstOrDefaultAsync();

                    if (CheckAccount == null)
                    {
                        var add = db.M_UserAccount.Add(new M_UserAccount
                        {
                            ID = query.ID,
                            Email_Address = user.Email_Address,
                            IsActive = true,
                            CreatedOnUtc = DateTime.Now,
                            Password = user.Password,
                            UpdatedOnUtc = DateTime.Now
                        });

                        await db.SaveChangesAsync();

                        return Content(HttpStatusCode.OK, "Created");
                    }
                    else
                    {
                        return Content(HttpStatusCode.Conflict, "Exist");
                    }
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

        [HttpPost]
        [Route("api/ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ClsUser user)
        {
            try
            {
                var query = await (from u in db.M_UserAccount
                                   where u.Email_Address == user.Email_Address
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
            if (user == null || string.IsNullOrWhiteSpace(user.Email_Address) || !ModelState.IsValid)
                return BadRequest("A valid email address is required.");

            var emailAddress = user.Email_Address.Trim();
            if (!new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(emailAddress))
                return BadRequest("A valid email address is required.");

            try
            {
                var query = await (from u in db.M_UserAccount
                                   where u.Email_Address
                                   == emailAddress
                                   && u.IsActive == true && u.DeletedOnUtc == null
                                   select u).FirstOrDefaultAsync();

                if (query != null)
                {
                    var profile = await (from u in db.M_UserProfile
                                         where u.ID == query.ID
                                         select u).FirstOrDefaultAsync();

                    await PasswordEmailService.SendAsync(profile == null ? null : profile.FullName,
                        query.Email_Address, query.Password);

                    return Content(HttpStatusCode.OK, "Sent");
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "Account Not Found");
                }
            }
            catch (Exception ex)
            {
                // Do not return SMTP diagnostics or account details to the caller.
                System.Diagnostics.Trace.TraceError("BMF password email failed ({0}).", ex.GetType().Name);
                return Content(HttpStatusCode.InternalServerError, "Unable to send the password email. Please try again later.");
            }
        }

    }
}
