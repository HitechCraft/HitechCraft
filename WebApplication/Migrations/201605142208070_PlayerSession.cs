namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlayerSession : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "PlayerSessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerName = c.String(maxLength: 128, storeType: "nvarchar"),
                        Session = c.String(unicode: false),
                        Server = c.String(unicode: false),
                        Token = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("Players", t => t.PlayerName)
                .Index(t => t.PlayerName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("PlayerSessions", "PlayerName", "Players");
            DropIndex("PlayerSessions", new[] { "PlayerName" });
            DropTable("PlayerSessions");
        }
    }
}
