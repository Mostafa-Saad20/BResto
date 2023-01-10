namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Carts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        ItemPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemQuantity = c.Int(nullable: false),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
