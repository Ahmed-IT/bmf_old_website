using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _Standards.jQDataTable.MVC;
using _Standards.Library.ExpressionBuilder.Enums;
using _Standards.Library.Helpers;
using BMF.Shared.Models.AccessManagement;
using BMF.Web.Admin.Database;

namespace BMF.Shared.Models.AccessManagement
{
    public partial class S_Feature : JQDataTableExtendDBViewModel<S_Feature, BMFDbContext>
    {
        #region System Properties

        [NotMapped]
        public bool Cleared { get; set; }
        [NotMapped]
        public bool Selected { get; set; }

        #endregion

        #region JQ DataTables

        public string ClassAcronym => "SF";

        public string _Visible => (Visibility ? "Yes" : "No");

        public string Operations
        {
            get
            {
                var BtnEdit = "<button type='button' class='btn btn-primary btn-xs' data-toggle='modal' " +
                    $"data-target='#M_Edit{ClassAcronym}' data-id='{ID}'><i class='fa fa-pencil'></i></button>";
                return $"<div class='btn-group'>{BtnEdit}</div>";
            }
        }

        public static List<Column> JQDT_Index()
        {
            var jqColumns = new List<Column>
            {
                new Column("Name", "Name"),
                new Column("Title", "Title"),
                new Column("URL", "URL"),
                new Column("_Visible", "Visible", Operator: WhrExpOp.Equals, FilterControl: ClntSideFltrCtrl.select),
                new Column("Level", "Level", Operator: WhrExpOp.Equals),
                new Column("Order", "Order", Operator: WhrExpOp.Equals),
            };
            return AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region Constructors

        public _SystemFeature(S_Feature Database) : this(Database, false, null, null) { }

        public _SystemFeature(S_Feature Database, bool bIncludeFeatures = false, List<long> Selected = null, List<long> Cleared = null) : this()
        {
            if (Database != null)
            {
                ID = Database.ID;
                ID_Path = Database.ID_Path;
                URL = Database.URL;
                Name = Database.Name;
                Title = Database.Title;
                RightIcons_URL = Database.RightIcons_URL;
                RightIcons_Fixed = Database.RightIcons_Fixed;
                LeftIcons_Fixed = Database.LeftIcons_Fixed;
                Level = Database.Level;
                Order = Database.Order;
                Visibility = Database.Visibility;
                GetDefaults(Database);
                ID_ParentFeature = Database.ID_ParentFeature;
                #region Relationships 
                if (bIncludeFeatures)
                {
                    Features = Database.S_Feature1.OrderBy(o => o.Order).Select(s => new _SystemFeature(s, bIncludeFeatures: bIncludeFeatures, Selected: Selected, Cleared: Cleared)).ToList();
                }
                #endregion
                this.Cleared = (Cleared != null && Cleared.Contains(ID) ? true : false);
                this.Selected = (Selected != null && Selected.Contains(ID) ? true : false);
            }
        }

        #endregion

        #region Converters

        public override void SetDatabaseValues(S_Feature Database)
        {
            Database.ID_Path = ID_Path.Trim();
            Database.URL = URL.Trim();
            Database.Name = Name.Trim();
            Database.Title = Title.Trim();
            Database.RightIcons_URL = RightIcons_URL;
            Database.RightIcons_Fixed = RightIcons_Fixed;
            Database.LeftIcons_Fixed = LeftIcons_Fixed;
            Database.Level = Level;
            Database.Order = Order;
            Database.Visibility = Visibility;
            Database.ID_ParentFeature = ID_ParentFeature;
        }

        public override S_Feature AsDatabaseNew()
        {
            S_Feature Database = new S_Feature();
            SetDatabaseValues(Database);
            SetDefaults(Database);
            return Database;
        }

        public override void AsDatabaseUpdate(S_Feature Database)
        {
            if (Database != null)
            {
                SetDatabaseValues(Database);
                UpdateDefaults(Database);
            }
        }

        public override void GetDefaults(S_Feature Database)
        {
            if (Database != null)
            {
                #region Defaults
                ID = Database.ID;
                S_Status = Database.IsActive;
                By_Created = Database.CreatedBy;
                By_Deleted = Database.By_Deleted;
                By_Updated = Database.By_Updated;
                Date_Created = Database.Date_Created;
                Date_Deleted = Database.Date_Deleted;
                Date_Updated = Database.Date_Updated;
                RowVersion = Database.RowVersion;
                #endregion
            }
        }

        public override void SetDefaults(S_Feature Database)
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

        public override void UpdateDefaults(S_Feature Database)
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
            if (string.IsNullOrWhiteSpace(Name))
            {
                modelState.AddModelError(pQ + "Name", "Name cannot be empty.");
            }
            else if (Name.Length > 255)
            {
                modelState.AddModelError(pQ + "Name", "Name cannot contain more than 255 characters.");
            }
            if (string.IsNullOrWhiteSpace(Title))
            {
                modelState.AddModelError(pQ + "Title", "Title cannot be empty.");
            }
            else if (Title.Length > 120)
            {
                modelState.AddModelError(pQ + "Title", "Title cannot contain more than 255 characters.");
            }
            if (string.IsNullOrWhiteSpace(URL))
            {
                modelState.AddModelError(pQ + "URL", "URL cannot be empty.");
            }
            else if (URL.Length > 255)
            {
                modelState.AddModelError(pQ + "URL", "URL cannot contain more than 255 characters.");
            }
        }

        #endregion
    }
}