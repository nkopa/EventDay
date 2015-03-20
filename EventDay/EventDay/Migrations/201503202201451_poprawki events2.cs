namespace EventDay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class poprawkievents2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "AccessId", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "RecruitmentId", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "ZipCode", c => c.String(nullable: false, maxLength: 6));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "ZipCode", c => c.String(maxLength: 6));
            AlterColumn("dbo.Events", "RecruitmentId", c => c.String());
            AlterColumn("dbo.Events", "AccessId", c => c.String());
        }
    }
}
