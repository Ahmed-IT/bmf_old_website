using System;
using System.Collections.Generic;
using System.Text;

namespace BMF_NewApp.Models
{
    public class ClsLicenseType
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Image_Front_URL { get; set; }
        public string Image_Back_URL { get; set; }

        public string LicenseTitleFull
        {
            get
            {
                return Title + " (" + Type + ")";

            }
        }
    }
}
