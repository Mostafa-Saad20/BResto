namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserPhone = c.String(),
                        UserAddress = c.String(),
                        Type = c.Int(nullable: false),
                        TotalPrice = c.Int(nullable: false),
                        Discount = c.Int(),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserPhone = c.String(),
                        NumberOfGuests = c.Int(nullable: false),
                        NumberOfTables = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Foods", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Foods", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Foods", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Foods", new[] { "User_Id" });
            DropIndex("dbo.Foods", new[] { "Order_Id" });
            DropIndex("dbo.Foods", new[] { "CategoryId" });
            DropTable("dbo.Users");
            DropTable("dbo.Reservations");
            DropTable("dbo.Orders");
            DropTable("dbo.Foods");
            DropTable("dbo.Categories");
        }
    }
}
