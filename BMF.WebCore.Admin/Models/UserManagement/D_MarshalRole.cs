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
    public partial class D_MarshalRole : IJQDataTableExtendDBViewModel<D_MarshalRole, BMFDbContext>, IDBViewModel<D_MarshalRole, BMFDbContext>
    {
        [NotMapped]
        public static string _controllerName => $"{nameof(D_MarshalRole)}s".Replace("D_", "");

        #region Fields

        #endregion

        #region Ctor

        public D_MarshalRole()
        {
            //UserLicense = new List<M_UserLicense>();
        }

        #endregion

        #region IJQDataTableExtendDBViewModel

        public string ClassAcronym => nameof(D_MarshalRole).Replace("D_", "").GetUppercaseLettersAsString();

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
                new Column(nameof(Name), "Name"),
            };
            return IJQDataTableExtendDBViewModel<D_MarshalRole, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region IDBViewModel

        public void GetDatabaseValues(D_MarshalRole Database)
        {
            Name = Database.Name;
        }

        public void SetDatabaseValues(D_MarshalRole Database)
        {
            Database.Name = Name;
        }

        public void AsDatabaseUpdate(D_MarshalRole Database)
        {
            SetDatabaseValues(Database);
            UpdateDefaults(Database);
        }

        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<D_MarshalRole> databaseContext = null)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Name)}", "Name cannot be empty.");
            }
            else if (Name.Length > 60)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Name)}", "Name cannot contain more than 60 characters.");
            }
            if (databaseContext != null)
            {
                if (ID == 0)
                {
                    if (databaseContext.AsNoTracking().Count(c => c.Name.ToLower().Trim() == Name.ToLower().Trim() && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Name)}", "Marshal role already exists.");
                    }
                }
                else
                {
                    if (databaseContext.AsNoTracking().Count(c => c.Name.ToLower().Trim() == Name.ToLower().Trim() && c.ID != ID && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Name)}", "Marshal role already exists.");
                    }
                }
            }
        }

        public void GetDefaults(D_MarshalRole Database)
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

        public void UpdateDefaults(D_MarshalRole Database)
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
