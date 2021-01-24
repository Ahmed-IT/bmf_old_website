using BMF.Shared.Entities.Security;
using BMF.Shared.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BMF.WebCore.Admin.Services.Security
{
    public partial class SystemOperationsProvider : ISystemOperationsProvider
    {
        public static partial class Admin
        {
            #region Admin Security - Section

            public static SystemOperation Security => new SystemOperation { IsActive = true, SystemName = nameof(Security), Children = new List<SystemOperation>() { SecurityAdminRole, SecurityAdminUser } };

            // Security - Admin Role
            public static SystemOperation SecurityAdminRole => new SystemOperation { IsActive = true, SystemName = nameof(SecurityAdminRole), Children = new List<SystemOperation>() { SecurityAdminRole_ViewAll, SecurityAdminRole_Add, SecurityAdminRole_Edit, SecurityAdminRole_Delete, SecurityAdminRole_ManageUsers, SecurityAdminRole_ManageOperations }, Parent = GetParent(nameof(Security)) };

            public static SystemOperation SecurityAdminRole_ViewAll => new SystemOperation { IsActive = true, SystemName = nameof(SecurityAdminRole_ViewAll), Parent = GetParent(nameof(SecurityAdminRole)) };
            public static SystemOperation SecurityAdminRole_Add => new SystemOperation { IsActive = true, SystemName = nameof(SecurityAdminRole_Add), Parent = GetParent(nameof(SecurityAdminRole)) };
            public static SystemOperation SecurityAdminRole_Edit => new SystemOperation { IsActive = true, SystemName = nameof(SecurityAdminRole_Edit), Parent = GetParent(nameof(SecurityAdminRole)) };
            public static SystemOperation SecurityAdminRole_Delete => new SystemOperation { IsActive = true, SystemName = nameof(SecurityAdminRole_Delete), Parent = GetParent(nameof(SecurityAdminRole)) };
            public static SystemOperation SecurityAdminRole_ManageUsers => new SystemOperation { IsActive = true, SystemName = nameof(SecurityAdminRole_ManageUsers), Parent = GetParent(nameof(SecurityAdminRole)) };
            public static SystemOperation SecurityAdminRole_ManageOperations => new SystemOperation { IsActive = true, SystemName = nameof(SecurityAdminRole_ManageOperations), Parent = GetParent(nameof(SecurityAdminRole)) };

            // Security - Admin User
            public static SystemOperation SecurityAdminUser => new SystemOperation { IsActive = true, SystemName = nameof(SecurityAdminUser), Children = new List<SystemOperation>() { SecurityAdminUser_ViewAll, SecurityAdminUser_Add, SecurityAdminUser_Edit, SecurityAdminUser_Delete, SecurityAdminUser_ManageRoles, SecurityAdminUser_ResetPassword }, Parent = GetParent(nameof(Security)) };
            public static SystemOperation SecurityAdminUser_ViewAll => new SystemOperation { IsActive = true, SystemName = nameof(SecurityAdminUser_ViewAll), Parent = GetParent(nameof(SecurityAdminUser)) };
            public static SystemOperation SecurityAdminUser_Add => new SystemOperation { IsActive = true, SystemName = nameof(SecurityAdminUser_Add), Parent = GetParent(nameof(SecurityAdminUser)) };
            public static SystemOperation SecurityAdminUser_Edit => new SystemOperation { IsActive = true, SystemName = nameof(SecurityAdminUser_Edit), Parent = GetParent(nameof(SecurityAdminUser)) };
            public static SystemOperation SecurityAdminUser_Delete => new SystemOperation { IsActive = true, SystemName = nameof(SecurityAdminUser_Delete), Parent = GetParent(nameof(SecurityAdminUser)) };
            public static SystemOperation SecurityAdminUser_ManageRoles => new SystemOperation { IsActive = true, SystemName = nameof(SecurityAdminUser_ManageRoles), Parent = GetParent(nameof(SecurityAdminUser)) };
            public static SystemOperation SecurityAdminUser_ResetPassword => new SystemOperation { IsActive = true, SystemName = nameof(SecurityAdminUser_ResetPassword), Parent = GetParent(nameof(SecurityAdminUser)) };

            #endregion

            #region Admin System Settings

            public static SystemOperation SystemSettings => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettings), Children = new List<SystemOperation>() { SystemSettingsCountry, SystemSettingsCity, SystemSettingsCurrency } };

            // System Settings - Countries
            public static SystemOperation SystemSettingsCountry => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCountry), Children = new List<SystemOperation>() { SystemSettingsCountry_ViewAll, SystemSettingsCountry_Add, SystemSettingsCountry_Edit, SystemSettingsCountry_Delete }, Parent = GetParent(nameof(SystemSettings)) };
            public static SystemOperation SystemSettingsCountry_ViewAll => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCountry_ViewAll), Parent = GetParent(nameof(SystemSettingsCountry)) };
            public static SystemOperation SystemSettingsCountry_Add => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCountry_Add), Parent = GetParent(nameof(SystemSettingsCountry)) };
            public static SystemOperation SystemSettingsCountry_Edit => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCountry_Edit), Parent = GetParent(nameof(SystemSettingsCountry)) };
            public static SystemOperation SystemSettingsCountry_Delete => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCountry_Delete), Parent = GetParent(nameof(SystemSettingsCountry)) };

            // System Settings - Cities
            public static SystemOperation SystemSettingsCity => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCity), Children = new List<SystemOperation>() { SystemSettingsCity_ViewAll, SystemSettingsCity_Add, SystemSettingsCity_Edit, SystemSettingsCity_Delete }, Parent = GetParent(nameof(SystemSettings)) };
            public static SystemOperation SystemSettingsCity_ViewAll => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCity_ViewAll), Parent = GetParent(nameof(SystemSettingsCity)) };
            public static SystemOperation SystemSettingsCity_Add => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCity_Add), Parent = GetParent(nameof(SystemSettingsCity)) };
            public static SystemOperation SystemSettingsCity_Edit => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCity_Edit), Parent = GetParent(nameof(SystemSettingsCity)) };
            public static SystemOperation SystemSettingsCity_Delete => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCity_Delete), Parent = GetParent(nameof(SystemSettingsCity)) };

            // System Settings - Currencies
            public static SystemOperation SystemSettingsCurrency => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCurrency), Children = new List<SystemOperation>() { SystemSettingsCurrency_ViewAll, SystemSettingsCurrency_Add, SystemSettingsCurrency_Edit, SystemSettingsCurrency_Delete }, Parent = GetParent(nameof(SystemSettings)) };
            public static SystemOperation SystemSettingsCurrency_ViewAll => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCurrency_ViewAll), Parent = GetParent(nameof(SystemSettingsCurrency)) };
            public static SystemOperation SystemSettingsCurrency_Add => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCurrency_Add), Parent = GetParent(nameof(SystemSettingsCurrency)) };
            public static SystemOperation SystemSettingsCurrency_Edit => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCurrency_Edit), Parent = GetParent(nameof(SystemSettingsCurrency)) };
            public static SystemOperation SystemSettingsCurrency_Delete => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCurrency_Delete), Parent = GetParent(nameof(SystemSettingsCurrency)) };

            #endregion
        }

        #region Methods

        public static SystemOperation GetParent(string SystemName)
        {
            return new SystemOperation() { SystemName = SystemName };
        }

        public List<SystemOperation> GetAll_Local(bool nested = true)
        {
            List<SystemOperation> Items;
            if (nested)
                Items = new List<SystemOperation> {
                    Admin.Security,
                    Admin.SystemSettings,
                };
            else
            {
                Items = new List<SystemOperation>();
                Items.AddRange(typeof(Admin).GetProperties(BindingFlags.Static | BindingFlags.Public).Where(w => w.PropertyType == typeof(SystemOperation)).Select(s => (SystemOperation)s.GetValue(null)).ToList());
            }
            return Items;
        }

        #endregion
    }
}
