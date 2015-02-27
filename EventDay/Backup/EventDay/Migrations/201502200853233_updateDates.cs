namespace EventDay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "DateBegin", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "DateEnd", c => c.DateTime(nullable: false));
            DropColumn("dbo.Events", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Events", "DateEnd");
            DropColumn("dbo.Events", "DateBegin");
        }
    }
}
