namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCartsColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "ItemsCount", c => c.Int());
            DropColumn("dbo.Carts", "Subtotal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "Subtotal", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Carts", "ItemsCount");
        }
    }
}
