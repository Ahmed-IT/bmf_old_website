namespace BMF.Web.Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.D_LicenseType",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 60),
                        Title = c.String(nullable: false, maxLength: 120),
                        Image_Front_URL = c.String(nullable: false, maxLength: 1024),
                        Image_Back_URL = c.String(nullable: false, maxLength: 1024),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        UpdatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(),
                        DeletedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeletedBy = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.M_UserLicense",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ID_User = c.Long(nullable: false),
                        ID_LicenseType = c.Long(nullable: false),
                        License_Number = c.String(nullable: false, maxLength: 50),
                        Expire_Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        UpdatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(),
                        DeletedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeletedBy = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.M_User", t => t.ID_User, cascadeDelete: true)
                .ForeignKey("dbo.D_LicenseType", t => t.ID_LicenseType, cascadeDelete: true)
                .Index(t => t.ID_User)
                .Index(t => t.ID_LicenseType);
            
            CreateTable(
                "dbo.M_User",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Email_Address = c.String(nullable: false, maxLength: 120),
                        CPR_Number = c.String(nullable: false, maxLength: 60),
                        Password = c.String(nullable: false, maxLength: 120),
                        Is_Admin = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        UpdatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(),
                        DeletedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeletedBy = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.M_MyFavRaces",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ID_User = c.Long(nullable: false),
                        ID_EventRoundSession = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        UpdatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(),
                        DeletedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeletedBy = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.M_EventRoundSession", t => t.ID_EventRoundSession, cascadeDelete: true)
                .ForeignKey("dbo.M_User", t => t.ID_User, cascadeDelete: true)
                .Index(t => t.ID_User)
                .Index(t => t.ID_EventRoundSession);
            
            CreateTable(
                "dbo.M_EventRoundSession",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ID_EventRound = c.Long(nullable: false),
                        Title = c.String(nullable: false, maxLength: 120),
                        Results_PDF_URL = c.String(nullable: false, maxLength: 1024),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        UpdatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(),
                        DeletedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeletedBy = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.M_EventRound", t => t.ID_EventRound, cascadeDelete: true)
                .Index(t => t.ID_EventRound);
            
            CreateTable(
                "dbo.M_EventRound",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ID_EventCategory = c.Long(nullable: false),
                        Round_Number = c.String(nullable: false, maxLength: 10),
                        DateTime_Start = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateTime_End = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        AddTo_Calendar = c.Boolean(nullable: false),
                        AddTo_Countdown = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        UpdatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(),
                        DeletedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeletedBy = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.M_EventCategory", t => t.ID_EventCategory, cascadeDelete: true)
                .Index(t => t.ID_EventCategory);
            
            CreateTable(
                "dbo.M_EventCategory",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ID_EventSeason = c.Long(nullable: false),
                        Title = c.String(nullable: false, maxLength: 60),
                        Image_URL = c.String(nullable: false, maxLength: 1024),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        UpdatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(),
                        DeletedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeletedBy = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.M_EventSeason", t => t.ID_EventSeason, cascadeDelete: true)
                .Index(t => t.ID_EventSeason);
            
            CreateTable(
                "dbo.M_EventCategoryRules",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ID_EventCategory = c.Long(nullable: false),
                        Title = c.String(nullable: false, maxLength: 60),
                        PDF_Url = c.String(nullable: false, maxLength: 1024),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        UpdatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(),
                        DeletedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeletedBy = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.M_EventCategory", t => t.ID_EventCategory, cascadeDelete: true)
                .Index(t => t.ID_EventCategory);
            
            CreateTable(
                "dbo.M_EventSeason",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 60),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        UpdatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(),
                        DeletedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeletedBy = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.M_MyRaces",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ID_User = c.Long(nullable: false),
                        ID_EventRoundSession = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        UpdatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(),
                        DeletedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeletedBy = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.M_EventRoundSession", t => t.ID_EventRoundSession, cascadeDelete: true)
                .ForeignKey("dbo.M_User", t => t.ID_User, cascadeDelete: true)
                .Index(t => t.ID_User)
                .Index(t => t.ID_EventRoundSession);
            
            CreateTable(
                "dbo.M_EventRoundSessionUserResults",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ID_EventRoundSession = c.Long(nullable: false),
                        ID_User = c.Long(nullable: false),
                        Points = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        UpdatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(),
                        DeletedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeletedBy = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.M_EventRoundSession", t => t.ID_EventRoundSession, cascadeDelete: true)
                .ForeignKey("dbo.M_User", t => t.ID_User, cascadeDelete: true)
                .Index(t => t.ID_EventRoundSession)
                .Index(t => t.ID_User);
            
            CreateTable(
                "dbo.M_UserProfile",
                c => new
                    {
                        ID = c.Long(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 255),
                        Nationality = c.String(maxLength: 255),
                        DateOfBirth = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Mobile_Number = c.String(nullable: false, maxLength: 60),
                        Full_Address = c.String(nullable: false, maxLength: 1024),
                        Blood_Group = c.String(nullable: false, maxLength: 5),
                        Place_Of_Birth = c.String(maxLength: 1024),
                        Photo_URL = c.String(nullable: false, maxLength: 1024),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        UpdatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(),
                        DeletedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeletedBy = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.M_User", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.M_UserLicenseRenewal",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ID_UserLicense = c.Long(nullable: false),
                        Issue_Type = c.String(nullable: false, maxLength: 15),
                        Issue_Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        UpdatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(),
                        DeletedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeletedBy = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.M_UserLicense", t => t.ID_UserLicense, cascadeDelete: true)
                .Index(t => t.ID_UserLicense);
            
            CreateTable(
                "dbo.M_UserLicenseRenewalMedicForms",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ID_UserLicenseRenewal = c.Long(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PDF_File = c.String(nullable: false, maxLength: 1024),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        UpdatedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(),
                        DeletedOnUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeletedBy = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.M_UserLicenseRenewal", t => t.ID_UserLicenseRenewal, cascadeDelete: true)
                .Index(t => t.ID_UserLicenseRenewal);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.M_UserLicense", "ID_LicenseType", "dbo.D_LicenseType");
            DropForeignKey("dbo.M_UserLicenseRenewal", "ID_UserLicense", "dbo.M_UserLicense");
            DropForeignKey("dbo.M_UserLicenseRenewalMedicForms", "ID_UserLicenseRenewal", "dbo.M_UserLicenseRenewal");
            DropForeignKey("dbo.M_EventRoundSessionUserResults", "ID_User", "dbo.M_User");
            DropForeignKey("dbo.M_UserProfile", "ID", "dbo.M_User");
            DropForeignKey("dbo.M_UserLicense", "ID_User", "dbo.M_User");
            DropForeignKey("dbo.M_MyFavRaces", "ID_User", "dbo.M_User");
            DropForeignKey("dbo.M_MyFavRaces", "ID_EventRoundSession", "dbo.M_EventRoundSession");
            DropForeignKey("dbo.M_EventRoundSessionUserResults", "ID_EventRoundSession", "dbo.M_EventRoundSession");
            DropForeignKey("dbo.M_MyRaces", "ID_User", "dbo.M_User");
            DropForeignKey("dbo.M_MyRaces", "ID_EventRoundSession", "dbo.M_EventRoundSession");
            DropForeignKey("dbo.M_EventRoundSession", "ID_EventRound", "dbo.M_EventRound");
            DropForeignKey("dbo.M_EventCategory", "ID_EventSeason", "dbo.M_EventSeason");
            DropForeignKey("dbo.M_EventRound", "ID_EventCategory", "dbo.M_EventCategory");
            DropForeignKey("dbo.M_EventCategoryRules", "ID_EventCategory", "dbo.M_EventCategory");
            DropIndex("dbo.M_UserLicenseRenewalMedicForms", new[] { "ID_UserLicenseRenewal" });
            DropIndex("dbo.M_UserLicenseRenewal", new[] { "ID_UserLicense" });
            DropIndex("dbo.M_UserProfile", new[] { "ID" });
            DropIndex("dbo.M_EventRoundSessionUserResults", new[] { "ID_User" });
            DropIndex("dbo.M_EventRoundSessionUserResults", new[] { "ID_EventRoundSession" });
            DropIndex("dbo.M_MyRaces", new[] { "ID_EventRoundSession" });
            DropIndex("dbo.M_MyRaces", new[] { "ID_User" });
            DropIndex("dbo.M_EventCategoryRules", new[] { "ID_EventCategory" });
            DropIndex("dbo.M_EventCategory", new[] { "ID_EventSeason" });
            DropIndex("dbo.M_EventRound", new[] { "ID_EventCategory" });
            DropIndex("dbo.M_EventRoundSession", new[] { "ID_EventRound" });
            DropIndex("dbo.M_MyFavRaces", new[] { "ID_EventRoundSession" });
            DropIndex("dbo.M_MyFavRaces", new[] { "ID_User" });
            DropIndex("dbo.M_UserLicense", new[] { "ID_LicenseType" });
            DropIndex("dbo.M_UserLicense", new[] { "ID_User" });
            DropTable("dbo.M_UserLicenseRenewalMedicForms");
            DropTable("dbo.M_UserLicenseRenewal");
            DropTable("dbo.M_UserProfile");
            DropTable("dbo.M_EventRoundSessionUserResults");
            DropTable("dbo.M_MyRaces");
            DropTable("dbo.M_EventSeason");
            DropTable("dbo.M_EventCategoryRules");
            DropTable("dbo.M_EventCategory");
            DropTable("dbo.M_EventRound");
            DropTable("dbo.M_EventRoundSession");
            DropTable("dbo.M_MyFavRaces");
            DropTable("dbo.M_User");
            DropTable("dbo.M_UserLicense");
            DropTable("dbo.D_LicenseType");
        }
    }
}
