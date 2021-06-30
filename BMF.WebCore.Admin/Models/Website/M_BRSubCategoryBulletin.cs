using AMK.Plugin.JQueryDataTable;
using BMF.Shared.Extensions;
using BMF.Shared.Services.Azure;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BMF.Shared.Entities.Website
{
    public partial class M_BRSubCategoryBulletin : IJQDataTableExtendDBViewModel<M_BRSubCategoryBulletin, BMFDbContext>, IDBViewModel<M_BRSubCategoryBulletin, BMFDbContext>
    {
        [NotMapped]
        public static string _controllerName => $"{nameof(M_BRSubCategoryBulletin)}".Replace("M_", "");

        [NotMapped]
        public static string _defaultPDF => $"https://bmfstorage.blob.core.windows.net/{AzureStorageContainers.BRSubCategoryBulletinPDF}/default.jpg";

        #region Fields

        #endregion

        #region Ctor

        public M_BRSubCategoryBulletin()
        {
        }

        #endregion

        #region IJQDataTableExtendDBViewModel

        public string ClassAcronym => nameof(M_BRSubCategoryBulletin).Replace("M_", "").GetUppercaseLettersAsString();

        public string Secondary_ClassAcronym => ClassAcronym;

        public virtual string DataTableOperations
        {
            get
            {
                var BtnEdit = "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' " +
                    $"data-target='#M_{"Edit"}{ClassAcronym}' data-id='{ID}'><i class='{"fas fa-pencil-alt"}'></i></button>";
                var BtnEditPDF = "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' " +
                    $"data-target='#M_E{"BRSCBPDF"}' data-id='{ID}'><i class='{"fas fa-file-pdf"}'></i></button>";
                var BtnDelete = "<button type='button' class='btn btn-danger btn-sm' data-toggle='modal' " +
                    $"data-target='#M_{"Delete"}{ClassAcronym}' data-id='{ID}'><i class='fas fa-trash-alt'></i></button>";
                return $"<div class='btn-group'>{BtnEdit}{BtnEditPDF}{BtnDelete}</div>";
            }
        }

        public static List<Column> JQDT_Index()
        {
            var jqColumns = new List<Column>
            {
                new Column(nameof(ID), "ID", searchable: false, orderable: false),
                new Column($"{nameof(SubCategory)}.{nameof(M_BRSubCategory.Title)}", "Sub-Category"),
                new Column(nameof(Title), "Title"),
            };
            return IJQDataTableExtendDBViewModel<M_BRSubCategoryBulletin, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region IDBViewModel

        public void GetDatabaseValues(M_BRSubCategoryBulletin Database)
        {
            ID_SubCategory = Database.ID_SubCategory;
            Title = Database.Title;
            PDF_Url = Database.PDF_Url;
        }

        public void SetDatabaseValues(M_BRSubCategoryBulletin Database)
        {
            Database.ID_SubCategory = ID_SubCategory;
            Database.Title = Title;
            Database.PDF_Url = PDF_Url;
        }

        public void AsDatabaseUpdate(M_BRSubCategoryBulletin Database)
        {
            SetDatabaseValues(Database);
            UpdateDefaults(Database);
        }

        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<M_BRSubCategoryBulletin> databaseContext = null)
        {
            if (ID_SubCategory == 0)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(ID_SubCategory)}", "Category must be selected.");
            }
            if (string.IsNullOrWhiteSpace(Title))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Title cannot be empty.");
            }
            else if (Title.Length > 255)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Title cannot contain more than 255 characters.");
            }
            if (databaseContext != null)
            {
                if (ID == 0)
                {
                    if (databaseContext.AsNoTracking().Count(c => c.ID_SubCategory == ID_SubCategory && c.Title.ToLower().Trim() == Title.ToLower().Trim() && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(ID_SubCategory)}", "Category bulletin already exists.");
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Category bulletin already exists.");
                    }
                }
                else
                {
                    if (databaseContext.AsNoTracking().Count(c => c.ID_SubCategory == ID_SubCategory && c.Title.ToLower().Trim() == Title.ToLower().Trim() && c.ID != ID && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(ID_SubCategory)}", "Category bulletin already exists.");
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Category bulletin already exists.");
                    }
                }
            }
        }

        public void GetDefaults(M_BRSubCategoryBulletin Database)
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

        public void UpdateDefaults(M_BRSubCategoryBulletin Database)
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
