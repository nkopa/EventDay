namespace EventDay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Username = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Events", new[] { "CategoryId" });
            DropForeignKey("dbo.Events", "CategoryId", "dbo.Categories");
            DropTable("dbo.Categories");
            DropTable("dbo.Events");
        }
    }
}
