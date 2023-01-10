namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrdersAndReservations : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "CardOwnerName");
            DropColumn("dbo.Orders", "CardNumber");
            DropColumn("dbo.Orders", "ExpDate");
            DropColumn("dbo.Orders", "CVV");
            DropColumn("dbo.Reservations", "PaymentMethod");
            DropColumn("dbo.Reservations", "CardOwnerName");
            DropColumn("dbo.Reservations", "CardNumber");
            DropColumn("dbo.Reservations", "ExpDate");
            DropColumn("dbo.Reservations", "CVV");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "CVV", c => c.Int());
            AddColumn("dbo.Reservations", "ExpDate", c => c.String());
            AddColumn("dbo.Reservations", "CardNumber", c => c.String());
            AddColumn("dbo.Reservations", "CardOwnerName", c => c.String());
            AddColumn("dbo.Reservations", "PaymentMethod", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "CVV", c => c.String());
            AddColumn("dbo.Orders", "ExpDate", c => c.String());
            AddColumn("dbo.Orders", "CardNumber", c => c.String());
            AddColumn("dbo.Orders", "CardOwnerName", c => c.String());
        }
    }
}
