namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class News : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(unicode: false),
                        TimeCreate = c.DateTime(nullable: false, precision: 0),
                        Author_Id = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("AspNetUsers", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(unicode: false),
                        Text = c.String(unicode: false),
                        Image = c.Binary(),
                        TimeCreate = c.DateTime(nullable: false, precision: 0),
                        ViewersCount = c.Int(nullable: false),
                        Author_Id = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("AspNetUsers", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("News", "Author_Id", "AspNetUsers");
            DropForeignKey("Comments", "Author_Id", "AspNetUsers");
            DropIndex("News", new[] { "Author_Id" });
            DropIndex("Comments", new[] { "Author_Id" });
            DropTable("News");
            DropTable("Comments");
        }
    }
}
