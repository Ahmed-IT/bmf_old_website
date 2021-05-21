using BMF.Shared.Entities.Location;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BMF.Shared.Entities.UserManagement
{
    public partial class M_UserProfile : BaseEntity
    {
        #region Foreign Keys

        [ForeignKey(nameof(Nationality))]
        [Display(Name = "Nationality", Prompt = "Nationality")]
        [JsonProperty(PropertyName = nameof(ID_Nationality))]
        public long? ID_Nationality { get; set; }

        #endregion

        #region Properties

        [Display(Name = "Full Name", Prompt = "Full Name")]
        [Required]
        [JsonProperty(PropertyName = nameof(FullName))]
        [MaxLength(255)]
        public string FullName { get; set; }

        [Display(Name = "CPR", Prompt = "CPR")]
        [JsonProperty(PropertyName = nameof(CPR))]
        [MaxLength(60)]
        public string CPR { get; set; }

        [Display(Name = "Date Of Birth", Prompt = "Date Of Birth")]
        [JsonProperty(PropertyName = nameof(DateOfBirth))]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Mobile Number", Prompt = "Mobile Number")]
        [JsonProperty(PropertyName = nameof(Mobile_Number))]
        [MaxLength(60)]
        public string Mobile_Number { get; set; }

        [Display(Name = "Full Address", Prompt = "Full Address")]
        [JsonProperty(PropertyName = nameof(Full_Address))]
        [MaxLength(1024)]
        public string Full_Address { get; set; }

        [Display(Name = "Blood Group", Prompt = "Blood Group")]
        [JsonProperty(PropertyName = nameof(Blood_Group))]
        [MaxLength(5)]
        public string Blood_Group { get; set; }

        [Display(Name = "Place Of Birth", Prompt = "Place Of Birth")]
        [JsonProperty(PropertyName = nameof(Place_Of_Birth))]
        [MaxLength(60)]
        public string Place_Of_Birth { get; set; }

        [Display(Name = "Emergency Contact Name", Prompt = "Emergency Contact Name")]
        [MaxLength(255)]
        [JsonProperty(PropertyName = nameof(EmergencyContact_Name))]
        public string EmergencyContact_Name { get; set; }

        [Display(Name = "Emergency Contact Mobile Number", Prompt = "Emergency Contact Mobile Number")]
        [MaxLength(60)]
        [JsonProperty(PropertyName = nameof(EmergencyContact_MobileNumber))]
        public string EmergencyContact_MobileNumber { get; set; }

        [Display(Name = "Parent Full Name", Prompt = "Parent Full Name")]
        [JsonProperty(PropertyName = nameof(Parent_FullName))]
        [MaxLength(255)]
        public string Parent_FullName { get; set; }

        [Display(Name = "Parent CPR", Prompt = "Parent CPR")]
        [JsonProperty(PropertyName = nameof(Parent_CPR))]
        [MaxLength(60)]
        public string Parent_CPR { get; set; }

        [Display(Name = "Photo URL", Prompt = "Photo URL")]
        [JsonProperty(PropertyName = nameof(Photo_URL))]
        [MaxLength(1024)]
        public string Photo_URL { get; set; }

        #endregion

        #region Relationships

        [Display(Name = "Nationality", Prompt = "Nationality")]
        [InverseProperty(nameof(Country.Users))]
        public Country Nationality { get; set; }

        [Display(Name = "User Account", Prompt = "User Account")]
        [InverseProperty(nameof(M_UserAccount.UserProfile))]
        public M_UserAccount UserAccount { get; set; }

        [Display(Name = "User Licenses", Prompt = "User Licenses")]
        [InverseProperty(nameof(M_UserLicense.UserProfile))]
        public ICollection<M_UserLicense> UserLicenses { get; set; }

        [Display(Name = "User Round Sign Ins", Prompt = "User Round Sign Ins")]
        [InverseProperty(nameof(M_UserRoundSignIn.UserProfile))]
        public ICollection<M_UserRoundSignIn> UserRoundSignIns { get; set; }

        #endregion
    }
}
