namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("Comments", "News_Id", c => c.Int());
            CreateIndex("Comments", "News_Id");
            AddForeignKey("Comments", "News_Id", "News", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Comments", "News_Id", "News");
            DropIndex("Comments", new[] { "News_Id" });
            DropColumn("Comments", "News_Id");
        }
    }
}
