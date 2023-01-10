namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCartsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItems", "CartId", "dbo.Carts");
            DropIndex("dbo.CartItems", new[] { "CartId" });
            AddColumn("dbo.Carts", "ItemName", c => c.String());
            AddColumn("dbo.Carts", "ItemPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Carts", "ItemQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.Carts", "Subtotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Carts", "ItemsCount");
            DropTable("dbo.CartItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CartId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Carts", "ItemsCount", c => c.Int());
            DropColumn("dbo.Carts", "Subtotal");
            DropColumn("dbo.Carts", "ItemQuantity");
            DropColumn("dbo.Carts", "ItemPrice");
            DropColumn("dbo.Carts", "ItemName");
            CreateIndex("dbo.CartItems", "CartId");
            AddForeignKey("dbo.CartItems", "CartId", "dbo.Carts", "Id", cascadeDelete: true);
        }
    }
}
