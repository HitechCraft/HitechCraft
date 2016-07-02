namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTransactionStatus : DbMigration
    {
        public override void Up()
        {
            DropColumn("IKTransactions", "Status");
        }
        
        public override void Down()
        {
            AddColumn("IKTransactions", "Status", c => c.Int(nullable: false));
        }
    }
}
