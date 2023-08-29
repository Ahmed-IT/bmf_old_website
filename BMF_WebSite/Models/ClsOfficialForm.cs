using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BMF_WebSite.Models
{
    public class ClsOfficialForm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your CPR/Passport number")]
        [Display(Name = "CPR")]
        [DataType(DataType.Text)]
        //[StringLength(9, MinimumLength = 9, ErrorMessage = "Please enter valid CPR number")]
        //[RegularExpression("([0-9]+)", ErrorMessage = "Please use only numbers")]
        public string CPR { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = "First Name")]
        [RegularExpression(@"([a-zA-Z\s]+)", ErrorMessage = "Special characters not allowed")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your middle name")]
        [Display(Name = "Middle Name")]
        [RegularExpression(@"([a-zA-Z\s]+)", ErrorMessage = "Special characters not allowed")]
        [DataType(DataType.Text)]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [Display(Name = "Last Name")]
        [RegularExpression(@"([a-zA-Z\s]+)", ErrorMessage = "Special characters not allowed")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Please select license type")]
        [Display(Name = "License Type")]
        [DataType(DataType.Text)]
        public string LicenseType { get; set; }

        //[Required(ErrorMessage = "Please select Nationality")]
        [Display(Name = "Nationality")]
        [DataType(DataType.Text)]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Please enter house/buildig number")]
        [Display(Name = "House Number")]
        [DataType(DataType.Text)]
        public string HouseNumber { get; set; }

        [Required(ErrorMessage = "Please enter road number")]
        [Display(Name = "Road Number")]
        [DataType(DataType.Text)]
        public string RoadNumber { get; set; }

        [Required(ErrorMessage = "Please enter block number")]
        [Display(Name = "Block Number")]
        [DataType(DataType.Text)]
        public string BlockNumber { get; set; }

        [Required(ErrorMessage = "Please enter City")]
        [Display(Name = "City")]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter place of birth")]
        [Display(Name = "Place of Birth")]
        [DataType(DataType.Text)]
        public string PlaceOfBirth { get; set; }

        [Required(ErrorMessage = "Please enter date of birth")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please enter your mobile number")]
        [Display(Name = "Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter emergency contact name")]
        [Display(Name = "Emergency Name")]
        [DataType(DataType.Text)]
        public string EmergencyName { get; set; }

        [Required(ErrorMessage = "Please enter emergency contact number")]
        [Display(Name = "Emergency Number")]
        [DataType(DataType.Text)]
        public string EmergencyNumber { get; set; }

        [Required(ErrorMessage = "Please select blood group")]
        [Display(Name = "Blood Group")]
        [DataType(DataType.Text)]
        public string BloodGroup { get; set; }

        [Required(ErrorMessage = "Please select photo")]
        //[FileExtensions(Extensions = "jpg|png|jpeg", ErrorMessage = "Please use only jpg,jpeg,png")]
        [Display(Name = "Photo")]
        public HttpPostedFileBase Photo { get; set; }

        public string Photo_Url { get; set; }

        [Required(ErrorMessage = "Please select nationality")]
        [Display(Name = "Nationality")]
        public long CountryId { get; set; }

        [Required(ErrorMessage = "Please select license request type")]
        [Display(Name = "License Request Type")]
        public string LicenseRequestType { get; set; }

        [Required(ErrorMessage = "Please accept the rules and regulations")]
        public bool ChkAccept { get; set; }

    }
}