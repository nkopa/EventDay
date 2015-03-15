namespace EventDay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUserContactModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserContacts", "UserMemberId", c => c.Int(nullable: false));
            AddColumn("dbo.UserContacts", "UserMember_UserId", c => c.Int());
            AddForeignKey("dbo.UserContacts", "UserMember_UserId", "dbo.UserProfiles", "UserId");
            CreateIndex("dbo.UserContacts", "UserMember_UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserContacts", new[] { "UserMember_UserId" });
            DropForeignKey("dbo.UserContacts", "UserMember_UserId", "dbo.UserProfiles");
            DropColumn("dbo.UserContacts", "UserMember_UserId");
            DropColumn("dbo.UserContacts", "UserMemberId");
        }
    }
}
