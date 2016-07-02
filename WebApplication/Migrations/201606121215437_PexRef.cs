namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PexRef : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "Pexes", newName: "Permissions");
        }
        
        public override void Down()
        {
            RenameTable(name: "Permissions", newName: "Pexes");
        }
    }
}
