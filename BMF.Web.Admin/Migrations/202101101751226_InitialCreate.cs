namespace BMF.Web.Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.M_News",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        Image_URL = c.String(),
                        NewsTimestampUtc = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOnUtc = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        DeletedOnUtc = c.DateTime(),
                        DeletedBy = c.String(),
                        RowVersion = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.M_News");
        }
    }
}
