using AMK.Plugin.JQueryDataTable;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BMF.Shared.Extensions;
using BMF.Shared.Services.Data;

namespace BMF.Shared.Entities.Location
{
    public partial class Country : IJQDataTableExtendDBViewModel<Country, BMFDbContext>, IDBViewModel<Country, BMFDbContext>
    {
        [NotMapped]
        public static string _controllerName => $"Location{nameof(Country)}";

        #region Fields

        #endregion

        #region IJQDataTableExtendDBViewModel

        public string ClassAcronym => nameof(Country).GetUppercaseLettersAsString();

        public string Secondary_ClassAcronym => ClassAcronym;

        public virtual string DataTableOperations
        {
            get
            {
                var BtnEdit = "<button type='button' class='btn btn-primary btn-xs' data-toggle='modal' " +
                    $"data-target='#M_{"Edit"}{ClassAcronym}' data-id='{ID}'><i class='{"fas fa-pencil-alt"}'></i></button>";
                var BtnDelete = "<button type='button' class='btn btn-danger btn-xs' data-toggle='modal' " +
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
                new Column(nameof(AlphaCode_2), "2 AlphaCode"),
                new Column(nameof(AlphaCode_3), "3 AlphaCode"),
                new Column(nameof(CallingCode), "Calling Code(s)"),
            };
            return IJQDataTableExtendDBViewModel<Country, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
        }

        #endregion

        #region IDBViewModel

        public void GetDatabaseValues(Country Database)
        {
            Name = Database.Name;
            AlphaCode_2 = Database.AlphaCode_2;
            AlphaCode_3 = Database.AlphaCode_3;
            NumericCode = Database.NumericCode;
            Demonym = Database.Demonym;
            Region = Database.Region;
            SubRegion = Database.SubRegion;
            CallingCode = Database.CallingCode;
        }

        public void SetDatabaseValues(Country Database)
        {
            Database.Name = Name;
            Database.AlphaCode_2 = AlphaCode_2;
            Database.AlphaCode_3 = AlphaCode_3;
            Database.NumericCode = NumericCode;
            Database.Demonym = Demonym;
            Database.Region = Region;
            Database.SubRegion = SubRegion;
            Database.CallingCode = CallingCode;
        }

        public void AsDatabaseUpdate(Country Database)
        {
            SetDatabaseValues(Database);
            UpdateDefaults(Database);
        }

        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<Country> databaseContext = null)
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
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Name)}", "Country already exists.");
                    }
                }
                else
                {
                    if (databaseContext.AsNoTracking().Count(c => c.Name.ToLower().Trim() == Name.ToLower().Trim() && c.ID != ID && !c.DeletedOnUtc.HasValue) > 0)
                    {
                        ModelState.AddModelError($"{propertyQualifier}{nameof(Name)}", "Country already exists.");
                    }
                }
            }
        }

        public void GetDefaults(Country Database)
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

        public void UpdateDefaults(Country Database)
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
