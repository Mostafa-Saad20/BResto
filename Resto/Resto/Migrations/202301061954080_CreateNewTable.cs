namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNewTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        Subtotal = c.Decimal(precision: 18, scale: 2),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "CustomerId", "dbo.Customers");
            DropIndex("dbo.CartItems", new[] { "CustomerId" });
            DropTable("dbo.CartItems");
        }
    }
}
