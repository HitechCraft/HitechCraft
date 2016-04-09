namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Currency : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(unicode: false),
                        user_id = c.String(maxLength: 128, storeType: "nvarchar"),
                        balance = c.Double(nullable: false),
                        realmoney = c.Double(nullable: false),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.user_id)
                .Index(t => t.user_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Currencies", "user_id", "dbo.AspNetUsers");
            DropIndex("dbo.Currencies", new[] { "user_id" });
            DropTable("dbo.Currencies");
        }
    }
}
