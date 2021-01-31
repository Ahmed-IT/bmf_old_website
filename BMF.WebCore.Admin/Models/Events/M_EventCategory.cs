using AMK.Plugin.JQueryDataTable;
using BMF.Shared.Extensions;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BMF.Shared.Entities.Events
{
    public partial class M_EventCategory : IJQDataTableExtendDBViewModel<M_EventCategory, BMFDbContext>, IDBViewModel<M_EventCategory, BMFDbContext>
    {
        [NotMapped]
        public static string _controllerName => $"{nameof(M_EventCategory)}".Replace("M_", "");

        [NotMapped]
        public static string _defaultImage => $"https://bmfstorage.blob.core.windows.net/events-category/default.jpg";

        #region Fields

        #endregion

        #region Ctor

        public M_EventCategory()
        {
            EventCategoryRules = new List<M_EventCategoryRules>();
            EventRounds = new List<M_EventRound>();
        }

        #endregion

        #region IJQDataTableExtendDBViewModel

        public string ClassAcronym => nameof(M_EventCategory).GetUppercaseLettersAsString().Replace("M", "");

        public string Secondary_ClassAcronym => ClassAcronym;

        public virtual string DataTableOperations
        {
            get
            {
                var BtnEdit = "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' " +
                    $"data-target='#M_{"Edit"}{ClassAcronym}' data-id='{ID}'><i class='{"fas fa-pencil-alt"}'></i></button>";
                var BtnEditImage = "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' " +
                    $"data-target='#M_{"ECI"}' data-id='{ID}'><i class='{"fas fa-image"}'></i></button>";
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
            return IJQDataTableExtendDBViewModel<M_EventCategory, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region IDBViewModel

        public void GetDatabaseValues(M_EventCategory Database)
        {
            ID_EventSeason = Database.ID_EventSeason;
            Title = Database.Title;
            Image_URL = Database.Image_URL;
        }

        public void SetDatabaseValues(M_EventCategory Database)
        {
            Database.ID_EventSeason = ID_EventSeason;
            Database.Title = Title;
            Database.Image_URL = Image_URL;
        }

        public void AsDatabaseUpdate(M_EventCategory Database)
        {
            SetDatabaseValues(Database);
            UpdateDefaults(Database);
        }

        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<M_EventCategory> databaseContext = null)
        {
            if (ID_EventSeason == 0)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(ID_EventSeason)}", "Season must be selected.");
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
                    if (databaseContext.AsNoTracking().Count(c => c.ID_EventSeason == ID_EventSeason && c.Title.ToLower().Trim() == Title.ToLower().Trim() && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(ID_EventSeason)}", "Event category already exists.");
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Event category already exists.");
                    }
                }
                else
                {
                    if (databaseContext.AsNoTracking().Count(c => c.ID_EventSeason == ID_EventSeason && c.Title.ToLower().Trim() == Title.ToLower().Trim() && c.ID != ID && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(ID_EventSeason)}", "Event category already exists.");
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Event category already exists.");
                    }
                }
            }
        }

        public void GetDefaults(M_EventCategory Database)
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

        public void UpdateDefaults(M_EventCategory Database)
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
