using System;
using Newtonsoft.Json;

namespace BMF_LicenseApp.Models
{

    public class License
    {
        [JsonProperty("Expire_Date")]
        public DateTime ExpireDate { get; set; }

        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("ID_LicenseType")]
        public int IDLicenseType { get; set; }

        [JsonProperty("ID_UserProfile")]
        public int IDUserProfile { get; set; }

        [JsonProperty("License_Number")]
        public string LicenseNumber { get; set; }

        [JsonProperty("LicenseType")]
        public LicenseType LicenseType { get; set; }

        [JsonProperty("Profile")]
        public UserProfile Profile { get; set; }
    }

    public class LicenseType
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Image_Front_URL")]
        public string ImageFrontURL { get; set; }

        [JsonProperty("Image_Back_URL")]
        public string ImageBackURL { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }
    }

    public class UserProfile
    {
        [JsonProperty("CPR")]
        public string CPR { get; set; }

        [JsonProperty("Photo_URL")]
        public string PhotoURL { get; set; }

        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("Blood_Group")]
        public string BloodGroup { get; set; }

        [JsonProperty("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }
    }

}
