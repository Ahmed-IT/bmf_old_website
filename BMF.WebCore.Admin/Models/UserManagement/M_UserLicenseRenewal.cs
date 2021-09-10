using AMK.Plugin.JQueryDataTable;
using BMF.Shared.Core.Helpers;
using BMF.Shared.Extensions;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.UserManagement
{
    public partial class M_UserLicenseRenewal : IJQDataTableExtendDBViewModel<M_UserLicenseRenewal, BMFDbContext>, IDBViewModel<M_UserLicenseRenewal, BMFDbContext>
    {
        [NotMapped]
        public static string _controllerName => $"{nameof(M_UserLicenseRenewal)}s".Replace("M_", "");

        [NotMapped]
        public static string _defaultCPR => $"";

        [NotMapped]
        public static string _defaultParentCPR => $"";

        [NotMapped]
        public static string _defaultMedicalForm => $"";

        [NotMapped]
        public static string _defaultNoObjectionLetter => $"";

        [NotMapped]
        [Display(Name = "Approval Status", Prompt = "Approval Status")]
        public string Display_ApprovalStatus => EnumHelper<Enum_ApprovalStatus>.GetDisplayName(ApprovalStatus);

        [NotMapped]
        [Display(Name = "Date Issued", Prompt = "Date Issued")]
        public string Display_DateIssued => DateIssued.ToString("dd-MMM-yyyy");

        [NotMapped]
        [Display(Name = "Date of Expiry", Prompt = "Date of Expiry")]
        public string Display_DateOfExpiry => DateOfExpiry.ToString("dd-MMM-yyyy");

        [NotMapped]
        [Display(Name = "URLs", Prompt = "URLs")]
        public string Display_URLs => (string.IsNullOrWhiteSpace(CPR_URL) ? "" : $"CPR: <a target='_blank' href='{CPR_URL}'>View</a> <br/>") +
            (string.IsNullOrWhiteSpace(Parent_CPR_URL) ? "" : $"Parent CPR: <a target='_blank' href='{Parent_CPR_URL}'>View</a> <br/>") +
            (string.IsNullOrWhiteSpace(MedicalForm_URL) ? "" : $"Medical Form: <a target='_blank' href='{MedicalForm_URL}'>View</a> <br/>") +
            (string.IsNullOrWhiteSpace(NoObjectionLetter_URL) ? "" : $"No Objection Letter: <a target='_blank' href='{NoObjectionLetter_URL}'>View</a> <br/>");

        #region Fields

        #endregion

        #region Ctor

        public M_UserLicenseRenewal()
        {
            //UserLicense = new List<M_UserLicense>();
        }

        #endregion

        #region IJQDataTableExtendDBViewModel

        public string ClassAcronym => nameof(M_UserLicenseRenewal).Replace("M_", "").GetUppercaseLettersAsString();

        public string Secondary_ClassAcronym => ClassAcronym;

        public virtual string DataTableOperations
        {
            get
            {
                var BtnEdit = "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' " +
                    $"data-target='#M_{"Edit"}{ClassAcronym}' data-id='{ID}'><i class='{"fas fa-pencil-alt"}'></i></button>";
                //var BtnEditAttachments= "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' " +
                //    $"data-target='#M_E{"ULRA"}' data-id='{ID}'><i class='{"fas fa-file-pdf"}'></i></button>";
                var BtnDelete = "<button type='button' class='btn btn-danger btn-sm' data-toggle='modal' " +
                    $"data-target='#M_{"Delete"}{ClassAcronym}' data-id='{ID}'><i class='fas fa-trash-alt'></i></button>";
                return $"<div class='btn-group'>{BtnEdit}{BtnDelete}</div>";
            }
        }

        public static List<Column> JQDT_Index()
        {
            var jqColumns = new List<Column>
            {
                new Column(nameof(ID), "ID", searchable: false, orderable: false),
                new Column($"{nameof(UserLicense)}.{nameof(M_UserLicense.UserProfile)}.{nameof(M_UserLicense.UserProfile.CPR)}", "CPR"),
                new Column($"{nameof(UserLicense)}.{nameof(M_UserLicense.UserProfile)}.{nameof(M_UserLicense.UserProfile.FullName)}", "Full Name"),
                new Column($"{nameof(UserLicense)}.{nameof(M_UserLicense.LicenseType)}.{nameof(M_UserLicense.LicenseType.Display_LicenseType)}", "License Type"),
                new Column($"{nameof(UserLicense)}.{nameof(M_UserLicense.License_Number)}", "License Number"),
                new Column($"{nameof(Display_ApprovalStatus)}", "Approval Status"),
                new Column($"{nameof(Display_URLs)}", "URLs"),
                new Column($"{nameof(Display_DateIssued)}", "Date of Issue"),
                new Column($"{nameof(Display_DateOfExpiry)}", "Date of Expiry"),
            };
            return IJQDataTableExtendDBViewModel<M_UserLicenseRenewal, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region IDBViewModel

        public void GetDatabaseValues(M_UserLicenseRenewal Database)
        {
            ID_UserLicense = Database.ID_UserLicense;
            ApprovalStatus = Database.ApprovalStatus;
            DateIssued = Database.DateIssued;
            DateOfExpiry = Database.DateOfExpiry;
            CPR_URL = Database.CPR_URL;
            Parent_CPR_URL = Database.Parent_CPR_URL;
            MedicalForm_URL = Database.MedicalForm_URL;
            NoObjectionLetter_URL = Database.NoObjectionLetter_URL;
        }

        public void SetDatabaseValues(M_UserLicenseRenewal Database)
        {
            Database.ID_UserLicense = ID_UserLicense;
            Database.ApprovalStatus = ApprovalStatus;
            Database.DateIssued = DateIssued;
            Database.DateOfExpiry = DateOfExpiry;
            Database.CPR_URL = CPR_URL;
            Database.Parent_CPR_URL = Parent_CPR_URL;
            Database.MedicalForm_URL = MedicalForm_URL;
            Database.NoObjectionLetter_URL = NoObjectionLetter_URL;
        }

        public void AsDatabaseUpdate(M_UserLicenseRenewal Database)
        {
            SetDatabaseValues(Database);
            UpdateDefaults(Database);
        }

        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<M_UserLicenseRenewal> databaseContext = null)
        {
            if (ID_UserLicense == 0)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(ID_UserLicense)}", "User license not selected.");
            }
        }

        public void GetDefaults(M_UserLicenseRenewal Database)
        {
            ID = Database.ID;
            IsActive = Database.IsActive;
            CreatedOnUtc = Database.CreatedOnUtc;
            CreatedBy = Database.CreatedBy;
            UpdatedOnUtc = Database.UpdatedOnUtc;
            UpdatedBy = Database.UpdatedBy;
            DeletedOnUtc = Database.DeletedOnUtc;
            DeletedBy = Database.DeletedBy;
            RowVersion = Database.RowVersion;
        }

        public void UpdateDefaults(M_UserLicenseRenewal Database)
        {
            Database.IsActive = IsActive;
            Database.DeletedBy = DeletedBy;
            Database.UpdatedBy = UpdatedBy;
            Database.DeletedOnUtc = DeletedOnUtc;
            Database.UpdatedOnUtc = UpdatedOnUtc;
        }

        #endregion
    }
}
