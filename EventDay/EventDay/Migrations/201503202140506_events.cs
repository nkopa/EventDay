namespace EventDay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class events : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "ContactNumber", c => c.String(maxLength: 7));
            AlterColumn("dbo.Events", "ContactEmail", c => c.String(maxLength: 25));
            AlterColumn("dbo.Events", "Website", c => c.String(maxLength: 30));
            AlterColumn("dbo.Events", "HourBegin", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "HourEnd", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "HourBeginRegistration", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "HourEndRegistration", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "Voivoweship", c => c.String(maxLength: 20));
            AlterColumn("dbo.Events", "Street", c => c.String(maxLength: 20));
            AlterColumn("dbo.Events", "HouseNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.Events", "ApartmentNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.Events", "ZipCode", c => c.String(maxLength: 6));
            AlterColumn("dbo.Events", "Directions", c => c.String(maxLength: 300));
            DropColumn("dbo.Events", "Locality");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Locality", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "Directions", c => c.String());
            AlterColumn("dbo.Events", "ZipCode", c => c.String());
            AlterColumn("dbo.Events", "ApartmentNumber", c => c.String());
            AlterColumn("dbo.Events", "HouseNumber", c => c.String());
            AlterColumn("dbo.Events", "Street", c => c.String());
            AlterColumn("dbo.Events", "Voivoweship", c => c.String());
            AlterColumn("dbo.Events", "HourEndRegistration", c => c.String());
            AlterColumn("dbo.Events", "HourBeginRegistration", c => c.String());
            AlterColumn("dbo.Events", "HourEnd", c => c.String());
            AlterColumn("dbo.Events", "HourBegin", c => c.String());
            AlterColumn("dbo.Events", "Website", c => c.String());
            AlterColumn("dbo.Events", "ContactEmail", c => c.String());
            AlterColumn("dbo.Events", "ContactNumber", c => c.String());
        }
    }
}
