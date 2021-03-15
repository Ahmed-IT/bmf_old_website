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

            public static SystemOperation SystemSettings => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettings), Children = new List<SystemOperation>() { SystemSettingsCountry } };

            // System Settings - Countries
            public static SystemOperation SystemSettingsCountry => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCountry), Children = new List<SystemOperation>() { SystemSettingsCountry_ViewAll, SystemSettingsCountry_Add, SystemSettingsCountry_Edit, SystemSettingsCountry_Delete }, Parent = GetParent(nameof(SystemSettings)) };
            public static SystemOperation SystemSettingsCountry_ViewAll => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCountry_ViewAll), Parent = GetParent(nameof(SystemSettingsCountry)) };
            public static SystemOperation SystemSettingsCountry_Add => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCountry_Add), Parent = GetParent(nameof(SystemSettingsCountry)) };
            public static SystemOperation SystemSettingsCountry_Edit => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCountry_Edit), Parent = GetParent(nameof(SystemSettingsCountry)) };
            public static SystemOperation SystemSettingsCountry_Delete => new SystemOperation { IsActive = true, SystemName = nameof(SystemSettingsCountry_Delete), Parent = GetParent(nameof(SystemSettingsCountry)) };

            #endregion
        }

        public static partial class Website_CMS
        {
            #region Website CMS

            public static SystemOperation WebsiteCMS => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS), Children = new List<SystemOperation>() { WebsiteCMS_News, WebsiteCMS_Club, WebsiteCMS_QuickLinks, WebsiteCMS_GalleryCategory } };

            // WebsiteCMS - News
            public static SystemOperation WebsiteCMS_News => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_News), Children = new List<SystemOperation>() { WebsiteCMS_News_ViewAll, WebsiteCMS_News_Add, WebsiteCMS_News_Edit, WebsiteCMS_News_Delete }, Parent = GetParent(nameof(WebsiteCMS)) };
            public static SystemOperation WebsiteCMS_News_ViewAll => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_News_ViewAll), Parent = GetParent(nameof(WebsiteCMS_News)) };
            public static SystemOperation WebsiteCMS_News_Add => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_News_Add), Parent = GetParent(nameof(WebsiteCMS_News)) };
            public static SystemOperation WebsiteCMS_News_Edit => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_News_Edit), Parent = GetParent(nameof(WebsiteCMS_News)) };
            public static SystemOperation WebsiteCMS_News_Delete => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_News_Delete), Parent = GetParent(nameof(WebsiteCMS_News)) };

            // WebsiteCMS - Club
            public static SystemOperation WebsiteCMS_Club => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_Club), Children = new List<SystemOperation>() { WebsiteCMS_Club_ViewAll, WebsiteCMS_Club_Add, WebsiteCMS_Club_Edit, WebsiteCMS_Club_Delete }, Parent = GetParent(nameof(WebsiteCMS)) };
            public static SystemOperation WebsiteCMS_Club_ViewAll => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_Club_ViewAll), Parent = GetParent(nameof(WebsiteCMS_Club)) };
            public static SystemOperation WebsiteCMS_Club_Add => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_Club_Add), Parent = GetParent(nameof(WebsiteCMS_Club)) };
            public static SystemOperation WebsiteCMS_Club_Edit => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_Club_Edit), Parent = GetParent(nameof(WebsiteCMS_Club)) };
            public static SystemOperation WebsiteCMS_Club_Delete => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_Club_Delete), Parent = GetParent(nameof(WebsiteCMS_Club)) };

            // WebsiteCMS - QuickLinks
            public static SystemOperation WebsiteCMS_QuickLinks => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_QuickLinks), Children = new List<SystemOperation>() { WebsiteCMS_QuickLinks_ViewAll, WebsiteCMS_QuickLinks_Add, WebsiteCMS_QuickLinks_Edit, WebsiteCMS_QuickLinks_Delete }, Parent = GetParent(nameof(WebsiteCMS)) };
            public static SystemOperation WebsiteCMS_QuickLinks_ViewAll => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_QuickLinks_ViewAll), Parent = GetParent(nameof(WebsiteCMS_QuickLinks)) };
            public static SystemOperation WebsiteCMS_QuickLinks_Add => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_QuickLinks_Add), Parent = GetParent(nameof(WebsiteCMS_QuickLinks)) };
            public static SystemOperation WebsiteCMS_QuickLinks_Edit => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_QuickLinks_Edit), Parent = GetParent(nameof(WebsiteCMS_QuickLinks)) };
            public static SystemOperation WebsiteCMS_QuickLinks_Delete => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_QuickLinks_Delete), Parent = GetParent(nameof(WebsiteCMS_QuickLinks)) };

            // WebsiteCMS - QuickLinks
            public static SystemOperation WebsiteCMS_GalleryCategory => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_GalleryCategory), Children = new List<SystemOperation>() { WebsiteCMS_GalleryCategory_ViewAll, WebsiteCMS_GalleryCategory_Add, WebsiteCMS_GalleryCategory_Edit, WebsiteCMS_GalleryCategory_Delete }, Parent = GetParent(nameof(WebsiteCMS)) };
            public static SystemOperation WebsiteCMS_GalleryCategory_ViewAll => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_GalleryCategory_ViewAll), Parent = GetParent(nameof(WebsiteCMS_GalleryCategory)) };
            public static SystemOperation WebsiteCMS_GalleryCategory_Add => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_GalleryCategory_Add), Parent = GetParent(nameof(WebsiteCMS_GalleryCategory)) };
            public static SystemOperation WebsiteCMS_GalleryCategory_Edit => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_GalleryCategory_Edit), Parent = GetParent(nameof(WebsiteCMS_GalleryCategory)) };
            public static SystemOperation WebsiteCMS_GalleryCategory_Delete => new SystemOperation { IsActive = true, SystemName = nameof(WebsiteCMS_GalleryCategory_Delete), Parent = GetParent(nameof(WebsiteCMS_GalleryCategory)) };

            #endregion
        }

        public static partial class EventMS
        {
            #region EMS

            public static SystemOperation EMS => new SystemOperation { IsActive = true, SystemName = nameof(EMS), Children = new List<SystemOperation>() { EMS_EventSeason , EMS_EventCategory, EMS_EventCategoryRules, EMS_EventCategoryBulletins } };

            // EMS - Event Season
            public static SystemOperation EMS_EventSeason => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventSeason), Children = new List<SystemOperation>() { EMS_EventSeason_ViewAll, EMS_EventSeason_Add, EMS_EventSeason_Edit, EMS_EventSeason_Delete }, Parent = GetParent(nameof(EMS)) };
            public static SystemOperation EMS_EventSeason_ViewAll => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventSeason_ViewAll), Parent = GetParent(nameof(EMS_EventSeason)) };
            public static SystemOperation EMS_EventSeason_Add => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventSeason_Add), Parent = GetParent(nameof(EMS_EventSeason)) };
            public static SystemOperation EMS_EventSeason_Edit => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventSeason_Edit), Parent = GetParent(nameof(EMS_EventSeason)) };
            public static SystemOperation EMS_EventSeason_Delete => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventSeason_Delete), Parent = GetParent(nameof(EMS_EventSeason)) };

            // EMS - Event Category
            public static SystemOperation EMS_EventCategory => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventCategory), Children = new List<SystemOperation>() { EMS_EventCategory_ViewAll, EMS_EventCategory_Add, EMS_EventCategory_Edit, EMS_EventCategory_Delete }, Parent = GetParent(nameof(EMS)) };
            public static SystemOperation EMS_EventCategory_ViewAll => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventCategory_ViewAll), Parent = GetParent(nameof(EMS_EventCategory)) };
            public static SystemOperation EMS_EventCategory_Add => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventCategory_Add), Parent = GetParent(nameof(EMS_EventCategory)) };
            public static SystemOperation EMS_EventCategory_Edit => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventCategory_Edit), Parent = GetParent(nameof(EMS_EventCategory)) };
            public static SystemOperation EMS_EventCategory_Delete => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventCategory_Delete), Parent = GetParent(nameof(EMS_EventCategory)) };

            // EMS - Event Category Rules
            public static SystemOperation EMS_EventCategoryRules => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventCategoryRules), Children = new List<SystemOperation>() { EMS_EventCategoryRules_ViewAll, EMS_EventCategoryRules_Add, EMS_EventCategoryRules_Edit, EMS_EventCategoryRules_Delete }, Parent = GetParent(nameof(EMS)) };
            public static SystemOperation EMS_EventCategoryRules_ViewAll => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventCategoryRules_ViewAll), Parent = GetParent(nameof(EMS_EventCategoryRules)) };
            public static SystemOperation EMS_EventCategoryRules_Add => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventCategoryRules_Add), Parent = GetParent(nameof(EMS_EventCategoryRules)) };
            public static SystemOperation EMS_EventCategoryRules_Edit => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventCategoryRules_Edit), Parent = GetParent(nameof(EMS_EventCategoryRules)) };
            public static SystemOperation EMS_EventCategoryRules_Delete => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventCategoryRules_Delete), Parent = GetParent(nameof(EMS_EventCategoryRules)) };

            // EMS - Event Category Bulletins
            public static SystemOperation EMS_EventCategoryBulletins => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventCategoryBulletins), Children = new List<SystemOperation>() { EMS_EventCategoryBulletins_ViewAll, EMS_EventCategoryBulletins_Add, EMS_EventCategoryBulletins_Edit, EMS_EventCategoryBulletins_Delete }, Parent = GetParent(nameof(EMS)) };
            public static SystemOperation EMS_EventCategoryBulletins_ViewAll => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventCategoryBulletins_ViewAll), Parent = GetParent(nameof(EMS_EventCategoryBulletins)) };
            public static SystemOperation EMS_EventCategoryBulletins_Add => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventCategoryBulletins_Add), Parent = GetParent(nameof(EMS_EventCategoryBulletins)) };
            public static SystemOperation EMS_EventCategoryBulletins_Edit => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventCategoryBulletins_Edit), Parent = GetParent(nameof(EMS_EventCategoryBulletins)) };
            public static SystemOperation EMS_EventCategoryBulletins_Delete => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventCategoryBulletins_Delete), Parent = GetParent(nameof(EMS_EventCategoryBulletins)) };

            // EMS - Event Round
            public static SystemOperation EMS_EventRound => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventRound), Children = new List<SystemOperation>() { EMS_EventRound_ViewAll, EMS_EventRound_Add, EMS_EventRound_Edit, EMS_EventRound_Delete }, Parent = GetParent(nameof(EMS)) };
            public static SystemOperation EMS_EventRound_ViewAll => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventRound_ViewAll), Parent = GetParent(nameof(EMS_EventRound)) };
            public static SystemOperation EMS_EventRound_Add => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventRound_Add), Parent = GetParent(nameof(EMS_EventRound)) };
            public static SystemOperation EMS_EventRound_Edit => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventRound_Edit), Parent = GetParent(nameof(EMS_EventRound)) };
            public static SystemOperation EMS_EventRound_Delete => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventRound_Delete), Parent = GetParent(nameof(EMS_EventRound)) };

            // EMS - Event Round Session
            public static SystemOperation EMS_EventRoundSession => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventRoundSession), Children = new List<SystemOperation>() { EMS_EventRoundSession_ViewAll, EMS_EventRoundSession_Add, EMS_EventRoundSession_Edit, EMS_EventRoundSession_Delete }, Parent = GetParent(nameof(EMS)) };
            public static SystemOperation EMS_EventRoundSession_ViewAll => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventRoundSession_ViewAll), Parent = GetParent(nameof(EMS_EventRoundSession)) };
            public static SystemOperation EMS_EventRoundSession_Add => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventRoundSession_Add), Parent = GetParent(nameof(EMS_EventRoundSession)) };
            public static SystemOperation EMS_EventRoundSession_Edit => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventRoundSession_Edit), Parent = GetParent(nameof(EMS_EventRoundSession)) };
            public static SystemOperation EMS_EventRoundSession_Delete => new SystemOperation { IsActive = true, SystemName = nameof(EMS_EventRoundSession_Delete), Parent = GetParent(nameof(EMS_EventRoundSession)) };

            #endregion
        }

        public static partial class BMFMS
        {
            #region UserMS

            public static SystemOperation BmfMS => new SystemOperation { IsActive = true, SystemName = nameof(BmfMS), Children = new List<SystemOperation>() { BmfMS_LicenseType } };

            // EMS - Event Season
            public static SystemOperation BmfMS_LicenseType => new SystemOperation { IsActive = true, SystemName = nameof(BmfMS_LicenseType), Children = new List<SystemOperation>() { BmfMS_LicenseType_ViewAll, BmfMS_LicenseType_Add, BmfMS_LicenseType_Edit, BmfMS_LicenseType_Delete }, Parent = GetParent(nameof(BmfMS)) };
            public static SystemOperation BmfMS_LicenseType_ViewAll => new SystemOperation { IsActive = true, SystemName = nameof(BmfMS_LicenseType_ViewAll), Parent = GetParent(nameof(BmfMS_LicenseType)) };
            public static SystemOperation BmfMS_LicenseType_Add => new SystemOperation { IsActive = true, SystemName = nameof(BmfMS_LicenseType_Add), Parent = GetParent(nameof(BmfMS_LicenseType)) };
            public static SystemOperation BmfMS_LicenseType_Edit => new SystemOperation { IsActive = true, SystemName = nameof(BmfMS_LicenseType_Edit), Parent = GetParent(nameof(BmfMS_LicenseType)) };
            public static SystemOperation BmfMS_LicenseType_Delete => new SystemOperation { IsActive = true, SystemName = nameof(BmfMS_LicenseType_Delete), Parent = GetParent(nameof(BmfMS_LicenseType)) };

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
                    Website_CMS.WebsiteCMS,
                    EventMS.EMS,
                    BMFMS.BmfMS,
                };
            else
            {
                Items = new List<SystemOperation>();
                Items.AddRange(typeof(Admin).GetProperties(BindingFlags.Static | BindingFlags.Public).Where(w => w.PropertyType == typeof(SystemOperation)).Select(s => (SystemOperation)s.GetValue(null)).ToList());
                Items.AddRange(typeof(Website_CMS).GetProperties(BindingFlags.Static | BindingFlags.Public).Where(w => w.PropertyType == typeof(SystemOperation)).Select(s => (SystemOperation)s.GetValue(null)).ToList());
                Items.AddRange(typeof(EventMS).GetProperties(BindingFlags.Static | BindingFlags.Public).Where(w => w.PropertyType == typeof(SystemOperation)).Select(s => (SystemOperation)s.GetValue(null)).ToList());
                Items.AddRange(typeof(BMFMS).GetProperties(BindingFlags.Static | BindingFlags.Public).Where(w => w.PropertyType == typeof(SystemOperation)).Select(s => (SystemOperation)s.GetValue(null)).ToList());
            }
            return Items;
        }

        #endregion
    }
}
