using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _Standards.jQDataTable.MVC;
using _Standards.Library.Helpers;
using _Standards.Library.ExpressionBuilder.Enums;
using BMF.Shared.Models.AccessManagement;
using BMF.Web.Admin.Database;

namespace BMF.Web.Admin.Models
{
    public class _SystemUser : JQDataTableExtendDBViewModel<S_User, BMFDbContext>
    {
        #region Properties

        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        #endregion

        #region Foreign Keys

        #endregion

        #region System Properties

        public bool Selected { get; set; }
        public bool DB_UpdateRoles { get; set; }
        public string ConfirmPassword { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        #endregion

        #region Relationships

        public List<_SystemRole> Roles { get; set; }

        #endregion

        #region JQ DataTables

        public override string ClassAcronym => "SU";

        public override string Operations
        {
            get
            {
                var BtnEdit = "<button type='button' class='btn btn-primary btn-xs' data-toggle='modal' " +
                    $"data-target='#M_Edit{ClassAcronym}' data-id='{ID}'><i class='fa fa-pencil'></i></button>";
                var BtnEditPassword = "<button type='button' class='btn btn-primary btn-xs' data-toggle='modal' " +
                    $"data-target='#M_Edit{ClassAcronym}P' data-id='{ID}'><i class='fa fa-key'></i></button>";
                var BtnEditRoles = "<button type='button' class='btn btn-primary btn-xs' data-toggle='modal' " +
                    $"data-target='#M_Edit{ClassAcronym}R' data-id='{ID}'><i class='fa fa-tags'></i></button>";
                var BtnDelete = "<button type='button' class='btn btn-danger btn-xs' data-toggle='modal' " +
                    $"data-target='#M_Delete{ClassAcronym}' data-id='{ID}'><i class='fa fa-remove'></i></button>";
                return $"<div class='btn-group'>{BtnEdit}{BtnEditPassword}{BtnEditRoles}{BtnDelete}</div>";
            }
        }

        public static List<Column> JQDT_Index()
        {
            var jqColumns = new List<Column>
            {
                new Column("FullName", "Full Name"),
                new Column("Username", "Username"),
                new Column("Email", "Email"),
            };
            return AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region Constructors

        public _SystemUser()
        {
            Roles = new List<_SystemRole>();
        }

        public _SystemUser(long ID) : this()
        {
            this.ID = ID;
        }

        public _SystemUser(S_User Database, bool bIncludeRoles = false, List<long> Selected = null) : this()
        {
            ID = Database.ID;
            Email = Database.Email;
            Username = Database.Username;
            Password = Database.Password;
            LastName = Database.LastName;
            FirstName = Database.FirstName;
            this.Selected = (Selected != null && Selected.Contains(ID) ? true : false);
            GetDefaults(Database);
            if (bIncludeRoles)
            {
                Roles = Database.S_RoleUser.Where(w => w.S_Status && !w.Date_Deleted.HasValue && w.S_Role.S_Status && !w.S_Role.Date_Deleted.HasValue).ToList().Select(s => new _SystemRole(s.S_Role, true)).ToList();
            }
        }

        #endregion

        #region Converters

        public override void SetDatabaseValues(S_User Database)
        {
            Database.Email = Email;
            Database.Username = Username;
            Database.Password = Password;
            Database.LastName = LastName;
            Database.FirstName = FirstName;
        }

        public override S_User AsDatabaseNew()
        {
            S_User Database = new S_User();
            SetDatabaseValues(Database);
            SetDefaults(Database);
            return Database;
        }

        public override void AsDatabaseUpdate(S_User Database)
        {
            if (Database != null)
            {
                SetDatabaseValues(Database);
                if (DB_UpdateRoles)
                {
                    foreach (var _OldRole in Database.S_RoleUser)
                    {
                        _OldRole.S_Status = false;
                        _OldRole.By_Updated = By_Updated;
                        _OldRole.Date_Updated = Date_Updated;
                    }
                    Roles.ForEach(set =>
                    {
                        var _Existing = Database.S_RoleUser.Where(w => w.ID_Role == set.ID).SingleOrDefault();
                        if (_Existing != null)
                        {
                            _Existing.S_Status = true;
                            _Existing.By_Deleted = null;
                            _Existing.Date_Deleted = null;
                            _Existing.By_Updated = By_Updated;
                            _Existing.Date_Updated = Date_Updated;
                        }
                        else
                        {
                            S_RoleUser _S_RoleUsers = new S_RoleUser();
                            _S_RoleUsers.S_Status = true;
                            _S_RoleUsers.ID_Role = set.ID;
                            _S_RoleUsers.By_Created = By_Created;
                            _S_RoleUsers.By_Updated = By_Updated;
                            _S_RoleUsers.Date_Created = Date_Created;
                            _S_RoleUsers.Date_Updated = Date_Updated;
                            Database.S_RoleUser.Add(_S_RoleUsers);
                        }
                    });
                }
                UpdateDefaults(Database);
            }
        }

        public override void GetDefaults(S_User Database)
        {
            if (Database != null)
            {
                #region Defaults
                ID = Database.ID;
                S_Status = Database.S_Status;
                By_Created = Database.By_Created;
                By_Deleted = Database.By_Deleted;
                By_Updated = Database.By_Updated;
                Date_Created = Database.Date_Created;
                Date_Deleted = Database.Date_Deleted;
                Date_Updated = Database.Date_Updated;
                RowVersion = Database.RowVersion;
                #endregion
            }
        }

        public override void SetDefaults(S_User Database)
        {
            if (Database != null)
            {
                #region Defaults
                Database.S_Status = S_Status;
                Database.By_Created = By_Created;
                Database.By_Deleted = By_Deleted;
                Database.By_Updated = By_Updated;
                Database.Date_Created = Date_Created;
                Database.Date_Deleted = Date_Deleted;
                Database.Date_Updated = Date_Updated;
                #endregion
            }
        }

        public override void UpdateDefaults(S_User Database)
        {
            if (Database != null)
            {
                #region Defaults
                Database.S_Status = S_Status;
                Database.By_Deleted = By_Deleted;
                Database.By_Updated = By_Updated;
                Database.Date_Deleted = Date_Deleted;
                Database.Date_Updated = Date_Updated;
                #endregion
            }
        }

        public override void Validate(ModelStateDictionary modelState, string pQ = "", AccessClass_DB _TheDatabase = null)
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                modelState.AddModelError("FirstName", "First name cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(LastName))
            {
                modelState.AddModelError("LastName", "Last name cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(Email))
            {
                modelState.AddModelError("Email", "Email cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(Username))
            {
                modelState.AddModelError("Username", "Username cannot be empty.");
            }
        }

        #endregion
    }
}