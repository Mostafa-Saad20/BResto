namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableCart2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Carts", "Quantity", c => c.Int());
            AlterColumn("dbo.Carts", "Price", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Carts", "ItemsCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "ItemsCount", c => c.Int());
            AlterColumn("dbo.Carts", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Carts", "Quantity", c => c.String());
        }
    }
}
