namespace EventDay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ocenki : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentCategories",
                c => new
                    {
                        CommentCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CommentCategoryId);
            
            AddColumn("dbo.Comments", "CommentCategoryId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Comments", "CommentCategoryId", "dbo.CommentCategories", "CommentCategoryId", cascadeDelete: true);
            CreateIndex("dbo.Comments", "CommentCategoryId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "CommentCategoryId" });
            DropForeignKey("dbo.Comments", "CommentCategoryId", "dbo.CommentCategories");
            DropColumn("dbo.Comments", "CommentCategoryId");
            DropTable("dbo.CommentCategories");
        }
    }
}
