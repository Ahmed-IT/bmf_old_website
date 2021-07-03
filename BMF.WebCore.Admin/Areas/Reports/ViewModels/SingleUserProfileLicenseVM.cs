using AMK.Plugin.JQueryDataTable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMF.WebCore.Admin.Areas.Reports.ViewModels
{
    public partial class SingleUserProfileLicenseVM
    {

        #region Properties

        public long ID { get; set; }
        public string CPR { get; set; }
        public string Email_Address { get; set; }
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Display_DateOfBirth { get; set; }
        public string Mobile_Number { get; set; }
        public string Full_Address { get; set; }
        public string Blood_Group { get; set; }
        public string Place_Of_Birth { get; set; }
        public string EmergencyContact_MobileNumber { get; set; }
        public string EmergencyContact_Name { get; set; }
        public string Parent_CPR { get; set; }
        public string Parent_FullName { get; set; }
        public string SystemRemarks { get; set; }
        public string License_Number { get; set; }
        public DateTime Expire_Date { get; set; }
        public string Display_Expire_Date { get; set; }
        public string LicenseType { get; set; }
        public string MarshalRole { get; set; }

        #endregion

        #region IJQDataTableExtendDBViewModel

        public static List<Column> JQDT_Index()
        {
            var jqColumns = new List<Column>
            {
                new Column(nameof(ID), "ID", searchable: false, orderable: false),
                new Column(nameof(CPR), "CPR"),
                new Column(nameof(Email_Address), "Email"),
                new Column(nameof(FullName), "Full Name"),
                new Column(nameof(Nationality), "Nationality"),
                new Column(nameof(Display_DateOfBirth), "Date Of Birth", orderable: false),
                new Column(nameof(Mobile_Number), "Mobile Number"),
                new Column(nameof(Full_Address), "Full Address"),
                new Column(nameof(Blood_Group), "Blood Group"),
                new Column(nameof(Place_Of_Birth), "Place Of Birth"),
                new Column(nameof(EmergencyContact_MobileNumber), "Emergency Contact Mobile Number"),
                new Column(nameof(EmergencyContact_Name), "Emergency Contact Name"),
                new Column(nameof(Parent_CPR), "Parent CPR"),
                new Column(nameof(Parent_FullName), "Parent Full Name"),
                new Column(nameof(License_Number), "License Number"),
                new Column(nameof(Display_Expire_Date), "Expire Date", orderable: false),
                new Column(nameof(LicenseType), "License Type"),
                new Column(nameof(MarshalRole), "Marshal Role"),
            };
            return jqColumns;
        }

        #endregion
    }
}
