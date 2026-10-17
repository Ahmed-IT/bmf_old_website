using AMK.Plugin.JQueryDataTable;
using BMF.Shared.Extensions;
using BMF.Shared.Services.Azure;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BMF.Shared.Entities.UserManagement
{
    public partial class D_LicenseType : IJQDataTableExtendDBViewModel<D_LicenseType, BMFDbContext>, IDBViewModel<D_LicenseType, BMFDbContext>
    {
        [NotMapped]
        public static string _controllerName => $"{nameof(D_LicenseType)}s".Replace("D_", "");

        [NotMapped]
        public static string _defaultImage => $"https://bmfstorage.blob.core.windows.net/{AzureStorageContainers.LicenseType}/default.jpg";

        [NotMapped]
        [JsonProperty(nameof(Display_LicenseType))]
        public string Display_LicenseType => $"{Title} (Type: {Type})";

        #region Fields

        #endregion

        #region Ctor

        public D_LicenseType()
        {
            //UserLicense = new List<M_UserLicense>();
        }

        #endregion

        #region IJQDataTableExtendDBViewModel

        public string ClassAcronym => nameof(D_LicenseType).Replace("D_", "").GetUppercaseLettersAsString();

        public string Secondary_ClassAcronym => ClassAcronym;

        public virtual string DataTableOperations
        {
            get
            {
                var BtnEdit = "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' " +
                    $"data-target='#M_{"Edit"}{ClassAcronym}' data-id='{ID}'><i class='{"fas fa-pencil-alt"}'></i></button>";
                var BtnEditImage = "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' " +
                    $"data-target='#M_E{"LTI"}' data-id='{ID}'><i class='{"fas fa-image"}'></i></button>";
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
                new Column(nameof(Type), "Type"),
                new Column(nameof(Title), "Title"),
            };
            return IJQDataTableExtendDBViewModel<D_LicenseType, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region IDBViewModel

        public void GetDatabaseValues(D_LicenseType Database)
        {
            Type = Database.Type;
            Title = Database.Title;
            Image_Front_URL = Database.Image_Front_URL;
            Image_Back_URL = Database.Image_Back_URL;
        }

        public void SetDatabaseValues(D_LicenseType Database)
        {
            Database.Type = Type;
            Database.Title = Title;
            Database.Image_Front_URL = Image_Front_URL;
            Database.Image_Back_URL = Image_Back_URL;
        }

        public void AsDatabaseUpdate(D_LicenseType Database)
        {
            SetDatabaseValues(Database);
            UpdateDefaults(Database);
        }

        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<D_LicenseType> databaseContext = null)
        {
            if (string.IsNullOrWhiteSpace(Type))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Type)}", "Type cannot be empty.");
            }
            else if (Type.Length > 60)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Type)}", "Type cannot contain more than 60 characters.");
            }
            if (string.IsNullOrWhiteSpace(Title))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Title cannot be empty.");
            }
            else if (Title.Length > 120)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Title cannot contain more than 120 characters.");
            }
            if (databaseContext != null)
            {
                if (ID == 0)
                {
                    if (databaseContext.AsNoTracking().Count(c => c.Type.ToLower().Trim() == Type.ToLower().Trim() && c.Title.ToLower().Trim() == Title.ToLower().Trim() && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Type)}", "License type already exists.");
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "License type already exists.");
                    }
                }
                else
                {
                    if (databaseContext.AsNoTracking().Count(c => c.Type.ToLower().Trim() == Type.ToLower().Trim() && c.Title.ToLower().Trim() == Title.ToLower().Trim() && c.ID != ID && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Type)}", "License type already exists.");
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "License type already exists.");
                    }
                }
            }
        }

        public void GetDefaults(D_LicenseType Database)
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

        public void UpdateDefaults(D_LicenseType Database)
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
