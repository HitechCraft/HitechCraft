namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Shop : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlayerItems",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nickname = c.String(unicode: false),
                        item_id = c.Int(nullable: false),
                        item_amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ShopItems", t => t.item_id, cascadeDelete: true)
                .Index(t => t.item_id);
            
            CreateTable(
                "dbo.ShopItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                        Description = c.Int(nullable: false),
                        Amount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerItems", "item_id", "dbo.ShopItems");
            DropIndex("dbo.PlayerItems", new[] { "item_id" });
            DropTable("dbo.ShopItems");
            DropTable("dbo.PlayerItems");
        }
    }
}
