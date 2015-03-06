namespace EventDay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserUpdateEvent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        AccountTypeId = c.String(),
                        Email = c.String(nullable: false),
                        Name = c.String(),
                        Surname = c.String(),
                        SexId = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Interest = c.String(),
                        Voivodeship = c.String(),
                        City = c.String(),
                        SmsNotification = c.String(),
                        PhoneNumber = c.String(),
                        ProfileImage = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        StatusId = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            AddColumn("dbo.Events", "AccessId", c => c.String());
            AddColumn("dbo.Events", "RecruitmentId", c => c.String());
            AddColumn("dbo.Events", "Capacity", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "Regulations", c => c.String());
            AddColumn("dbo.Events", "DressCode", c => c.String());
            AddColumn("dbo.Events", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Events", "ContactNumber", c => c.String());
            AddColumn("dbo.Events", "ContactEmail", c => c.String());
            AddColumn("dbo.Events", "HourBegin", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "HourEnd", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "DateBeginRegistation", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "HourBeginRegistration", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "DateEndRegistation", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "HourEndRegistration", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "Voivoweship", c => c.String());
            AddColumn("dbo.Events", "HouseNumber", c => c.String());
            AddColumn("dbo.Events", "ApartmentNumber", c => c.String());
            AddColumn("dbo.Events", "ZipCode", c => c.String());
            AddColumn("dbo.Events", "Directions", c => c.String());
            AlterColumn("dbo.Events", "Description", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "Description", c => c.String(nullable: false));
            DropColumn("dbo.Events", "Directions");
            DropColumn("dbo.Events", "ZipCode");
            DropColumn("dbo.Events", "ApartmentNumber");
            DropColumn("dbo.Events", "HouseNumber");
            DropColumn("dbo.Events", "Voivoweship");
            DropColumn("dbo.Events", "HourEndRegistration");
            DropColumn("dbo.Events", "DateEndRegistation");
            DropColumn("dbo.Events", "HourBeginRegistration");
            DropColumn("dbo.Events", "DateBeginRegistation");
            DropColumn("dbo.Events", "HourEnd");
            DropColumn("dbo.Events", "HourBegin");
            DropColumn("dbo.Events", "ContactEmail");
            DropColumn("dbo.Events", "ContactNumber");
            DropColumn("dbo.Events", "Price");
            DropColumn("dbo.Events", "DressCode");
            DropColumn("dbo.Events", "Regulations");
            DropColumn("dbo.Events", "Capacity");
            DropColumn("dbo.Events", "RecruitmentId");
            DropColumn("dbo.Events", "AccessId");
            DropTable("dbo.Users");
        }
    }
}
