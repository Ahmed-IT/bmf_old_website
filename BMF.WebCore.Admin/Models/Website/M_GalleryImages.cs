using AMK.Plugin.JQueryDataTable;
using BMF.Shared.Extensions;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Website
{
    public partial class M_GalleryImages : IJQDataTableExtendDBViewModel<M_GalleryImages, BMFDbContext>, IDBViewModel<M_GalleryImages, BMFDbContext>
    {
        [NotMapped]
        public static string _controllerName => $"{nameof(M_GalleryImages)}".Replace("M_", "");

        #region Fields

        #endregion

        #region Ctor

        #endregion

        #region IJQDataTableExtendDBViewModel

        public string ClassAcronym => nameof(M_GalleryImages).Replace("M_", "").GetUppercaseLettersAsString();

        public string Secondary_ClassAcronym => ClassAcronym;

        public virtual string DataTableOperations
        {
            get
            {
                var BtnEdit = "<button type='button' class='btn btn-primary btn-xs' data-toggle='modal' " +
                    $"data-target='#M_{"Edit"}{ClassAcronym}' data-id='{ID}'><i class='{"fas fa-pencil-alt"}'></i></button>";
                var BtnDelete = "<button type='button' class='btn btn-danger btn-xs' data-toggle='modal' " +
                    $"data-target='#M_{"Delete"}{ClassAcronym}' data-id='{ID}'><i class='fas fa-trash-alt'></i></button>";
                return $"<div class='btn-group'>{BtnEdit}{BtnDelete}</div>";
            }
        }

        public static List<Column> JQDT_Index()
        {
            var jqColumns = new List<Column>
            {
                new Column(nameof(ID), "ID", searchable: false, orderable: false),
                new Column(nameof(Image_URL), "Image URL"),
            };
            return IJQDataTableExtendDBViewModel<M_GalleryImages, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region IDBViewModel

        public void GetDatabaseValues(M_GalleryImages Database)
        {
            ID_GalleryCategory = Database.ID_GalleryCategory;
            Image_URL = Database.Image_URL;
        }

        public void SetDatabaseValues(M_GalleryImages Database)
        {
            Database.ID_GalleryCategory = ID_GalleryCategory;
            Database.Image_URL = Image_URL;
        }

        public void AsDatabaseUpdate(M_GalleryImages Database)
        {
            SetDatabaseValues(Database);
            UpdateDefaults(Database);
        }

        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<M_GalleryImages> databaseContext = null)
        {
            if (ID_GalleryCategory == 0)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(ID_GalleryCategory)}", "Gallery category must be selected.");
            }
            if (string.IsNullOrWhiteSpace(Image_URL))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Image_URL)}", "Image URL cannot be empty.");
            }
            else if (Image_URL.Length > 1024)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Image_URL)}", "Image URL cannot contain more than 1,000 characters.");
            }
        }

        public void GetDefaults(M_GalleryImages Database)
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

        public void UpdateDefaults(M_GalleryImages Database)
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