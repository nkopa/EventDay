namespace EventDay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addJoinEvent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JoinEvents",
                c => new
                    {
                        JoinEventId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                        EventId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JoinEventId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.JoinEvents", new[] { "EventId" });
            DropForeignKey("dbo.JoinEvents", "EventId", "dbo.Events");
            DropTable("dbo.JoinEvents");
        }
    }
}
