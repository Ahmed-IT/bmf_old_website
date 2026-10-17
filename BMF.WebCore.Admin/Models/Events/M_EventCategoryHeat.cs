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
    public partial class M_EventCategoryHeat : IJQDataTableExtendDBViewModel<M_EventCategoryHeat, BMFDbContext>, IDBViewModel<M_EventCategoryHeat, BMFDbContext>
    {
        [NotMapped]
        public static string _controllerName => $"{nameof(M_EventCategoryHeat)}".Replace("M_", "");

        #region Fields

        #endregion

        #region Ctor

        public M_EventCategoryHeat()
        {
        }

        #endregion

        #region IJQDataTableExtendDBViewModel

        public string ClassAcronym => nameof(M_EventCategoryHeat).Replace("M_", "").GetUppercaseLettersAsString();

        public string Secondary_ClassAcronym => ClassAcronym;

        public virtual string DataTableOperations
        {
            get
            {
                var BtnEdit = "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' " +
                    $"data-target='#M_{"Edit"}{ClassAcronym}' data-id='{ID}'><i class='{"fas fa-pencil-alt"}'></i></button>";
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
                new Column($"{nameof(EventCategory)}.{nameof(M_EventCategory.Title)}", "Event Category"),
                new Column(nameof(Name), "Heat Name"),
            };
            return IJQDataTableExtendDBViewModel<M_EventCategoryHeat, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region IDBViewModel

        public void GetDatabaseValues(M_EventCategoryHeat Database)
        {
            ID_EventCategory = Database.ID_EventCategory;
            Name = Database.Name;
        }

        public void SetDatabaseValues(M_EventCategoryHeat Database)
        {
            Database.ID_EventCategory = ID_EventCategory;
            Database.Name = Name;
        }

        public void AsDatabaseUpdate(M_EventCategoryHeat Database)
        {
            SetDatabaseValues(Database);
            UpdateDefaults(Database);
        }

        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<M_EventCategoryHeat> databaseContext = null)
        {
            if (ID_EventCategory == 0)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(ID_EventCategory)}", "Event category must be selected.");
            }
            if (string.IsNullOrWhiteSpace(Name))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Name)}", "Name cannot be empty.");
            }
            if (databaseContext != null)
            {
                if (ID == 0)
                {
                    if (databaseContext.AsNoTracking().Count(c => c.ID_EventCategory == ID_EventCategory && c.Name.ToLower().Trim() == Name.ToLower().Trim() && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(ID_EventCategory)}", "Event category heat already exists.");
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Name)}", "Event category heat already exists.");
                    }
                }
                else
                {
                    if (databaseContext.AsNoTracking().Count(c => c.ID_EventCategory == ID_EventCategory && c.Name.ToLower().Trim() == Name.ToLower().Trim() && c.ID != ID && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(ID_EventCategory)}", "Event category heat already exists.");
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Name)}", "Event category heat already exists.");
                    }
                }
            }
        }

        public void GetDefaults(M_EventCategoryHeat Database)
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

        public void UpdateDefaults(M_EventCategoryHeat Database)
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
