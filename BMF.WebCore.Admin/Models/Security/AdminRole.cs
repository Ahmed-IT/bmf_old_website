using AMK.Plugin.JQueryDataTable;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BMF.Shared.Extensions;
using BMF.Shared.Services.Data;

namespace BMF.Shared.Entities.Security
{
    public partial class AdminRole : IJQDataTableExtendDBViewModel<AdminRole, BMFDbContext>, IDBViewModel<AdminRole, BMFDbContext>
    {
        [NotMapped]
        public static string _controllerName => $"{nameof(AdminRole)}";

        #region Fields

        #endregion

        #region Ctor

        #endregion

        #region IJQDataTableExtendDBViewModel

        public string ClassAcronym => nameof(AdminRole).GetUppercaseLettersAsString();

        public string Secondary_ClassAcronym => ClassAcronym;

        public virtual string DataTableOperations
        {
            get
            {
                var BtnEdit = "<button type='button' class='btn btn-primary btn-xs' data-toggle='modal' " +
                    $"data-target='#M_{"Edit"}{ClassAcronym}' data-id='{ID}'><i class='{"fas fa-pencil-alt"}'></i></button>";
                var BtnEditOperations = "<button type='button' class='btn btn-primary btn-xs' data-toggle='modal' " +
                    $"data-target='#M_{"Edit"}{ClassAcronym}ASO' data-id='{ID}'><i class='{"fas fa-bars"}'></i></button>";
                var BtnEditUsers = "<button type='button' class='btn btn-primary btn-xs' data-toggle='modal' " +
                    $"data-target='#M_{"Edit"}{ClassAcronym}AAU' data-id='{ID}'><i class='{"fas fa-users"}'></i></button>";
                var BtnDelete = "<button type='button' class='btn btn-danger btn-xs' data-toggle='modal' " +
                    $"data-target='#M_{"Delete"}{ClassAcronym}' data-id='{ID}'><i class='fas fa-trash-alt'></i></button>";
                if (IsIAMRelated) return $"<div class='btn-group'>{BtnEditUsers}</div>";
                return $"<div class='btn-group'>{BtnEdit}{BtnEditOperations}{BtnEditUsers}{BtnDelete}</div>";
            }
        }

        public static List<Column> JQDT_Index()
        {
            var jqColumns = new List<Column>
            {
                new Column(nameof(ID), "ID", searchable: false, orderable: false),
                new Column(nameof(Name), "Name"),
            };
            return IJQDataTableExtendDBViewModel<AdminRole, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region IDBViewModel

        public void GetDatabaseValues(AdminRole Database)
        {
            Name = Database.Name;
            Description = Database.Description;
            IsIAMRelated = Database.IsIAMRelated;
        }

        public void SetDatabaseValues(AdminRole Database)
        {
            Database.Name = Name;
            Database.Description = Description;
            Database.IsIAMRelated = IsIAMRelated;
        }

        public void AsDatabaseUpdate(AdminRole Database)
        {
            SetDatabaseValues(Database);
            if (DB_UpdateOperations)
            {
                foreach (var _OldOperation in Database.AssignedOperations)
                {
                    _OldOperation.IsActive = false;
                    _OldOperation.UpdatedBy = UpdatedBy;
                    _OldOperation.UpdatedOnUtc = UpdatedOnUtc;
                }
                foreach (var set in AssignedOperations)
                {
                    var _Existing = Database.AssignedOperations.Where(w => w.ID_Operation == set.ID_Operation).SingleOrDefault();
                    if (_Existing != null)
                    {
                        _Existing.IsActive = true;
                        _Existing.UpdatedBy = UpdatedBy;
                        _Existing.UpdatedOnUtc = UpdatedOnUtc;
                        _Existing.DeletedBy = null;
                        _Existing.DeletedOnUtc = null;
                    }
                    else
                    {
                        AdminRoleOperation adminRoleOperation = new AdminRoleOperation
                        {
                            ID_Operation = set.ID_Operation,
                            IsActive = true,
                            CreatedBy = CreatedBy,
                            UpdatedBy = UpdatedBy,
                            CreatedOnUtc = CreatedOnUtc,
                            UpdatedOnUtc = UpdatedOnUtc
                        };
                        Database.AssignedOperations.Add(adminRoleOperation);
                    }
                }
            }
            if (DB_UpdateUsers)
            {
                foreach (var _OldUser in Database.AssignedUsers)
                {
                    _OldUser.IsActive = false;
                    _OldUser.UpdatedBy = UpdatedBy;
                    _OldUser.UpdatedOnUtc = UpdatedOnUtc;
                }
                foreach (var set in AssignedUsers)
                {
                    var _Existing = Database.AssignedUsers.Where(w => w.ID_User == set.ID_User).SingleOrDefault();
                    if (_Existing != null)
                    {
                        _Existing.IsActive = true;
                        _Existing.UpdatedBy = UpdatedBy;
                        _Existing.UpdatedOnUtc = UpdatedOnUtc;
                        _Existing.DeletedBy = null;
                        _Existing.DeletedOnUtc = null;
                    }
                    else
                    {
                        AdminUserRole adminUserRole = new AdminUserRole
                        {
                            ID_User = set.ID_User,
                            IsActive = true,
                            CreatedBy = CreatedBy,
                            UpdatedBy = UpdatedBy,
                            CreatedOnUtc = CreatedOnUtc,
                            UpdatedOnUtc = UpdatedOnUtc
                        };
                        Database.AssignedUsers.Add(adminUserRole);
                    }
                }
            }
            UpdateDefaults(Database);
        }

        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<AdminRole> databaseContext = null)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Name)}", "Name cannot be empty.");
            }
            else if (Name.Length > 255)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Name)}", "Name cannot contain more than 255 characters.");
            }
            if (databaseContext != null)
            {
                if (ID == 0)
                {
                    if (databaseContext.AsNoTracking().Count(c => c.Name.ToLower().Trim() == Name.ToLower().Trim() && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Name)}", "Admin role already exists.");
                    }
                }
                else
                {
                    if (databaseContext.AsNoTracking().Count(c => c.Name.ToLower().Trim() == Name.ToLower().Trim() && c.ID != ID && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Name)}", "Admin role already exists.");
                    }
                }
            }
        }

        public void GetDefaults(AdminRole Database)
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

        public void UpdateDefaults(AdminRole Database)
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
