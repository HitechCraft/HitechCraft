namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IKTransaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "IKTransactions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Player_Name = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("Players", t => t.Player_Name)
                .Index(t => t.Player_Name);
        }
        
        public override void Down()
        {
            DropForeignKey("IKTransactions", "Player_Name", "Players");
            DropIndex("IKTransactions", new[] { "Player_Name" });
            DropTable("IKTransactions");
        }
    }
}
