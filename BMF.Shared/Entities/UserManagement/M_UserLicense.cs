using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BMF.Shared.Entities.UserManagement
{
    public partial class M_UserLicense : BaseEntity
    {
        #region Foreign Keys

        [ForeignKey(nameof(UserProfile))]
        [Display(Name = "User Profile", Prompt = "User Profile")]
        [JsonProperty(PropertyName = nameof(ID_UserProfile))]
        [Required]
        public long ID_UserProfile { get; set; }

        [ForeignKey(nameof(LicenseType))]
        [Display(Name = "License Type", Prompt = "License Type")]
        [JsonProperty(PropertyName = nameof(ID_LicenseType))]
        [Required]
        public long ID_LicenseType { get; set; }

        #endregion

        #region Properties

        [Display(Name = "License Number", Prompt = "License Number")]
        [Required]
        [JsonProperty(PropertyName = nameof(License_Number))]
        [MaxLength(50)]
        public string License_Number { get; set; }

        [Display(Name = "Expiry Date", Prompt = "Expiry Date")]
        [Required]
        [JsonProperty(PropertyName = nameof(Expire_Date))]
        public DateTime Expire_Date { get; set; }

        #endregion

        #region Relationships

        [Display(Name = "User Profile", Prompt = "User Profile")]
        [InverseProperty(nameof(M_UserProfile.UserLicenses))]
        public M_UserProfile UserProfile { get; set; }

        [Display(Name = "License Type", Prompt = "License Type")]
        [InverseProperty(nameof(D_LicenseType.UserLicenses))]
        public D_LicenseType LicenseType { get; set; }

        [Display(Name = "User Round Sign Ins", Prompt = "User Round Sign Ins")]
        [InverseProperty(nameof(M_UserRoundSignIn.UserLicense))]
        public ICollection<M_UserRoundSignIn> UserRoundSignIns { get; set; }

        [Display(Name = "License Renewals", Prompt = "License Renewals")]
        [InverseProperty(nameof(M_UserLicenseRenewal.UserLicense))]
        public ICollection<M_UserLicenseRenewal> UserLicenseRenewal { get; set; }

        #endregion
    }
}
