namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Carts", new[] { "CustomerId" });
            AlterColumn("dbo.Carts", "CustomerId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Carts", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "CustomerId");
            AddForeignKey("dbo.Carts", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
