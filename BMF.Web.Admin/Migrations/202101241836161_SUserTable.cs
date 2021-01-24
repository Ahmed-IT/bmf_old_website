namespace BMF.Web.Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SUserTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.S_User",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 120),
                        Username = c.String(nullable: false, maxLength: 60),
                        FirstName = c.String(nullable: false, maxLength: 60),
                        LastName = c.String(nullable: false, maxLength: 60),
                        Password = c.String(nullable: false, maxLength: 120),
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
            
            AlterColumn("dbo.M_User", "Password", c => c.String(nullable: false, maxLength: 225));
            DropColumn("dbo.M_User", "Is_Admin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.M_User", "Is_Admin", c => c.Boolean(nullable: false));
            AlterColumn("dbo.M_User", "Password", c => c.String(nullable: false, maxLength: 120));
            DropTable("dbo.S_User");
        }
    }
}
