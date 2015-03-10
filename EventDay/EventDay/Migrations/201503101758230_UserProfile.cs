namespace EventDay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserProfile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                {
                    UserId = c.Int(nullable: false, identity: true),
                    UserName = c.String(nullable: false),
                    AccountTypeId = c.String(nullable: false),
                    Email = c.String(nullable: false),
                    Name = c.String(),
                    Surname = c.String(),
                    SexId = c.String(),
                    Birthday = c.DateTime(),
                    Interest  = c.String(),
                    Voivodeship = c.String(),
                    City = c.String(),
                    SmsNotification = c.String(),
                    PhoneNumber = c.String(),
                    ProfileImage = c.String(),
                    CreateTime = c.DateTime(),
                    UpdateTime = c.DateTime(),
                    StatusId = c.String(),
                })
                .PrimaryKey(t => t.UserId);
        }

        public override void Down()
        {
        }
    }
}
