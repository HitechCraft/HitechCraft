namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PexFixDefault : DbMigration
    {
        public override void Up()
        {
            AlterColumn("PexEntities", "isDefault", c => c.String(nullable: false, defaultValue: "0"));
        }
        
        public override void Down()
        {
        }
    }
}
