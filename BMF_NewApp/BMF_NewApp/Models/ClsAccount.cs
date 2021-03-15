using System;
using System.Collections.Generic;
using System.Text;

namespace BMF_NewApp.Models
{
    public class ClsAccount
    {
        public int ID { get; set; }
        public string CPR_Number { get; set; }
        public string Email_Address { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
