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
            if (user == null || !ModelState.IsValid || !IsSingleEmailAddress(user.Email_Address))
                return Content(HttpStatusCode.BadRequest, "A valid email address is required.");

            var emailAddress = user.Email_Address.Trim();
            try
            {
                var matches = await FindPasswordRecoveryAccountsAsync(emailAddress.ToLowerInvariant());
                var accounts = matches.Where(candidate => candidate.IsActive && candidate.DeletedOnUtc == null).ToList();
                if (accounts.Count == 0)
                    return Content(HttpStatusCode.NotFound, "Account Not Found");
                if (accounts.Count > 1)
                    return Content(HttpStatusCode.Conflict, "More than one active account uses this email address. Please contact BMF support.");

                var account = accounts[0];
                // Always use the registered recipient and stored credentials, never request-supplied account details.
                var recipient = account.Email_Address == null ? null : account.Email_Address.Trim();
                if (!IsSingleEmailAddress(recipient) || !string.Equals(recipient, emailAddress, StringComparison.OrdinalIgnoreCase)
                    || string.IsNullOrWhiteSpace(account.Password))
                    return Content(HttpStatusCode.InternalServerError, "Unable to process the password request. Please contact BMF support.");

                try
                {
                    await SendPasswordRecoveryEmailAsync(account, recipient);
                }
                catch (MailKit.Net.Smtp.SmtpCommandException ex)
                {
                    System.Diagnostics.Trace.TraceError("BMF password email rejected ({0}).", ex.GetType().Name);
                    if ((int)ex.StatusCode >= 400 && (int)ex.StatusCode < 500)
                        return Content(HttpStatusCode.ServiceUnavailable, "Password recovery email is temporarily unavailable. Please try again later.");
                    return Content(HttpStatusCode.BadGateway, "The email server could not accept the password email. Please contact BMF support.");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.TraceError("BMF password email unavailable ({0}).", ex.GetType().Name);
                    return Content(HttpStatusCode.ServiceUnavailable, "Password recovery email is temporarily unavailable. Please try again later.");
                }

                return Content(HttpStatusCode.OK, "Sent");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("BMF password recovery lookup failed ({0}).", ex.GetType().Name);
                return Content(HttpStatusCode.InternalServerError, "Unable to process the password request. Please try again later.");
            }
        }

        protected virtual Task<List<M_UserAccount>> FindPasswordRecoveryAccountsAsync(string normalizedEmail)
        {
            return db.M_UserAccount.Include(account => account.M_UserProfile)
                .Where(account => account.Email_Address != null && account.Email_Address.Trim().ToLower() == normalizedEmail
                    && account.IsActive && account.DeletedOnUtc == null)
                .Take(2).ToListAsync();
        }

        protected virtual Task SendPasswordRecoveryEmailAsync(M_UserAccount account, string recipient)
        {
            return PasswordEmailService.SendAsync(account.M_UserProfile == null ? null : account.M_UserProfile.FullName,
                recipient, account.Password);
        }

        private static bool IsSingleEmailAddress(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            var email = value.Trim();
            if (email.Length > 254 || !new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email)) return false;
            try
            {
                return string.Equals(new System.Net.Mail.MailAddress(email).Address, email, StringComparison.OrdinalIgnoreCase);
            }
            catch (FormatException) { return false; }
        }
    }
}
