namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BanIps",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 128, storeType: "nvarchar"),
                        lastip = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("Players", t => t.name)
                .Index(t => t.name);
            
            CreateTable(
                "Players",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Name)                
                .ForeignKey("AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Email = c.String(maxLength: 255, storeType: "nvarchar"),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        Gender = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)                
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })                
                .ForeignKey("AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })                
                .ForeignKey("AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "Bans",
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
                .ForeignKey("Players", t => t.admin)
                .ForeignKey("Players", t => t.name)
                .Index(t => t.name)
                .Index(t => t.admin);
            
            CreateTable(
                "Currencies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(maxLength: 128, storeType: "nvarchar"),
                        balance = c.Double(nullable: false),
                        realmoney = c.Double(nullable: false),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("Players", t => t.username)
                .Index(t => t.username);
            
            CreateTable(
                "PlayerItems",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nickname = c.String(maxLength: 128, storeType: "nvarchar"),
                        item_id = c.String(maxLength: 128, storeType: "nvarchar"),
                        item_amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("ShopItems", t => t.item_id)
                .ForeignKey("Players", t => t.nickname)
                .Index(t => t.nickname)
                .Index(t => t.item_id);
            
            CreateTable(
                "ShopItems",
                c => new
                    {
                        GameId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        Image = c.Binary(),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.GameId)                ;
            
            CreateTable(
                "AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)                
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "UserSkins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128, storeType: "nvarchar"),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("UserSkins", "UserId", "AspNetUsers");
            DropForeignKey("AspNetUserRoles", "RoleId", "AspNetRoles");
            DropForeignKey("PlayerItems", "nickname", "Players");
            DropForeignKey("PlayerItems", "item_id", "ShopItems");
            DropForeignKey("Currencies", "username", "Players");
            DropForeignKey("Bans", "name", "Players");
            DropForeignKey("Bans", "admin", "Players");
            DropForeignKey("BanIps", "name", "Players");
            DropForeignKey("Players", "UserId", "AspNetUsers");
            DropForeignKey("AspNetUserRoles", "UserId", "AspNetUsers");
            DropForeignKey("AspNetUserLogins", "UserId", "AspNetUsers");
            DropForeignKey("AspNetUserClaims", "UserId", "AspNetUsers");
            DropIndex("UserSkins", new[] { "UserId" });
            DropIndex("AspNetRoles", "RoleNameIndex");
            DropIndex("PlayerItems", new[] { "item_id" });
            DropIndex("PlayerItems", new[] { "nickname" });
            DropIndex("Currencies", new[] { "username" });
            DropIndex("Bans", new[] { "admin" });
            DropIndex("Bans", new[] { "name" });
            DropIndex("AspNetUserRoles", new[] { "RoleId" });
            DropIndex("AspNetUserRoles", new[] { "UserId" });
            DropIndex("AspNetUserLogins", new[] { "UserId" });
            DropIndex("AspNetUserClaims", new[] { "UserId" });
            DropIndex("AspNetUsers", "UserNameIndex");
            DropIndex("Players", new[] { "UserId" });
            DropIndex("BanIps", new[] { "name" });
            DropTable("UserSkins");
            DropTable("AspNetRoles");
            DropTable("ShopItems");
            DropTable("PlayerItems");
            DropTable("Currencies");
            DropTable("Bans");
            DropTable("AspNetUserRoles");
            DropTable("AspNetUserLogins");
            DropTable("AspNetUserClaims");
            DropTable("AspNetUsers");
            DropTable("Players");
            DropTable("BanIps");
        }
    }
}
