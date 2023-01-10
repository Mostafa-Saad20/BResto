namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableOrders1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "DeliveryAddress", c => c.String());
            AlterColumn("dbo.Orders", "Type", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Type", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "DeliveryAddress", c => c.String(nullable: false));
        }
    }
}
