using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BMF_WebSite.Models
{
    public class ClsLicRenewForm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please selected compition license category")]
        [Display(Name = "Compition License")]
        [DataType(DataType.Text)]
        public string CompitionLicense { get; set; }

        [Required(ErrorMessage = "Please enter your CPR number")]
        [Display(Name = "CPR")]
        [DataType(DataType.Text)]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Please enter valid CPR number")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please use only numbers")]
        public string CPR { get; set; }

        public string FullName { get; set; }
        public string Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public string EmergencyName { get; set; }
        public string EmergencyNumber { get; set; }
        public string BloodGroup { get; set; }
        public string CPR_Url { get; set; }
        public string Medical_Url { get; set; }
        public string ParentCPR_Url { get; set; }
        public long LicenseCategoryId { get; set; }
        public string ValidateSubmitButton { get; set; }
        public string LicenseType { get; set; }
        public DateTime LicenseExpireDate { get; set; }
        public string ImageUrl { get; set; }
        public string SubmitButton { get; set; }

        [Required(ErrorMessage = "Please select a CPR Photo")]
        //[FileExtensions(Extensions = "jpg|png|jpeg", ErrorMessage = "Please use only jpg,jpeg,png")]
        [Display(Name = "CPR")]
        public HttpPostedFileBase CPRFile { get; set; }

        [Required(ErrorMessage = "Please select medical form")]
        //[FileExtensions(Extensions = "jpg|png|jpeg", ErrorMessage = "Please use only jpg,jpeg,png")]
        [Display(Name = "Medical Form")]
        public HttpPostedFileBase MedicalFile { get; set; }

        [Display(Name = "Parent FullName")]
        [DataType(DataType.Text)]
        public string ParentFullName { get; set; }

        [Display(Name = "Parent CPR number")]
        [DataType(DataType.Text)]
        public string ParentCPR { get; set; }

        [Display(Name = "Parent CPR")]
        public HttpPostedFileBase ParentCPRFile { get; set; }

        [Display(Name = "IsUnder18")]
        public bool IsUnder18 { get; set; }
    }
}