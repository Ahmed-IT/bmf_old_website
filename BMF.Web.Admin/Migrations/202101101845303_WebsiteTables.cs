namespace BMF.Web.Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebsiteTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.M_Club",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 1024),
                        Website_Link = c.String(nullable: false, maxLength: 1024),
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
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.M_GalleryCategory",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
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
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.M_GalleryImages",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ID_GalleryCategory = c.Long(nullable: false),
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
                .ForeignKey("dbo.M_GalleryCategory", t => t.ID_GalleryCategory, cascadeDelete: true)
                .Index(t => t.ID_GalleryCategory);
            
            CreateTable(
                "dbo.M_News",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Content = c.String(nullable: false, maxLength: 3000),
                        Image_URL = c.String(nullable: false, maxLength: 1024),
                        NewsTimestampUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
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
                "dbo.M_QuickLinks",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Website_Link = c.String(nullable: false, maxLength: 1024),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.M_GalleryImages", "ID_GalleryCategory", "dbo.M_GalleryCategory");
            DropIndex("dbo.M_GalleryImages", new[] { "ID_GalleryCategory" });
            DropTable("dbo.M_QuickLinks");
            DropTable("dbo.M_News");
            DropTable("dbo.M_GalleryImages");
            DropTable("dbo.M_GalleryCategory");
            DropTable("dbo.M_Club");
        }
    }
}
