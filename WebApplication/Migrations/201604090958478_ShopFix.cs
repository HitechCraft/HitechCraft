namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShopFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ShopItems", "Name", c => c.String(unicode: false));
            AlterColumn("dbo.ShopItems", "Description", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShopItems", "Description", c => c.Int(nullable: false));
            AlterColumn("dbo.ShopItems", "Name", c => c.Int(nullable: false));
        }
    }
}
