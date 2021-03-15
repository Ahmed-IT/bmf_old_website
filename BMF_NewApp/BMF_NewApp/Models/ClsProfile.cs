using System;
using System.Collections.Generic;
using System.Text;

namespace BMF_NewApp.Models
{
    public class ClsProfile
    {
        public int ID { get; set; }
        public string CPR { get; set; }
        public string FullName { get; set; }
        public string Photo_URL { get; set; }
        public string Mobile_Number { get; set; }
        public string Nationality { get; set; }
        public string Blood_Group { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ClsAccount Account { get; set; }

        public ClsProfile()
        {
            Account = new ClsAccount();
        }
    }
}
