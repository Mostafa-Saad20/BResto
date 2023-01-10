namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableCartAndFood : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Foods", "CartId", "dbo.Carts");
            DropIndex("dbo.Foods", new[] { "CartId" });
            AddColumn("dbo.Carts", "ItemName", c => c.String());
            AddColumn("dbo.Carts", "Quantity", c => c.String());
            AddColumn("dbo.Carts", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Foods", "CartId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Foods", "CartId", c => c.Int());
            DropColumn("dbo.Carts", "Price");
            DropColumn("dbo.Carts", "Quantity");
            DropColumn("dbo.Carts", "ItemName");
            CreateIndex("dbo.Foods", "CartId");
            AddForeignKey("dbo.Foods", "CartId", "dbo.Carts", "Id");
        }
    }
}
