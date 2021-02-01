using AMK.Plugin.JQueryDataTable;
using BMF.Shared.Extensions;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BMF.Shared.Entities.Website
{
    public partial class M_News : IJQDataTableExtendDBViewModel<M_News, BMFDbContext>, IDBViewModel<M_News, BMFDbContext>
    {
        [NotMapped]
        public static string _controllerName => $"{nameof(M_News)}".Replace("M_", "");

        [NotMapped]
        public static string _defaultImage => $"https://bmfstorage.blob.core.windows.net/websitecms-news/default.jpg";

        #region Fields

        [NotMapped]
        public string Display_NewsTimestampUtc => NewsTimestampUtc.ToString("dd-MMM-yyyy hh:mm tt");

        #endregion

        #region Ctor

        public M_News()
        {
            NewsTimestampUtc = DateTime.UtcNow;
        }

        #endregion

        #region IJQDataTableExtendDBViewModel

        public string ClassAcronym => nameof(M_News).GetUppercaseLettersAsString().Replace("M", "");

        public string Secondary_ClassAcronym => ClassAcronym;

        public virtual string DataTableOperations
        {
            get
            {
                var BtnEdit = "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' " +
                    $"data-target='#M_{"Edit"}{ClassAcronym}' data-id='{ID}'><i class='{"fas fa-pencil-alt"}'></i></button>";
                var BtnEditImage = "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' " +
                    $"data-target='#M_{"ENI"}' data-id='{ID}'><i class='{"fas fa-image"}'></i></button>";
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
                new Column(nameof(Title), "Title"),
            };
            return IJQDataTableExtendDBViewModel<M_News, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region IDBViewModel

        public void GetDatabaseValues(M_News Database)
        {
            Title = Database.Title;
            Content = Database.Content;
            NewsTimestampUtc = Database.NewsTimestampUtc;
            Image_URL = Database.Image_URL;
        }

        public void SetDatabaseValues(M_News Database)
        {
            Database.Title = Title;
            Database.Content = Content;
            Database.NewsTimestampUtc = NewsTimestampUtc;
            Database.Image_URL = Image_URL;
        }

        public void AsDatabaseUpdate(M_News Database)
        {
            SetDatabaseValues(Database);
            UpdateDefaults(Database);
        }

        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<M_News> databaseContext = null)
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Title cannot be empty.");
            }
            else if (Title.Length > 255)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Title cannot contain more than 255 characters.");
            }
            if (string.IsNullOrWhiteSpace(Content))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Content)}", "Content cannot be empty.");
            }
            else if (Content.Length > 3000)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Content)}", "Content cannot contain more than 3,000 characters.");
            }
            if (databaseContext != null)
            {
                if (ID == 0)
                {
                    if (databaseContext.AsNoTracking().Count(c => c.Title.ToLower().Trim() == Title.ToLower().Trim() && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "News already exists.");
                    }
                }
                else
                {
                    if (databaseContext.AsNoTracking().Count(c => c.Title.ToLower().Trim() == Title.ToLower().Trim() && c.ID != ID && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "News already exists.");
                    }
                }
            }
        }

        public void GetDefaults(M_News Database)
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

        public void UpdateDefaults(M_News Database)
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
