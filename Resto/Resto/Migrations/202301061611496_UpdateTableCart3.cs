namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableCart3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Carts", "ItemName");
            DropColumn("dbo.Carts", "Price");
            DropColumn("dbo.Carts", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "Quantity", c => c.Int());
            AddColumn("dbo.Carts", "Price", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Carts", "ItemName", c => c.String());
        }
    }
}
