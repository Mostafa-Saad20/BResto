namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropColumnInCartItem : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CartItems", "Subtotal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CartItems", "Subtotal", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
