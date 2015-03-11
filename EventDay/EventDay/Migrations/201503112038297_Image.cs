namespace EventDay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Image : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        EventId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        AddDate = c.DateTime(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Images", new[] { "EventId" });
            DropForeignKey("dbo.Images", "EventId", "dbo.Events");
            DropTable("dbo.Images");
        }
    }
}
