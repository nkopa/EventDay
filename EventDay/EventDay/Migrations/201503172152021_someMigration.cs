namespace EventDay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class someMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserContacts",
                c => new
                    {
                        UserContactId = c.Int(nullable: false, identity: true),
                        UserOwnerId = c.Int(nullable: false),
                        UserMemberId = c.Int(nullable: false),
                        UserOwner_UserId = c.Int(),
                        UserMember_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.UserContactId)
                .ForeignKey("dbo.UserProfiles", t => t.UserOwner_UserId)
                .ForeignKey("dbo.UserProfiles", t => t.UserMember_UserId)
                .Index(t => t.UserOwner_UserId)
                .Index(t => t.UserMember_UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserContacts", new[] { "UserMember_UserId" });
            DropIndex("dbo.UserContacts", new[] { "UserOwner_UserId" });
            DropForeignKey("dbo.UserContacts", "UserMember_UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.UserContacts", "UserOwner_UserId", "dbo.UserProfiles");
            DropTable("dbo.UserContacts");
        }
    }
}
