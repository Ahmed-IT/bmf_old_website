using AMK.Plugin.JQueryDataTable;
using BMF.Shared.Extensions;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BMF.Shared.Entities.Events
{
    public partial class M_EventRound : IJQDataTableExtendDBViewModel<M_EventRound, BMFDbContext>, IDBViewModel<M_EventRound, BMFDbContext>
    {
        [NotMapped]
        public static string _controllerName => $"{nameof(M_EventRound)}".Replace("M_", "");

        [NotMapped]
        public string Display_DateTime_Start => DateTime_Start.ToString("dd-MMM-yyyy hh:mm tt");

        [NotMapped]
        public string Display_DateTime_End => DateTime_End.ToString("dd-MMM-yyyy hh:mm tt");

        [NotMapped]
        public string Display_AddTo_Calendar => AddTo_Calendar ? "Yes" : "No";

        [NotMapped]
        public string Display_AddTo_Countdown => AddTo_Countdown ? "Yes" : "No";

        #region Fields

        #endregion

        #region Ctor

        public M_EventRound()
        {
            DateTime_Start = DateTime.Now;
            DateTime_End = DateTime.Now;
            EventRoundSessions = new List<M_EventRoundSession>();
        }

        #endregion

        #region IJQDataTableExtendDBViewModel

        public string ClassAcronym => nameof(M_EventRound).Replace("M_", "").GetUppercaseLettersAsString();

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
                new Column(nameof(Round_Number), "Round Number"),
            };
            return IJQDataTableExtendDBViewModel<M_EventRound, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region IDBViewModel

        public void GetDatabaseValues(M_EventRound Database)
        {
            ID_EventCategory = Database.ID_EventCategory;
            Round_Number = Database.Round_Number;
            DateTime_Start = Database.DateTime_Start;
            DateTime_End = Database.DateTime_End;
            AddTo_Calendar = Database.AddTo_Calendar;
            AddTo_Countdown = Database.AddTo_Countdown;
        }

        public void SetDatabaseValues(M_EventRound Database)
        {
            Database.ID_EventCategory = ID_EventCategory;
            Database.Round_Number = Round_Number;
            Database.DateTime_Start = DateTime_Start;
            Database.DateTime_End = DateTime_End;
            Database.AddTo_Calendar = AddTo_Calendar;
            Database.AddTo_Countdown = AddTo_Countdown;
        }

        public void AsDatabaseUpdate(M_EventRound Database)
        {
            SetDatabaseValues(Database);
            UpdateDefaults(Database);
        }

        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<M_EventRound> databaseContext = null)
        {
            if (ID_EventCategory == 0)
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(ID_EventCategory)}", "Event category must be selected.");
            }
            if (string.IsNullOrWhiteSpace(Round_Number))
            {
                ModelState.AddModelError($"{propertyQualifier}{nameof(Round_Number)}", "Round number cannot be empty.");
            }
            if (databaseContext != null)
            {
                if (ID == 0)
                {
                    if (databaseContext.AsNoTracking().Count(c => c.ID_EventCategory == ID_EventCategory && c.Round_Number.ToLower().Trim() == Round_Number.ToLower().Trim() && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(ID_EventCategory)}", "Event round already exists.");
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Round_Number)}", "Event round already exists.");
                    }
                }
                else
                {
                    if (databaseContext.AsNoTracking().Count(c => c.ID_EventCategory == ID_EventCategory && c.Round_Number.ToLower().Trim() == Round_Number.ToLower().Trim() && c.ID != ID && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(ID_EventCategory)}", "Event round already exists.");
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Round_Number)}", "Event round already exists.");
                    }
                }
            }
        }

        public void GetDefaults(M_EventRound Database)
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

        public void UpdateDefaults(M_EventRound Database)
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
