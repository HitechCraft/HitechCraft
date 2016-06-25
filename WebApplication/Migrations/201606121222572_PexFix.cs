namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PexFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("PexInheritances", "child", c => c.String(unicode: false));
            DropColumn("PexInheritances", "chiled");
        }
        
        public override void Down()
        {
            AddColumn("PexInheritances", "chiled", c => c.String(unicode: false));
            DropColumn("PexInheritances", "child");
        }
    }
}
