namespace EventDay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeLocalizationFromUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Events", "Locality");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Locality", c => c.String(nullable: false));
        }
    }
}
