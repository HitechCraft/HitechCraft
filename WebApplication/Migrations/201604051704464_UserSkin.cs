namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserSkin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSkins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128, storeType: "nvarchar"),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSkins", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserSkins", new[] { "UserId" });
            DropTable("dbo.UserSkins");
        }
    }
}
