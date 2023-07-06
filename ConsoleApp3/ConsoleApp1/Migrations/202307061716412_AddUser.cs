namespace ConsoleApp1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Orders", "UserId");
            AddForeignKey("dbo.Orders", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropTable("dbo.Users");
        }
    }
}
