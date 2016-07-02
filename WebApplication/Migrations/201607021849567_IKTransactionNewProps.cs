namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IKTransactionNewProps : DbMigration
    {
        public override void Up()
        {
            AddColumn("IKTransactions", "Status", c => c.Int(nullable: false));
            AddColumn("IKTransactions", "Time", c => c.DateTime(nullable: false, precision: 0));
        }
        
        public override void Down()
        {
            DropColumn("IKTransactions", "Time");
            DropColumn("IKTransactions", "Status");
        }
    }
}
