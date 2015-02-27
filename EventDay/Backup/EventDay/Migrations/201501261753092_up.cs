namespace EventDay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Title", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Events", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "City", c => c.String(nullable: false));
            AddColumn("dbo.Events", "Locality", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Locality");
            DropColumn("dbo.Events", "City");
            DropColumn("dbo.Events", "Date");
            DropColumn("dbo.Events", "Title");
        }
    }
}
