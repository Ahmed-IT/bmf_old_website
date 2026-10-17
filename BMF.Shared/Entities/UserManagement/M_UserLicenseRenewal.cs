using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.UserManagement
{
    public partial class M_UserLicenseRenewal : BaseEntity
    {
        #region Foreign Keys

        [ForeignKey(nameof(UserLicense))]
        [Display(Name = "User License ", Prompt = "User License ")]
        [Required]
        [JsonProperty(PropertyName = nameof(ID_UserLicense))]
        public long ID_UserLicense { get; set; }

        #endregion

        #region Properties

        [Display(Name = "Approval Status", Prompt = "Approval Status")]
        [Required]
        [JsonProperty(PropertyName = nameof(ApprovalStatus))]
        public Enum_ApprovalStatus ApprovalStatus { get; set; }

        [Display(Name = "Date Issued", Prompt = "Date Issued")]
        [Required]
        [JsonProperty(PropertyName = nameof(DateIssued))]
        public DateTime DateIssued { get; set; }

        [Display(Name = "Date of Expiry", Prompt = "Date of Expiry")]
        [Required]
        [JsonProperty(PropertyName = nameof(DateOfExpiry))]
        public DateTime DateOfExpiry { get; set; }

        // Attachments
        
        [Display(Name = "CPR", Prompt = "CPR")]
        [JsonProperty(PropertyName = nameof(CPR_URL))]
        [MaxLength(1024)]
        public string CPR_URL { get; set; }

        [Display(Name = "Parent CPR", Prompt = "Parent CPR")]
        [JsonProperty(PropertyName = nameof(Parent_CPR_URL))]
        [MaxLength(1024)]
        public string Parent_CPR_URL { get; set; }

        [Display(Name = "Medical Form", Prompt = "Medical Form")]
        [JsonProperty(PropertyName = nameof(MedicalForm_URL))]
        [MaxLength(1024)]
        public string MedicalForm_URL { get; set; }

        [Display(Name = "No Objection Letter", Prompt = "No Objection Letter")]
        [JsonProperty(PropertyName = nameof(NoObjectionLetter_URL))]
        [MaxLength(1024)]
        public string NoObjectionLetter_URL { get; set; }

        #endregion

        #region Relationships

        [Display(Name = "User License ", Prompt = "User License ")]
        [InverseProperty(nameof(M_UserLicense.UserLicenseRenewals))]
        public M_UserLicense UserLicense { get; set; }

        #endregion
    }
}
