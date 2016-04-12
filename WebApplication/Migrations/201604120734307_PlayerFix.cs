namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlayerFix : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Players", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "Id", c => c.Int(nullable: false));
        }
    }
}
