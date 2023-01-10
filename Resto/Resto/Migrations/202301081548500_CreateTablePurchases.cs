namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTablePurchases : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OnlinePurchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardOwnerName = c.String(nullable: false),
                        CardNumber = c.Int(nullable: false),
                        ExpDate = c.String(nullable: false),
                        CVV = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            AddColumn("dbo.Orders", "PaymentMethod", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "Copun", c => c.Int());
            DropColumn("dbo.Orders", "Discount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Discount", c => c.Int());
            DropForeignKey("dbo.OnlinePurchases", "CustomerId", "dbo.Customers");
            DropIndex("dbo.OnlinePurchases", new[] { "CustomerId" });
            DropColumn("dbo.Orders", "Copun");
            DropColumn("dbo.Orders", "PaymentMethod");
            DropTable("dbo.OnlinePurchases");
        }
    }
}
