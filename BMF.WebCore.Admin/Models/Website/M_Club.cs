using AMK.Plugin.JQueryDataTable;
using BMF.Shared.Extensions;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BMF.Shared.Entities.Website
{
    public partial class M_Club : IJQDataTableExtendDBViewModel<M_Club, BMFDbContext>, IDBViewModel<M_Club, BMFDbContext>
    {
        [NotMapped]
        public static string _controllerName => $"{nameof(M_Club)}".Replace("M_", "");

        #region Fields

        #endregion

        #region Ctor

        #endregion

        #region IJQDataTableExtendDBViewModel

        public string ClassAcronym => nameof(M_Club).GetUppercaseLettersAsString().Replace("M_", "");

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
                new Column(nameof(Title), "Title"),
            };
            return IJQDataTableExtendDBViewModel<M_Club, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region IDBViewModel

        public void GetDatabaseValues(M_Club Database)
        {
            Title = Database.Title;
            Description = Database.Description;
            Website_Link = Database.Website_Link;
            Image_URL = Database.Image_URL;
        }

        public void SetDatabaseValues(M_Club Database)
        {
            Database.Title = Title;
            Database.Description = Description;
            Database.Website_Link = Website_Link;
            Database.Image_URL = Image_URL;
        }

        public void AsDatabaseUpdate(M_Club Database)
        {
            SetDatabaseValues(Database);
            UpdateDefaults(Database);
        }

        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<M_Club> databaseContext = null)
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Title cannot be empty.");
            }
            else if (Title.Length > 255)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Title cannot contain more than 255 characters.");
            }
            if (string.IsNullOrWhiteSpace(Website_Link))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Website_Link)}", "Website link cannot be empty.");
            }
            else if (Website_Link.Length > 255)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Website_Link)}", "Website link cannot contain more than 255 characters.");
            }
            if (databaseContext != null)
            {
                if (ID == 0)
                {
                    if (databaseContext.AsNoTracking().Count(c => c.Title.ToLower().Trim() == Title.ToLower().Trim() && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Club already exists.");
                    }
                }
                else
                {
                    if (databaseContext.AsNoTracking().Count(c => c.Title.ToLower().Trim() == Title.ToLower().Trim() && c.ID != ID && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Club already exists.");
                    }
                }
            }
        }

        public void GetDefaults(M_Club Database)
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

        public void UpdateDefaults(M_Club Database)
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
