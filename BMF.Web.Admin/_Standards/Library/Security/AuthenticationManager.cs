using System;
using System.Web;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using _Standards.Library.Security;
using BMF.Web.Admin.Database;

namespace _Standards.Library.Security
{
    [DataContract]
    public class AuthenticationManager
    {
        #region Properties
        private static BMFDbContext _TheDatabase { get; set; }
        //private static List<_SystemRole> SystemRoles { get; set; }

        [DataMember]
        private long ID { get; set; }
        [DataMember]
        private string Name { get; set; }
        //[DataMember]
        //private List<long> Features { get; set; }
        [DataMember]
        private DateTime LoggedIn_Timestamp { get; set; }
        #endregion

        protected static void Initialize_FromDB()
        {
            try
            {
                //var _DataFunctions = new DataFunctions();
                //SystemRoles = _DataFunctions.GetAll_SystemRoles(true, true);
            }
            catch (Exception LogError)
            {
                //SystemRoles = new List<_SystemRole>();
            }
        }

        protected static bool Update_FromDB(DateTime LoggedIn_Timestamp)
        {
            try
            {
                //_TheDatabase = new AccessClass_DB();
                //int Updated_RL = _TheDatabase.Table_SystemRole.Count(w => w.Date_Updated > LoggedIn_Timestamp);
                //int Updated_RU = _TheDatabase.Table_SystemRoleUser.Count(w => w.Date_Updated > LoggedIn_Timestamp);
                //int Updated_RF = _TheDatabase.Table_SystemRoleFeature.Count(w => w.Date_Updated > LoggedIn_Timestamp);
                //if (Updated_RL > 0 || Updated_RU > 0 || Updated_RF > 0)
                //{
                    return true;
                //}
            }
            catch (Exception LogError)
            {
            }
            return false;
        }

        public AuthenticationManager(long ID, string FirstName, string LastName, List<long> Features)
        {
            this.ID = ID;
            //this.Features = Features;
            LoggedIn_Timestamp = DateTime.Now;
            Name = string.Format("{0} {1}", FirstName, LastName);
        }

        public AuthenticationManager()
        {

        }

        public static bool Authenticate(FormBasedUser SignInRequest, out string _EncTicket)
        {
            _EncTicket = string.Empty;
            try
            {
                Initialize_FromDB();
                _TheDatabase = new BMFDbContext();
                var User = _TheDatabase.Table_SystemUser.Where(w => w.Username.ToLower().Trim() == SignInRequest.Username.ToLower().Trim() || w.Email.ToLower().Trim() == SignInRequest.Username.ToLower().Trim()).SingleOrDefault();
                if (User == null)
                {
                    return false;
                }
                else
                {
                    byte[] _Password = Encoding.ASCII.GetBytes(SignInRequest.Password + User.ID);
                    _Password = new System.Security.Cryptography.SHA256Managed().ComputeHash(_Password);
                    if (!string.Equals(Convert.ToBase64String(_Password), User.Password))
                    {
                        return false;
                    }
                    else
                    {
                        //var _User = new _SystemUser(User, true);
                        //var _Features = _User.Roles.SelectMany(s => s.Features.Select(sf => sf.ID)).Distinct().ToList();
                        var _UserData = JsonConvert.SerializeObject(new AuthenticationManager(User.ID, User.FirstName, User.LastName, null));
                        FormsAuthenticationTicket _Ticket = new FormsAuthenticationTicket(1, User.ID.ToString(), DateTime.Now, DateTime.Now.AddHours(1), true, _UserData, FormsAuthentication.FormsCookiePath);
                        _EncTicket = FormsAuthentication.Encrypt(_Ticket);
                        return true;
                    }
                }
            }
            catch (Exception LogError)
            {

            }
            finally
            {
                _TheDatabase.Dispose();
            }
            return false;
        }

        public static void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        #region Session Reading Helpers

        public static bool IsAuthenticated()
        {
            try { return HttpContext.Current.User.Identity.IsAuthenticated; }
            catch (Exception LogError) { }
            return false;
        }

        public static bool IsInRole(string Role)
        {
         
            return false;
        }

        public static long Get_UserID()
        {
            try
            {
                FormsIdentity _Identity = (FormsIdentity)HttpContext.Current.User.Identity;
                FormsAuthenticationTicket _Ticket = _Identity.Ticket;
                var _UserData = JsonConvert.DeserializeObject<AuthenticationManager>(_Ticket.UserData);
                return _UserData.ID;
            }
            catch (Exception LogError)
            {
            }
            return 0;
        }

        public static string Get_DisplayName()
        {
            try
            {
                FormsIdentity _Identity = (FormsIdentity)HttpContext.Current.User.Identity;
                FormsAuthenticationTicket _Ticket = _Identity.Ticket;
                var _UserData = JsonConvert.DeserializeObject<AuthenticationManager>(_Ticket.UserData);
                return _UserData.Name;
            }
            catch (Exception LogError)
            {
            }
            return string.Empty;
        }

        //public static List<long> GetAll_Features()
        //{
        //    try
        //    {
        //        FormsIdentity _Identity = (FormsIdentity)HttpContext.Current.User.Identity;
        //        FormsAuthenticationTicket _Ticket = _Identity.Ticket;
        //        var _UserData = JsonConvert.DeserializeObject<AuthenticationManager>(_Ticket.UserData);
        //        if (Update_FromDB(_UserData.LoggedIn_Timestamp))
        //        {
        //            var User = _TheDatabase.Table_SystemUser.Find(_UserData.ID);
        //            var _User = new _SystemUser(User, true);
        //            _TheDatabase.Dispose();
        //            var _Features = _User.Roles.SelectMany(s => s.Features.Select(sf => sf.ID)).Distinct().ToList();
        //            _UserData.Features = _Features;
        //            _UserData.LoggedIn_Timestamp = DateTime.Now;
        //            _Ticket = new FormsAuthenticationTicket(1, _User.ID.ToString(), DateTime.Now, DateTime.Now.AddHours(1), true, JsonConvert.SerializeObject(_UserData), FormsAuthentication.FormsCookiePath);
        //            string _EncTicket = FormsAuthentication.Encrypt(_Ticket);
        //            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, _EncTicket));
        //        }
        //        return _UserData.Features;
        //    }
        //    catch (Exception LogError)
        //    {
        //    }
        //    return null;
        //}

        #endregion
    }
}
