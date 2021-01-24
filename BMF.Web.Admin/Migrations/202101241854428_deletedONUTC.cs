namespace BMF.Web.Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedONUTC : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.M_EventCategory", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_EventCategory", "DeletedBy", c => c.String());
            AlterColumn("dbo.M_EventCategoryRules", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_EventCategoryRules", "DeletedBy", c => c.String());
            AlterColumn("dbo.M_EventRound", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_EventRound", "DeletedBy", c => c.String());
            AlterColumn("dbo.M_EventRoundSession", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_EventRoundSession", "DeletedBy", c => c.String());
            AlterColumn("dbo.M_MyFavRaces", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_MyFavRaces", "DeletedBy", c => c.String());
            AlterColumn("dbo.M_User", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_User", "DeletedBy", c => c.String());
            AlterColumn("dbo.M_MyRaces", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_MyRaces", "DeletedBy", c => c.String());
            AlterColumn("dbo.M_UserLicense", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_UserLicense", "DeletedBy", c => c.String());
            AlterColumn("dbo.D_LicenseType", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.D_LicenseType", "DeletedBy", c => c.String());
            AlterColumn("dbo.M_UserLicenseRenewal", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_UserLicenseRenewal", "DeletedBy", c => c.String());
            AlterColumn("dbo.M_UserLicenseRenewalMedicForms", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_UserLicenseRenewalMedicForms", "DeletedBy", c => c.String());
            AlterColumn("dbo.M_UserProfile", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_UserProfile", "DeletedBy", c => c.String());
            AlterColumn("dbo.M_EventRoundSessionUserResults", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_EventRoundSessionUserResults", "DeletedBy", c => c.String());
            AlterColumn("dbo.M_EventSeason", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_EventSeason", "DeletedBy", c => c.String());
            AlterColumn("dbo.S_User", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.S_User", "DeletedBy", c => c.String());
            AlterColumn("dbo.M_Club", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_Club", "DeletedBy", c => c.String());
            AlterColumn("dbo.M_GalleryCategory", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_GalleryCategory", "DeletedBy", c => c.String());
            AlterColumn("dbo.M_GalleryImages", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_GalleryImages", "DeletedBy", c => c.String());
            AlterColumn("dbo.M_News", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_News", "DeletedBy", c => c.String());
            AlterColumn("dbo.M_QuickLinks", "DeletedOnUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_QuickLinks", "DeletedBy", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.M_QuickLinks", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.M_QuickLinks", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_News", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.M_News", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_GalleryImages", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.M_GalleryImages", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_GalleryCategory", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.M_GalleryCategory", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_Club", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.M_Club", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.S_User", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.S_User", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_EventSeason", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.M_EventSeason", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_EventRoundSessionUserResults", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.M_EventRoundSessionUserResults", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_UserProfile", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.M_UserProfile", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_UserLicenseRenewalMedicForms", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.M_UserLicenseRenewalMedicForms", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_UserLicenseRenewal", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.M_UserLicenseRenewal", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.D_LicenseType", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.D_LicenseType", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_UserLicense", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.M_UserLicense", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_MyRaces", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.M_MyRaces", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_User", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.M_User", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_MyFavRaces", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.M_MyFavRaces", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_EventRoundSession", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.M_EventRoundSession", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_EventRound", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.M_EventRound", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_EventCategoryRules", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.M_EventCategoryRules", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.M_EventCategory", "DeletedBy", c => c.String(nullable: false));
            AlterColumn("dbo.M_EventCategory", "DeletedOnUtc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
