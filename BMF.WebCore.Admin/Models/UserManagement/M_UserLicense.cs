using AMK.Plugin.JQueryDataTable;
using BMF.Shared.Extensions;
using BMF.Shared.Services.Azure;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BMF.Shared.Entities.UserManagement
{
	public partial class M_UserLicense : IJQDataTableExtendDBViewModel<M_UserLicense, BMFDbContext>, IDBViewModel<M_UserLicense, BMFDbContext>
	{
		[NotMapped]
		public static string _controllerName => $"{nameof(M_UserLicense)}s".Replace("M_", "");

		[NotMapped]
		public static string _defaultImage => $"https://bmfstorage.blob.core.windows.net/{AzureStorageContainers.UserProfile}/default.jpg";

		[NotMapped]
		public string Display_MarshalRole => MarshalRole != null ? MarshalRole.Name : string.Empty;

		[NotMapped]
		public string Display_Expire_Date => Expire_Date.ToString("dd-MMM-yyyy");

		[NotMapped]
		public string Display_LicenseType => LicenseType != null ? LicenseType.Display_LicenseType : string.Empty;

		#region Fields

		#endregion

		#region Ctor

		public M_UserLicense()
		{
			Expire_Date = DateTime.Now;
			//UserLicense = new List<M_UserLicense>();
		}

		#endregion

		#region IJQDataTableExtendDBViewModel

		public string ClassAcronym => nameof(M_UserLicense).Replace("M_", "").GetUppercaseLettersAsString();

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
				new Column($"{nameof(UserProfile)}.{nameof(M_UserProfile.CPR)}", "CPR"),
				new Column($"{nameof(UserProfile)}.{nameof(M_UserProfile.FullName)}", "Full Name"),
				new Column($"{nameof(LicenseType)}.{nameof(D_LicenseType.Title)}", "License Title"),
				new Column($"{nameof(LicenseType)}.{nameof(D_LicenseType.Type)}", "License Type"),
				new Column(nameof(License_Number), "License Number"),
			};
			return IJQDataTableExtendDBViewModel<M_UserLicense, BMFDbContext>.AddStatusAndOperationColumns(jqColumns);
		}

		#endregion

		#region IDBViewModel

		public void GetDatabaseValues(M_UserLicense Database)
		{
			ID_UserProfile = Database.ID_UserProfile;
			ID_LicenseType = Database.ID_LicenseType;
			ID_MarshalRole = Database.ID_MarshalRole;
			License_Number = Database.License_Number;
			Expire_Date = Database.Expire_Date;
		}

		public void SetDatabaseValues(M_UserLicense Database)
		{
			Database.ID_UserProfile = ID_UserProfile;
			Database.ID_LicenseType = ID_LicenseType;
			Database.ID_MarshalRole = ID_MarshalRole;
			Database.License_Number = License_Number;
			Database.Expire_Date = Expire_Date;
		}

		public void AsDatabaseUpdate(M_UserLicense Database)
		{
			SetDatabaseValues(Database);
			UpdateDefaults(Database);
		}

		public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<M_UserLicense> databaseContext = null)
		{
			if (ID_UserProfile == 0)
			{
				ModelState.AddModelError($"{propertyQualifier}{nameof(ID_UserProfile)}", "Invalid selection.");
			}
			if (ID_LicenseType == 0)
			{
				ModelState.AddModelError($"{propertyQualifier}{nameof(ID_LicenseType)}", "Invalid selection.");
			}
			if (ID_MarshalRole == 0)
			{
				ModelState.AddModelError($"{propertyQualifier}{nameof(ID_MarshalRole)}", "Invalid selection.");
			}
			if (string.IsNullOrWhiteSpace(License_Number))
			{
				ModelState.AddModelError($"{propertyQualifier}{nameof(License_Number)}", "License number cannot be empty.");
			}
			else if (License_Number.Length > 50)
			{
				ModelState.AddModelError($"{propertyQualifier}{nameof(License_Number)}", "License number contain more than 120 characters.");
			}
			if (databaseContext != null)
			{
				if (ID == 0)
				{
					if (databaseContext.AsNoTracking().Count(c => c.ID_LicenseType == ID_LicenseType && c.License_Number.ToLower().Trim() == License_Number.ToLower().Trim() && !c.DeletedOnUtc.HasValue) > 0)
					{
						ModelState.AddModelError($"{propertyQualifier}{nameof(ID_LicenseType)}", "License number already exists.");
						ModelState.AddModelError($"{propertyQualifier}{nameof(License_Number)}", "License number already exists.");
					}
				}
				else
				{
					if (databaseContext.AsNoTracking().Count(c => c.ID_LicenseType == ID_LicenseType && c.License_Number.ToLower().Trim() == License_Number.ToLower().Trim() && c.ID != ID && !c.DeletedOnUtc.HasValue) > 0)
					{
						ModelState.AddModelError($"{propertyQualifier}{nameof(ID_LicenseType)}", "License number already exists.");
						ModelState.AddModelError($"{propertyQualifier}{nameof(License_Number)}", "License number already exists.");
					}
				}
			}
		}

		public void GetDefaults(M_UserLicense Database)
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

		public void UpdateDefaults(M_UserLicense Database)
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
