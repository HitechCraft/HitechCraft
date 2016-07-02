namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServerMap : DbMigration
    {
        public override void Up()
        {
            AddColumn("Servers", "MapPort", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Servers", "MapPort");
        }
    }
}
