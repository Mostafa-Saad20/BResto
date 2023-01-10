namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRelationships : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OnlinePurchases", "CustomerId", "dbo.Customers");
            DropIndex("dbo.OnlinePurchases", new[] { "CustomerId" });
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardOwnerName = c.String(nullable: false),
                        CardNumber = c.Int(nullable: false),
                        ExpDate = c.String(nullable: false),
                        CVV = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: true),
                        OrderId = c.Int(nullable: true),
                        ReservationId = c.Int(nullable: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reservations", t => t.ReservationId, cascadeDelete: false)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: false)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: false)
                .Index(t => t.CustomerId)
                .Index(t => t.OrderId)
                .Index(t => t.ReservationId);
            
            DropTable("dbo.OnlinePurchases");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OnlinePurchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardOwnerName = c.String(nullable: false),
                        CardNumber = c.Int(nullable: false),
                        ExpDate = c.String(nullable: false),
                        CVV = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Payments", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Payments", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Payments", "ReservationId", "dbo.Reservations");
            DropIndex("dbo.Payments", new[] { "ReservationId" });
            DropIndex("dbo.Payments", new[] { "OrderId" });
            DropIndex("dbo.Payments", new[] { "CustomerId" });
            DropTable("dbo.Payments");
            CreateIndex("dbo.OnlinePurchases", "CustomerId");
            AddForeignKey("dbo.OnlinePurchases", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
