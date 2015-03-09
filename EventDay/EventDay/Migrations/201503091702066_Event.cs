namespace EventDay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Event : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "AccessId", c => c.String());
            AddColumn("dbo.Events", "RecruitmentId", c => c.String());
            AddColumn("dbo.Events", "Capacity", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "Regulations", c => c.String());
            AddColumn("dbo.Events", "ProfileImage", c => c.String());
            AddColumn("dbo.Events", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Events", "ContactNumber", c => c.String());
            AddColumn("dbo.Events", "ContactEmail", c => c.String());
            AddColumn("dbo.Events", "Website", c => c.String());
            AddColumn("dbo.Events", "HourBegin", c => c.String());
            AddColumn("dbo.Events", "HourEnd", c => c.String());
            AddColumn("dbo.Events", "DateBeginRegistation", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "HourBeginRegistration", c => c.String());
            AddColumn("dbo.Events", "DateEndRegistation", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "HourEndRegistration", c => c.String());
            AddColumn("dbo.Events", "Voivoweship", c => c.String());
            AddColumn("dbo.Events", "Street", c => c.String());
            AddColumn("dbo.Events", "HouseNumber", c => c.String());
            AddColumn("dbo.Events", "ApartmentNumber", c => c.String());
            AddColumn("dbo.Events", "ZipCode", c => c.String());
            AddColumn("dbo.Events", "Directions", c => c.String());
            AlterColumn("dbo.Events", "Description", c => c.String(nullable: false, maxLength: 1000));
            DropColumn("dbo.Events", "Locality");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Locality", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "Description", c => c.String(nullable: false));
            DropColumn("dbo.Events", "Directions");
            DropColumn("dbo.Events", "ZipCode");
            DropColumn("dbo.Events", "ApartmentNumber");
            DropColumn("dbo.Events", "HouseNumber");
            DropColumn("dbo.Events", "Street");
            DropColumn("dbo.Events", "Voivoweship");
            DropColumn("dbo.Events", "HourEndRegistration");
            DropColumn("dbo.Events", "DateEndRegistation");
            DropColumn("dbo.Events", "HourBeginRegistration");
            DropColumn("dbo.Events", "DateBeginRegistation");
            DropColumn("dbo.Events", "HourEnd");
            DropColumn("dbo.Events", "HourBegin");
            DropColumn("dbo.Events", "Website");
            DropColumn("dbo.Events", "ContactEmail");
            DropColumn("dbo.Events", "ContactNumber");
            DropColumn("dbo.Events", "Price");
            DropColumn("dbo.Events", "ProfileImage");
            DropColumn("dbo.Events", "Regulations");
            DropColumn("dbo.Events", "Capacity");
            DropColumn("dbo.Events", "RecruitmentId");
            DropColumn("dbo.Events", "AccessId");
        }
    }
}
