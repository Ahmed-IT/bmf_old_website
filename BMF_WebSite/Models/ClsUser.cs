using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMF_WebSite.Models
{
    public class ClsUser
    {
        public int ID { get; set; }
        public string Email_Address { get; set; }
        public string CPR_Number { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string OldPassword { get; set; }
        public bool IsActive { get; set; }
    }
}
