using AMK.Plugin.JQueryDataTable;
using BMF.Shared.Extensions;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Events
{
    public partial class M_EventRoundSessionUserResults : IJQDataTableExtendDBViewModel<M_EventRoundSessionUserResults, BMFDbContext>, IDBViewModel<M_EventRoundSessionUserResults, BMFDbContext>
    {
        [NotMapped]
        public static string _controllerName => $"{nameof(M_EventRoundSessionUserResults)}".Replace("M_", "");

        #region Fields

        #endregion

        #region Ctor

        public M_EventRoundSessionUserResults()
        {
        }

        #endregion

        #region IJQDataTableExtendDBViewModel

        public string ClassAcronym => nameof(M_EventRoundSessionUserResults).Replace("M_", "").GetUppercaseLettersAsString();

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
                
                new Column(nameof(Points), "Title"),
            };
            return IJQDataTableExtendDBViewModel<M_EventRoundSessionUserResults, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region IDBViewModel

        public void GetDatabaseValues(M_EventRoundSessionUserResults Database)
        {
            ID_EventRoundSession = Database.ID_EventRoundSession;
            //ID_User = Database.ID_User;
            Points = Database.Points;
        }

        public void SetDatabaseValues(M_EventRoundSessionUserResults Database)
        {
            Database.ID_EventRoundSession = ID_EventRoundSession;
            //Database.ID_User = ID_User;
            Database.Points = Points;
        }

        public void AsDatabaseUpdate(M_EventRoundSessionUserResults Database)
        {
            SetDatabaseValues(Database);
            UpdateDefaults(Database);
        }

        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<M_EventRoundSessionUserResults> databaseContext = null)
        {
            if (ID_EventRoundSession == 0)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(ID_EventRoundSession)}", "Event round session must be selected.");
            }
            //if (ID_User == 0)
            //{
            //    ModelState.AddModelError($"{propertyQualifier}{nameof(ID_User)}", "User must be selected.");
            //}
        }

        public void GetDefaults(M_EventRoundSessionUserResults Database)
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

        public void UpdateDefaults(M_EventRoundSessionUserResults Database)
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
