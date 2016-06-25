namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServerModification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Modifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Version = c.String(unicode: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)                ;
            
            CreateTable(
                "ServerModifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Modification_Id = c.Int(),
                        Server_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("Modifications", t => t.Modification_Id)
                .ForeignKey("Servers", t => t.Server_Id)
                .Index(t => t.Modification_Id)
                .Index(t => t.Server_Id);
            
            CreateTable(
                "Servers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        ClientVersion = c.String(unicode: false),
                        IpAddress = c.String(unicode: false),
                        Port = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)                ;
            
        }
        
        public override void Down()
        {
            DropForeignKey("ServerModifications", "Server_Id", "Servers");
            DropForeignKey("ServerModifications", "Modification_Id", "Modifications");
            DropIndex("ServerModifications", new[] { "Server_Id" });
            DropIndex("ServerModifications", new[] { "Modification_Id" });
            DropTable("Servers");
            DropTable("ServerModifications");
            DropTable("Modifications");
        }
    }
}
