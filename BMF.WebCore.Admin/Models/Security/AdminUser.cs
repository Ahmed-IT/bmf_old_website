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
    public partial class AdminUser : IJQDataTableExtendDBViewModel<AdminUser, BMFDbContext>, IDBViewModel<AdminUser, BMFDbContext>
    {
        [NotMapped]
        public static string _controllerName => $"{nameof(AdminUser)}";

        #region Fields

        #endregion

        #region Ctor

        #endregion

        #region IJQDataTableExtendDBViewModel

        public string ClassAcronym => nameof(AdminUser).GetUppercaseLettersAsString();

        public string Secondary_ClassAcronym => ClassAcronym;

        public virtual string DataTableOperations
        {
            get
            {
                var BtnEdit = "<button type='button' class='btn btn-primary btn-xs' data-toggle='modal' " +
                    $"data-target='#M_{"Edit"}{ClassAcronym}' data-id='{ID}'><i class='{"fas fa-pencil-alt"}'></i></button>";
                var BtnEditPassword = "<button type='button' class='btn btn-primary btn-xs' data-toggle='modal' " +
                    $"data-target='#M_{"Edit"}{ClassAcronym}P' data-id='{ID}'><i class='{"fas fa-key"}'></i></button>";
                var BtnEditRoles = "<button type='button' class='btn btn-primary btn-xs' data-toggle='modal' " +
                    $"data-target='#M_{"Edit"}{ClassAcronym}AAR' data-id='{ID}'><i class='{"fas fa-tags"}'></i></button>";
                var BtnDelete = "<button type='button' class='btn btn-danger btn-xs' data-toggle='modal' " +
                    $"data-target='#M_{"Delete"}{ClassAcronym}' data-id='{ID}'><i class='fas fa-trash-alt'></i></button>";
                return $"<div class='btn-group'>{BtnEdit}{BtnEditPassword}{BtnEditRoles}{BtnDelete}</div>";
            }
        }

        public static List<Column> JQDT_Index()
        {
            var jqColumns = new List<Column>
            {
                new Column(nameof(ID), "ID", searchable: false, orderable: false),
                new Column(nameof(FullName), "Name"),
                new Column(nameof(Username), "Username"),
                new Column(nameof(Email), "Email"),
            };
            return IJQDataTableExtendDBViewModel<AdminUser, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region IDBViewModel

        public void GetDatabaseValues(AdminUser Database)
        {
            Email = Database.Email;
            Username = Database.Username;
            FirstName = Database.FirstName;
            LastName = Database.LastName;
            Password = Database.Password;
        }

        public void SetDatabaseValues(AdminUser Database)
        {
            Database.Email = Email;
            Database.Username = Username;
            Database.FirstName = FirstName;
            Database.LastName = LastName;
            Database.Password = Password;
        }

        public void AsDatabaseUpdate(AdminUser Database)
        {
            SetDatabaseValues(Database);
            if (DB_UpdateRoles)
            {
                foreach (var _OldRole in Database.AssignedRoles)
                {
                    _OldRole.IsActive = false;
                    _OldRole.UpdatedBy = UpdatedBy;
                    _OldRole.UpdatedOnUtc = UpdatedOnUtc;
                }
                foreach (var set in AssignedRoles)
                {
                    var _Existing = Database.AssignedRoles.Where(w => w.ID_Role == set.ID_Role).SingleOrDefault();
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
                            ID_Role = set.ID_Role,
                            IsActive = true,
                            CreatedBy = CreatedBy,
                            UpdatedBy = UpdatedBy,
                            CreatedOnUtc = CreatedOnUtc,
                            UpdatedOnUtc = UpdatedOnUtc
                        };
                        Database.AssignedRoles.Add(adminUserRole);
                    }
                }
            }
            UpdateDefaults(Database);
        }

        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<AdminUser> databaseContext = null)
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Email)}", "Email cannot be empty.");
            }
            else if (Email.Length > 255)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Email)}", "Email cannot contain more than 255 characters.");
            }
            if (string.IsNullOrWhiteSpace(Username))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Username)}", "Username cannot be empty.");
            }
            else if (Username.Length > 255)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Username)}", "Username cannot contain more than 255 characters.");
            }
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(FirstName)}", "First name cannot be empty.");
            }
            else if (FirstName.Length > 255)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(FirstName)}", "First name cannot contain more than 255 characters.");
            }
            if (string.IsNullOrWhiteSpace(LastName))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(LastName)}", "Last name cannot be empty.");
            }
            else if (LastName.Length > 255)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(LastName)}", "Last name cannot contain more than 255 characters.");
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Password)}", "Password cannot be empty.");
            }
            else if (Password.Length > 255)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Password)}", "Password cannot contain more than 255 characters.");
            }
            if (databaseContext != null)
            {
                if (ID == 0)
                {
                    if (databaseContext.AsNoTracking().Count(c => c.Email.ToLower().Trim() == Email.ToLower().Trim() && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Email)}", "Admin user already exists. Email must be unique");
                    }
                    if (databaseContext.AsNoTracking().Count(c => c.Username.ToLower().Trim() == Username.ToLower().Trim() && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Username)}", "Admin user already exists. Username must be unique.");
                    }
                }
                else
                {
                    if (databaseContext.AsNoTracking().Count(c => c.Email.ToLower().Trim() == Email.ToLower().Trim() && c.ID != ID && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Email)}", "Admin user already exists. Email must be unique");
                    }
                    if (databaseContext.AsNoTracking().Count(c => c.Username.ToLower().Trim() == Username.ToLower().Trim() && c.ID != ID && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Username)}", "Admin user already exists. Username must be unique.");
                    }
                }
            }
        }

        public void GetDefaults(AdminUser Database)
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

        public void UpdateDefaults(AdminUser Database)
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
