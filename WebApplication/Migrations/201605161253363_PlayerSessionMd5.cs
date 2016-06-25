namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlayerSessionMd5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("PlayerSessions", "Md5", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("PlayerSessions", "Md5");
        }
    }
}
