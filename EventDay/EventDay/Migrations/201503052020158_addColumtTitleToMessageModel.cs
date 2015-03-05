namespace EventDay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumtTitleToMessageModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Title", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "Title");
        }
    }
}
