namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableCartItems : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Carts", new[] { "Id" });
            DropPrimaryKey("dbo.Carts");
            AddColumn("dbo.Carts", "CustomerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Carts", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Carts", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CartItems");
            AlterColumn("dbo.CartItems", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.CartItems", "CustomerId");
            AddPrimaryKey("dbo.CartItems", "Id");
            CreateIndex("dbo.CartItems", "Id");
            AddForeignKey("dbo.Carts", "Id", "dbo.Customers", "Id");
            RenameTable(name: "dbo.CartItems", newName: "Carts");
        }
    }
}
