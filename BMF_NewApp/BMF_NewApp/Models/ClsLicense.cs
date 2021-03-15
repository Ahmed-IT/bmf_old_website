using System;
using System.Collections.Generic;
using System.Text;

namespace BMF_NewApp.Models
{
    public class ClsLicense
    {
        public int ID { get; set; }
        public int ID_LicenseType { get; set; }
        public int ID_User { get; set; }
        public int License_Number { get; set; }
        public string License_Number_Full { get { return LicenseType.Type + " " + License_Number; } }
        public DateTime Expire_Date { get; set; }
        public ClsLicenseType LicenseType { get; set; }
        public ClsProfile Profile { get; set; }

    }
}
