using AMK.Plugin.JQueryDataTable;
using BMF.Shared.Extensions;
using BMF.Shared.Services.Azure;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BMF.Shared.Entities.Events
{
    public partial class M_EventCategoryBulletins : IJQDataTableExtendDBViewModel<M_EventCategoryBulletins, BMFDbContext>, IDBViewModel<M_EventCategoryBulletins, BMFDbContext>
    {
        [NotMapped]
        public static string _controllerName => $"{nameof(M_EventCategoryBulletins)}".Replace("M_", "");

        [NotMapped]
        public static string _defaultPDF => $"https://bmfstorage.blob.core.windows.net/{AzureStorageContainers.EventCategoryBulletinPDF}/default.jpg";

        #region Fields

        #endregion

        #region Ctor

        public M_EventCategoryBulletins()
        {
        }

        #endregion

        #region IJQDataTableExtendDBViewModel

        public string ClassAcronym => nameof(M_EventCategoryBulletins).Replace("M_", "").GetUppercaseLettersAsString();

        public string Secondary_ClassAcronym => ClassAcronym;

        public virtual string DataTableOperations
        {
            get
            {
                var BtnEdit = "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' " +
                    $"data-target='#M_{"Edit"}{ClassAcronym}' data-id='{ID}'><i class='{"fas fa-pencil-alt"}'></i></button>";
                var BtnEditPDF = "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' " +
                    $"data-target='#M_E{"ECBPDF"}' data-id='{ID}'><i class='{"fas fa-image"}'></i></button>";
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
                new Column($"{nameof(EventCategory)}.{nameof(M_EventCategory.Title)}", "Event Category"),
                new Column(nameof(Title), "Title"),
            };
            return IJQDataTableExtendDBViewModel<M_EventCategoryBulletins, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region IDBViewModel

        public void GetDatabaseValues(M_EventCategoryBulletins Database)
        {
            ID_EventCategory = Database.ID_EventCategory;
            Title = Database.Title;
            PDF_Url = Database.PDF_Url;
        }

        public void SetDatabaseValues(M_EventCategoryBulletins Database)
        {
            Database.ID_EventCategory = ID_EventCategory;
            Database.Title = Title;
            Database.PDF_Url = PDF_Url;
        }

        public void AsDatabaseUpdate(M_EventCategoryBulletins Database)
        {
            SetDatabaseValues(Database);
            UpdateDefaults(Database);
        }

        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<M_EventCategoryBulletins> databaseContext = null)
        {
            if (ID_EventCategory == 0)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(ID_EventCategory)}", "Event category must be selected.");
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
                    if (databaseContext.AsNoTracking().Count(c => c.ID_EventCategory == ID_EventCategory && c.Title.ToLower().Trim() == Title.ToLower().Trim() && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(ID_EventCategory)}", "Event category bulletin already exists.");
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Event category bulletin already exists.");
                    }
                }
                else
                {
                    if (databaseContext.AsNoTracking().Count(c => c.ID_EventCategory == ID_EventCategory && c.Title.ToLower().Trim() == Title.ToLower().Trim() && c.ID != ID && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(ID_EventCategory)}", "Event category bulletin already exists.");
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Event category bulletin already exists.");
                    }
                }
            }
        }

        public void GetDefaults(M_EventCategoryBulletins Database)
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

        public void UpdateDefaults(M_EventCategoryBulletins Database)
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
