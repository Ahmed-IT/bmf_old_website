//using AMK.Plugin.JQueryDataTable;
//using BMF.Shared.Core.Helpers;
//using BMF.Shared.Entities.Events;
//using BMF.Shared.Extensions;
//using BMF.Shared.Services.Data;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;

//namespace BMF.Shared.Entities.UserManagement
//{
//    public partial class M_UserAccount : IJQDataTableExtendDBViewModel<M_UserAccount, BMFDbContext>, IDBViewModel<M_UserAccount, BMFDbContext>
//    {
//        [NotMapped]
//        public static string _controllerName => $"{nameof(M_UserAccount)}s".Replace("M_", "");

//        [NotMapped]
//        public static string _defaultImage => $"https://bmfstorage.blob.core.windows.net/user-profile-image/default.jpg";

//        #region Fields

//        #endregion

//        #region Ctor

//        public M_UserAccount()
//        {
//            UserLicense = new List<M_UserLicense>();
//            UserResults = new List<M_EventRoundSessionUserResults>();
//            MyRaces = new List<M_MyRaces>();
//            MyFavRaces = new List<M_MyFavRaces>();
//        }

//        #endregion

//        #region IJQDataTableExtendDBViewModel

//        public string ClassAcronym => nameof(M_UserAccount).Replace("M_", "").GetUppercaseLettersAsString();

//        public string Secondary_ClassAcronym => ClassAcronym;

//        public virtual string DataTableOperations
//        {
//            get
//            {
//                var BtnEdit = "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' " +
//                    $"data-target='#M_{"Edit"}{ClassAcronym}' data-id='{ID}'><i class='{"fas fa-pencil-alt"}'></i></button>";
//                var BtnEditImage = "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' " +
//                    $"data-target='#M_E{"UI"}' data-id='{ID}'><i class='{"fas fa-image"}'></i></button>";
//                var BtnDelete = "<button type='button' class='btn btn-danger btn-sm' data-toggle='modal' " +
//                    $"data-target='#M_{"Delete"}{ClassAcronym}' data-id='{ID}'><i class='fas fa-trash-alt'></i></button>";
//                return $"<div class='btn-group'>{BtnEdit}{BtnEditImage}{BtnDelete}</div>";
//            }
//        }

//        public static List<Column> JQDT_Index()
//        {
//            var jqColumns = new List<Column>
//            {
//                new Column(nameof(ID), "ID", searchable: false, orderable: false),
//                new Column(nameof(Email_Address), "Email"),
//            };
//            return IJQDataTableExtendDBViewModel<M_UserAccount, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
//        }

//        #endregion

//        #region IDBViewModel

//        public void GetDatabaseValues(M_UserAccount Database)
//        {
//            Email_Address = Database.Email_Address;
//            Password = Database.Password;
//        }

//        public void SetDatabaseValues(M_UserAccount Database)
//        {
//            Database.Email_Address = Email_Address;
//            Database.Password = Password;
//        }

//        public void AsDatabaseUpdate(M_UserAccount Database)
//        {
//            SetDatabaseValues(Database);
//            UpdateDefaults(Database);
//        }

//        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<M_UserAccount> databaseContext = null)
//        {
//            if (string.IsNullOrWhiteSpace(Email_Address))
//            {
//                ModelState.AddModelError($"{propertyQualifier}{nameof(Email_Address)}", "Email address cannot be empty.");
//            }
//            else if (Email_Address.Length > 255)
//            {
//                ModelState.AddModelError($"{propertyQualifier}{nameof(Email_Address)}", "Email address cannot contain more than 255 characters.");
//            }
//            else if (!CommonHelper.IsValidEmail(Email_Address))
//            {
//                ModelState.AddModelError($"{propertyQualifier}{nameof(Email_Address)}", "Invalid email address.");
//            }
//            if (databaseContext != null)
//            {
//                if (ID == 0)
//                {
//                    if (databaseContext.AsNoTracking().Count(c => c.Email_Address.ToLower().Trim() == Email_Address.ToLower().Trim() && !c.DeletedOnUtc.HasValue) > 0)
//                    {
//                        ModelState.AddModelError($"{propertyQualifier}{nameof(Email_Address)}", "User with this Email already exists.");
//                    }
//                }
//                else
//                {
//                    if (databaseContext.AsNoTracking().Count(c => c.Email_Address.ToLower().Trim() == Email_Address.ToLower().Trim() && c.ID != ID && !c.DeletedOnUtc.HasValue) > 0)
//                    {
//                        ModelState.AddModelError($"{propertyQualifier}{nameof(Email_Address)}", "User with this Email already exists.");
//                    }
//                }
//            }
//        }

//        public void GetDefaults(M_UserAccount Database)
//        {
//            ID = Database.ID;
//            IsActive = Database.IsActive;
//            CreatedOnUtc = Database.CreatedOnUtc;
//            CreatedBy = Database.CreatedBy;
//            UpdatedOnUtc = Database.UpdatedOnUtc;
//            UpdatedBy = Database.UpdatedBy;
//            DeletedOnUtc = Database.DeletedOnUtc;
//            DeletedBy = Database.DeletedBy;
//            RowVersion = Database.RowVersion;
//        }

//        public void UpdateDefaults(M_UserAccount Database)
//        {
//            Database.IsActive = IsActive;
//            Database.DeletedBy = DeletedBy;
//            Database.UpdatedBy = UpdatedBy;
//            Database.DeletedOnUtc = DeletedOnUtc;
//            Database.UpdatedOnUtc = UpdatedOnUtc;
//        }

//        #endregion
//    }
//}
