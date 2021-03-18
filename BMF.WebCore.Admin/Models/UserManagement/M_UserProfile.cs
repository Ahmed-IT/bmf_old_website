using AMK.Plugin.JQueryDataTable;
using BMF.Shared.Extensions;
using BMF.Shared.Services.Azure;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BMF.Shared.Entities.UserManagement
{
    public partial class M_UserProfile : IJQDataTableExtendDBViewModel<M_UserProfile, BMFDbContext>, IDBViewModel<M_UserProfile, BMFDbContext>
    {
        [NotMapped]
        public static string _controllerName => $"{nameof(M_UserProfile)}s".Replace("M_", "");

        [NotMapped]
        public static string _defaultImage => $"https://bmfstorage.blob.core.windows.net/{AzureStorageContainers.UserProfile}/default.jpg";

        [NotMapped]
        public string Display_DateOfBirth => DateOfBirth.ToString("dd-MMM-yyyy");

        #region Fields

        #endregion

        #region Ctor

        public M_UserProfile()
        {
            //UserLicense = new List<M_UserLicense>();
        }

        #endregion

        #region IJQDataTableExtendDBViewModel

        public string ClassAcronym => nameof(M_UserProfile).Replace("M_", "").GetUppercaseLettersAsString();

        public string Secondary_ClassAcronym => ClassAcronym;

        public virtual string DataTableOperations
        {
            get
            {
                var BtnEdit = "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' " +
                    $"data-target='#M_{"Edit"}{ClassAcronym}' data-id='{ID}'><i class='{"fas fa-pencil-alt"}'></i></button>";
                var BtnEditImage = "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' " +
                    $"data-target='#M_E{"UPI"}' data-id='{ID}'><i class='{"fas fa-image"}'></i></button>";
                var BtnDelete = "<button type='button' class='btn btn-danger btn-sm' data-toggle='modal' " +
                    $"data-target='#M_{"Delete"}{ClassAcronym}' data-id='{ID}'><i class='fas fa-trash-alt'></i></button>";
                return $"<div class='btn-group'>{BtnEdit}{BtnEditImage}{BtnDelete}</div>";
            }
        }

        public static List<Column> JQDT_Index()
        {
            var jqColumns = new List<Column>
            {
                new Column(nameof(ID), "ID", searchable: false, orderable: false),
                new Column(nameof(FullName), "Full Name"),
                new Column(nameof(CPR), "CPR"),
            };
            return IJQDataTableExtendDBViewModel<M_UserProfile, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region IDBViewModel

        public void GetDatabaseValues(M_UserProfile Database)
        {
            ID_Nationality = Database.ID_Nationality;
            FullName = Database.FullName;
            CPR = Database.CPR;
            DateOfBirth = Database.DateOfBirth;
            Mobile_Number = Database.Mobile_Number;
            Full_Address = Database.Full_Address;
            Blood_Group = Database.Blood_Group;
            Place_Of_Birth = Database.Place_Of_Birth;
            Photo_URL = Database.Photo_URL;
        }

        public void SetDatabaseValues(M_UserProfile Database)
        {
            Database.ID_Nationality = ID_Nationality;
            Database.FullName = FullName;
            Database.CPR = CPR;
            Database.DateOfBirth = DateOfBirth;
            Database.Mobile_Number = Mobile_Number;
            Database.Full_Address = Full_Address;
            Database.Blood_Group = Blood_Group;
            Database.Place_Of_Birth = Place_Of_Birth;
            Database.Photo_URL = Photo_URL;
        }

        public void AsDatabaseUpdate(M_UserProfile Database)
        {
            SetDatabaseValues(Database);
            UpdateDefaults(Database);
        }

        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<M_UserProfile> databaseContext = null)
        {
            if (string.IsNullOrWhiteSpace(FullName))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(FullName)}", "Full name cannot be empty.");
            }
            else if (FullName.Length > 120)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(FullName)}", "Full name contain more than 120 characters.");
            }
            if (databaseContext != null)
            {
                if (ID == 0)
                {
                    if (databaseContext.AsNoTracking().Count(c => c.CPR.ToLower().Trim() == CPR.ToLower().Trim() && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(CPR)}", "CPR already exists.");
                    }
                }
                else
                {
                    if (databaseContext.AsNoTracking().Count(c => c.CPR.ToLower().Trim() == CPR.ToLower().Trim() && c.ID != ID && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(CPR)}", "CPR already exists.");
                    }
                }
            }
        }

        public void GetDefaults(M_UserProfile Database)
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

        public void UpdateDefaults(M_UserProfile Database)
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
