namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServerImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("Servers", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("Servers", "Image");
        }
    }
}
