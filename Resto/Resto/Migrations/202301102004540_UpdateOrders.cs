namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrders : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "PaymentMethod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "PaymentMethod", c => c.String(nullable: false));
        }
    }
}
