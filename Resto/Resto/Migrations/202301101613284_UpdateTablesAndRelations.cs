namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTablesAndRelations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payments", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.Payments", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Payments", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Payments", new[] { "CustomerId" });
            DropIndex("dbo.Payments", new[] { "OrderId" });
            DropIndex("dbo.Payments", new[] { "ReservationId" });
            AddColumn("dbo.Orders", "CardOwnerName", c => c.String());
            AddColumn("dbo.Orders", "CardNumber", c => c.Int());
            AddColumn("dbo.Orders", "ExpDate", c => c.String());
            AddColumn("dbo.Orders", "CVV", c => c.Int());
            AddColumn("dbo.Reservations", "PaymentMethod", c => c.String(nullable: false));
            AddColumn("dbo.Reservations", "CardOwnerName", c => c.String());
            AddColumn("dbo.Reservations", "CardNumber", c => c.Int());
            AddColumn("dbo.Reservations", "ExpDate", c => c.String());
            AddColumn("dbo.Reservations", "CVV", c => c.Int());
            DropTable("dbo.Payments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardOwnerName = c.String(nullable: false),
                        CardNumber = c.Int(nullable: false),
                        ExpDate = c.String(nullable: false),
                        CVV = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        ReservationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Reservations", "CVV");
            DropColumn("dbo.Reservations", "ExpDate");
            DropColumn("dbo.Reservations", "CardNumber");
            DropColumn("dbo.Reservations", "CardOwnerName");
            DropColumn("dbo.Reservations", "PaymentMethod");
            DropColumn("dbo.Orders", "CVV");
            DropColumn("dbo.Orders", "ExpDate");
            DropColumn("dbo.Orders", "CardNumber");
            DropColumn("dbo.Orders", "CardOwnerName");
            CreateIndex("dbo.Payments", "ReservationId");
            CreateIndex("dbo.Payments", "OrderId");
            CreateIndex("dbo.Payments", "CustomerId");
            AddForeignKey("dbo.Payments", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Payments", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Payments", "ReservationId", "dbo.Reservations", "Id", cascadeDelete: true);
        }
    }
}
