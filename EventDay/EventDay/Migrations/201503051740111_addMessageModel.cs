namespace EventDay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMessageModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        FromUser = c.String(),
                        ToUser = c.String(nullable: false),
                        SendDate = c.DateTime(nullable: false),
                        MessageText = c.String(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Messages");
        }
    }
}
