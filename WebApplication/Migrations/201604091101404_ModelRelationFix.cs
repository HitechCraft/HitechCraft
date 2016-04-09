namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelRelationFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlayerItems", "user_id", c => c.String(maxLength: 128, storeType: "nvarchar"));
            CreateIndex("dbo.PlayerItems", "user_id");
            AddForeignKey("dbo.PlayerItems", "user_id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerItems", "user_id", "dbo.AspNetUsers");
            DropIndex("dbo.PlayerItems", new[] { "user_id" });
            DropColumn("dbo.PlayerItems", "user_id");
        }
    }
}