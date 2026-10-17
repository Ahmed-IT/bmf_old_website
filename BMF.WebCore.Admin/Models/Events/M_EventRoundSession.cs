using AMK.Plugin.JQueryDataTable;
using BMF.Shared.Entities.UserManagement;
using BMF.Shared.Extensions;
using BMF.Shared.Services.Azure;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Events
{
    public partial class M_EventRoundSession : IJQDataTableExtendDBViewModel<M_EventRoundSession, BMFDbContext>, IDBViewModel<M_EventRoundSession, BMFDbContext>
    {
        [NotMapped]
        public static string _controllerName => $"{nameof(M_EventRoundSession)}".Replace("M_", "");

        [NotMapped]
        public static string _defaultResultsPDF => $"https://bmfstorage.blob.core.windows.net/{AzureStorageContainers.EventRoundSession}/default.jpg";

        #region Fields

        #endregion

        #region Ctor

        public M_EventRoundSession()
        {
            UserResults = new List<M_EventRoundSessionUserResults>();
            //User_MyRaces = new List<M_MyRaces>();
            //User_MyFavRaces = new List<M_MyFavRaces>();
        }

        #endregion

        #region IJQDataTableExtendDBViewModel

        public string ClassAcronym => nameof(M_EventRoundSession).Replace("M_", "").GetUppercaseLettersAsString();

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
                new Column($"{nameof(EventRound)}.{nameof(M_EventRound.EventSeason)}.{nameof(M_EventSeason.Title)}", "Season"),
                new Column($"{nameof(EventRound)}.{nameof(M_EventRound.EventCategory)}.{nameof(M_EventCategory.Title)}", "Event Category"),
                new Column($"{nameof(EventRound)}.{nameof(M_EventRound.Round_Number)}", "Event Round Number"),
                new Column(nameof(Title), "Title"),
            };
            return IJQDataTableExtendDBViewModel<M_EventRoundSession, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region IDBViewModel

        public void GetDatabaseValues(M_EventRoundSession Database)
        {
            ID_EventRound = Database.ID_EventRound;
            Title = Database.Title;
            Results_PDF_URL = Database.Results_PDF_URL;
        }

        public void SetDatabaseValues(M_EventRoundSession Database)
        {
            Database.ID_EventRound = ID_EventRound;
            Database.Title = Title;
            Database.Results_PDF_URL = Results_PDF_URL;
        }

        public void AsDatabaseUpdate(M_EventRoundSession Database)
        {
            SetDatabaseValues(Database);
            UpdateDefaults(Database);
        }

        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<M_EventRoundSession> databaseContext = null)
        {
            if (ID_EventRound == 0)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(ID_EventRound)}", "Event round must be selected.");
            }
            if (string.IsNullOrWhiteSpace(Title))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Title)}", "Title cannot be empty.");
            }
        }

        public void GetDefaults(M_EventRoundSession Database)
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

        public void UpdateDefaults(M_EventRoundSession Database)
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
