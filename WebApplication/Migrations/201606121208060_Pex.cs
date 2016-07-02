namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pex : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Pexes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(unicode: false),
                        permission = c.String(unicode: false),
                        type = c.Int(nullable: false),
                        value = c.String(unicode: false),
                        world = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.id)                ;
            
            CreateTable(
                "PexEntities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(unicode: false),
                        type = c.Int(nullable: false),
                        isDefault = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)                ;
            
            CreateTable(
                "PexInheritances",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        chiled = c.String(unicode: false),
                        parent = c.String(unicode: false),
                        type = c.Int(nullable: false),
                        world = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.id)                ;
            
        }
        
        public override void Down()
        {
            DropTable("PexInheritances");
            DropTable("PexEntities");
            DropTable("Pexes");
        }
    }
}
