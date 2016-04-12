namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlayerModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BanIps",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 128, storeType: "nvarchar"),
                        lastip = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Players", t => t.name)
                .Index(t => t.name);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Id = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Bans",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 128, storeType: "nvarchar"),
                        reason = c.String(unicode: false),
                        admin = c.String(maxLength: 128, storeType: "nvarchar"),
                        time = c.Int(nullable: false),
                        temptime = c.Int(nullable: false),
                        type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Players", t => t.admin)
                .ForeignKey("dbo.Players", t => t.name)
                .Index(t => t.name)
                .Index(t => t.admin);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(maxLength: 128, storeType: "nvarchar"),
                        balance = c.Double(nullable: false),
                        realmoney = c.Double(nullable: false),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Players", t => t.username)
                .Index(t => t.username);
            
            CreateTable(
                "dbo.PlayerItems",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nickname = c.String(maxLength: 128, storeType: "nvarchar"),
                        item_id = c.Int(nullable: false),
                        item_amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ShopItems", t => t.item_id, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.nickname)
                .Index(t => t.nickname)
                .Index(t => t.item_id);
            
            CreateTable(
                "dbo.ShopItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        Amount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerItems", "nickname", "dbo.Players");
            DropForeignKey("dbo.PlayerItems", "item_id", "dbo.ShopItems");
            DropForeignKey("dbo.Currencies", "username", "dbo.Players");
            DropForeignKey("dbo.Bans", "name", "dbo.Players");
            DropForeignKey("dbo.Bans", "admin", "dbo.Players");
            DropForeignKey("dbo.BanIps", "name", "dbo.Players");
            DropForeignKey("dbo.Players", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.PlayerItems", new[] { "item_id" });
            DropIndex("dbo.PlayerItems", new[] { "nickname" });
            DropIndex("dbo.Currencies", new[] { "username" });
            DropIndex("dbo.Bans", new[] { "admin" });
            DropIndex("dbo.Bans", new[] { "name" });
            DropIndex("dbo.Players", new[] { "UserId" });
            DropIndex("dbo.BanIps", new[] { "name" });
            DropTable("dbo.ShopItems");
            DropTable("dbo.PlayerItems");
            DropTable("dbo.Currencies");
            DropTable("dbo.Bans");
            DropTable("dbo.Players");
            DropTable("dbo.BanIps");
        }
    }
}
