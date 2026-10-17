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
    public partial class M_GalleryCategory : IJQDataTableExtendDBViewModel<M_GalleryCategory, BMFDbContext>, IDBViewModel<M_GalleryCategory, BMFDbContext>
    {
        [NotMapped]
        public static string _controllerName => $"{nameof(M_GalleryCategory)}".Replace("M_", "");

        [NotMapped]
        public static string _defaultImage => $"https://bmfstorage.blob.core.windows.net/{AzureStorageContainers.GalleryCategoryImage}/default.jpg";

        #region Fields

        #endregion

        #region Ctor

        #endregion

        #region IJQDataTableExtendDBViewModel

        public string ClassAcronym => nameof(M_GalleryCategory).Replace("M_", "").GetUppercaseLettersAsString();

        public string Secondary_ClassAcronym => ClassAcronym;

        public virtual string DataTableOperations
        {
            get
            {
                var BtnEdit = "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' " +
                    $"data-target='#M_{"Edit"}{ClassAcronym}' data-id='{ID}'><i class='{"fas fa-pencil-alt"}'></i></button>";
                var BtnEditImages = "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' " +
                    $"data-target='#M_{"EGCI"}' data-id='{ID}'><i class='{"fas fa-images"}'></i></button>";
                var BtnDelete = "<button type='button' class='btn btn-danger btn-sm' data-toggle='modal' " +
                    $"data-target='#M_{"Delete"}{ClassAcronym}' data-id='{ID}'><i class='fas fa-trash-alt'></i></button>";
                return $"<div class='btn-group'>{BtnEdit}{BtnEditImages}{BtnDelete}</div>";
            }
        }

        public static List<Column> JQDT_Index()
        {
            var jqColumns = new List<Column>
            {
                new Column(nameof(ID), "ID", searchable: false, orderable: false),
                new Column(nameof(Title), "Title"),
            };
            return IJQDataTableExtendDBViewModel<M_GalleryCategory, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region IDBViewModel

        public void GetDatabaseValues(M_GalleryCategory Database)
        {
            Title = Database.Title;
            Image_URL = Database.Image_URL;
        }

        public void SetDatabaseValues(M_GalleryCategory Database)
        {
            Database.Title = Title;
            Database.Image_URL = Image_URL;
        }

        public void AsDatabaseUpdate(M_GalleryCategory Database)
        {
            SetDatabaseValues(Database);
            UpdateDefaults(Database);
        }

        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<M_GalleryCategory> databaseContext = null)
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Title cannot be empty.");
            }
            else if (Title.Length > 255)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Title cannot contain more than 255 characters.");
            }
            if (string.IsNullOrWhiteSpace(Image_URL))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Image_URL)}", "Image URL cannot be empty.");
            }
            else if (Image_URL.Length > 1024)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Image_URL)}", "Image URL cannot contain more than 1,000 characters.");
            }
            if (databaseContext != null)
            {
                if (ID == 0)
                {
                    if (databaseContext.AsNoTracking().Count(c => c.Title.ToLower().Trim() == Title.ToLower().Trim() && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Gallery category already exists.");
                    }
                }
                else
                {
                    if (databaseContext.AsNoTracking().Count(c => c.Title.ToLower().Trim() == Title.ToLower().Trim() && c.ID != ID && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Gallery category already exists.");
                    }
                }
            }
        }

        public void GetDefaults(M_GalleryCategory Database)
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

        public void UpdateDefaults(M_GalleryCategory Database)
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
