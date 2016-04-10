namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BanObjects : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BanIps",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(unicode: false),
                        UserId = c.String(maxLength: 128, storeType: "nvarchar"),
                        lastip = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Bans",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(unicode: false),
                        BanedUserId = c.String(maxLength: 128, storeType: "nvarchar"),
                        reason = c.String(unicode: false),
                        admin = c.String(unicode: false),
                        AdminUserId = c.String(maxLength: 128, storeType: "nvarchar"),
                        time = c.Int(nullable: false),
                        temptime = c.Int(nullable: false),
                        type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.AdminUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.BanedUserId)
                .Index(t => t.BanedUserId)
                .Index(t => t.AdminUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bans", "BanedUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bans", "AdminUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BanIps", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Bans", new[] { "AdminUserId" });
            DropIndex("dbo.Bans", new[] { "BanedUserId" });
            DropIndex("dbo.BanIps", new[] { "UserId" });
            DropTable("dbo.Bans");
            DropTable("dbo.BanIps");
        }
    }
}
