using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _Standards.jQDataTable.MVC;
using _Standards.Library.ExpressionBuilder.Enums;
using _Standards.Library.Helpers;
using BMF.Shared.Models.AccessManagement;
using BMF.Web.Admin.Database;

namespace BMF.Web.Admin.Models
{
    public class _SystemRole : JQDataTableExtendDBViewModel<S_Role, BMFDbContext>
    {
        #region Properties

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Admin { get; set; }

        #endregion

        #region Foreign Keys

        #endregion

        #region System Properties

        public bool Selected { get; set; }
        public bool DB_UpdateUsers { get; set; }
        public bool DB_UpdateFeatures { get; set; }

        #endregion

        #region Relationships

        public List<_SystemUser> Users { get; set; }
        public List<_SystemFeature> Features { get; set; }

        #endregion

        #region JQ DataTables

        public override string ClassAcronym => "SR";

        public override string Operations
        {
            get
            {
                var BtnEdit = "<button type='button' class='btn btn-primary btn-xs' data-toggle='modal' " +
                    $"data-target='#M_Edit{ClassAcronym}' data-id='{ID}'><i class='fa fa-pencil'></i></button>";
                var BtnEditFeatures = "<button type='button' class='btn btn-primary btn-xs' data-toggle='modal' " +
                    $"data-target='#M_Edit{ClassAcronym}F' data-id='{ID}'><i class='fa fa-bars'></i></button>";
                var BtnEditUsers = "<button type='button' class='btn btn-primary btn-xs' data-toggle='modal' " +
                    $"data-target='#M_Edit{ClassAcronym}U' data-id='{ID}'><i class='fa fa-users'></i></button>";
                var BtnDelete = "<button type='button' class='btn btn-danger btn-xs' data-toggle='modal' " +
                    $"data-target='#M_Delete{ClassAcronym}' data-id='{ID}'><i class='fa fa-remove'></i></button>";
                if (Admin) return $"<div class='btn-group'>{BtnEditUsers}</div>";
                return $"<div class='btn-group'>{BtnEdit}{BtnEditFeatures}{BtnEditUsers}{BtnDelete}</div>";
            }
        }

        public static List<Column> JQDT_Index()
        {
            var jqColumns = new List<Column>
            {
                new Column("Name", "Name"),
            };
            return AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region Constructors

        public _SystemRole()
        {
            Users = new List<_SystemUser>();
            Features = new List<_SystemFeature>();
        }

        public _SystemRole(long ID) : this()
        {
            this.ID = ID;
        }

        public _SystemRole(S_Role Database) : this(Database, false, false, null) { }

        public _SystemRole(S_Role Database, bool bIncludeFeatures = false, bool bIncludeUsers = false, List<long> Selected = null) : this()
        {
            ID = Database.ID;
            Name = Database.Name;
            Admin = Database.Admin;
            Description = Database.Description;
            this.Selected = (Selected != null && Selected.Contains(ID) ? true : false);
            GetDefaults(Database);
            if (bIncludeFeatures)
            {
                Features = Database.S_RoleFeature.Where(w => w.S_Status && !w.Date_Deleted.HasValue && w.S_Feature.S_Status && !w.S_Feature.Date_Deleted.HasValue).ToList().Select(s => new _SystemFeature(s.S_Feature)).ToList();
            }
            if (bIncludeUsers)
            {
                Users = Database.S_RoleUser.Where(w => w.S_Status && !w.Date_Deleted.HasValue && w.S_User.S_Status && !w.S_User.Date_Deleted.HasValue).ToList().Select(s => new _SystemUser(s.S_User)).ToList();
            }
        }

        #endregion

        #region Converters

        public override void SetDatabaseValues(S_Role Database)
        {
            Database.Name = Name.Trim();
            Database.Description = Description;
        }

        public override S_Role AsDatabaseNew()
        {
            S_Role Database = new S_Role();
            SetDatabaseValues(Database);
            SetDefaults(Database);
            return Database;
        }

        public override void AsDatabaseUpdate(S_Role Database)
        {
            if (Database != null)
            {
                SetDatabaseValues(Database);
                if (DB_UpdateUsers)
                {
                    foreach (var _OldUser in Database.S_RoleUser)
                    {
                        _OldUser.S_Status = false;
                        _OldUser.By_Updated = By_Updated;
                        _OldUser.Date_Updated = Date_Updated;
                    }
                    Users.ForEach(set =>
                    {
                        var _Existing = Database.S_RoleUser.Where(w => w.ID_User == set.ID).SingleOrDefault();
                        if (_Existing != null)
                        {
                            _Existing.S_Status = true;
                            _Existing.By_Updated = By_Updated;
                            _Existing.Date_Updated = Date_Updated;
                            _Existing.By_Deleted = null;
                            _Existing.Date_Deleted = null;
                        }
                        else
                        {
                            S_RoleUser _S_RoleUsers = new S_RoleUser();
                            _S_RoleUsers.S_Status = true;
                            _S_RoleUsers.ID_User = set.ID;
                            _S_RoleUsers.By_Created = By_Created;
                            _S_RoleUsers.By_Updated = By_Updated;
                            _S_RoleUsers.Date_Created = Date_Created;
                            _S_RoleUsers.Date_Updated = Date_Updated;
                            Database.S_RoleUser.Add(_S_RoleUsers);
                        }
                    });
                }
                if (DB_UpdateFeatures)
                {
                    foreach (var _OldFeature in Database.S_RoleFeature)
                    {
                        _OldFeature.S_Status = false;
                        _OldFeature.By_Updated = By_Updated;
                        _OldFeature.Date_Updated = Date_Updated;
                    }
                    Features.ForEach(set =>
                    {
                        var _Existing = Database.S_RoleFeature.Where(w => w.ID_Feature == set.ID).SingleOrDefault();
                        if (_Existing != null)
                        {
                            _Existing.S_Status = true;
                            _Existing.By_Updated = By_Updated;
                            _Existing.Date_Updated = Date_Updated;
                            _Existing.By_Deleted = null;
                            _Existing.Date_Deleted = null;
                        }
                        else
                        {
                            S_RoleFeature _S_RoleFeatures = new S_RoleFeature();
                            _S_RoleFeatures.S_Status = true;
                            _S_RoleFeatures.ID_Feature = set.ID;
                            _S_RoleFeatures.By_Created = By_Created;
                            _S_RoleFeatures.By_Updated = By_Updated;
                            _S_RoleFeatures.Date_Created = Date_Created;
                            _S_RoleFeatures.Date_Updated = Date_Updated;
                            Database.S_RoleFeature.Add(_S_RoleFeatures);
                        }
                    });
                }
                UpdateDefaults(Database);
            }
        }

        public override void GetDefaults(S_Role Database)
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

        public override void SetDefaults(S_Role Database)
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

        public override void UpdateDefaults(S_Role Database)
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

        public override void Validate(ModelStateDictionary modelState, string pQ = "", BMFDbContext _TheDatabase = null)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                modelState.AddModelError(pQ + "Name", "Name cannot be empty.");
            }
            else if (Name.Length > 255)
            {
                modelState.AddModelError(pQ + "Name", "Name cannot contain more than 255 characters.");
            }
        }

        #endregion
    }
}