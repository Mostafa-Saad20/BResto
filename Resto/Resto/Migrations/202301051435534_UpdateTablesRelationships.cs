namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTablesRelationships : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "Customers");
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ItemsCount = c.Int(),
                        Subtotal = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Id)
                .Index(t => t.Id);
            
            AddColumn("dbo.Foods", "CartId", c => c.Int());
            AddColumn("dbo.Orders", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "CustomerPhone", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "DeliveryAddress", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Reservations", "Phone", c => c.String(nullable: false));
            AddColumn("dbo.Reservations", "CustomerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Reservations", "NumberOfGuests", c => c.String(nullable: false));
            AlterColumn("dbo.Reservations", "NumberOfTables", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Address", c => c.String(nullable: false));
            CreateIndex("dbo.Orders", "CustomerId");
            CreateIndex("dbo.Reservations", "CustomerId");
            CreateIndex("dbo.Foods", "CartId");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reservations", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Foods", "CartId", "dbo.Carts", "Id");
            DropColumn("dbo.Orders", "UserName");
            DropColumn("dbo.Orders", "UserPhone");
            DropColumn("dbo.Orders", "UserAddress");
            DropColumn("dbo.Reservations", "UserName");
            DropColumn("dbo.Reservations", "UserPhone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "UserPhone", c => c.String());
            AddColumn("dbo.Reservations", "UserName", c => c.String());
            AddColumn("dbo.Orders", "UserAddress", c => c.String());
            AddColumn("dbo.Orders", "UserPhone", c => c.String());
            AddColumn("dbo.Orders", "UserName", c => c.String());
            DropForeignKey("dbo.Foods", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.Reservations", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Carts", "Id", "dbo.Customers");
            DropIndex("dbo.Foods", new[] { "Cart_Id" });
            DropIndex("dbo.Reservations", new[] { "CustomerId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.Carts", new[] { "Id" });
            AlterColumn("dbo.Customers", "Address", c => c.String());
            AlterColumn("dbo.Customers", "Phone", c => c.String());
            AlterColumn("dbo.Customers", "Password", c => c.String());
            AlterColumn("dbo.Customers", "Email", c => c.String());
            AlterColumn("dbo.Customers", "Name", c => c.String());
            AlterColumn("dbo.Reservations", "NumberOfTables", c => c.Int(nullable: false));
            AlterColumn("dbo.Reservations", "NumberOfGuests", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "TotalPrice", c => c.Int(nullable: false));
            DropColumn("dbo.Reservations", "CustomerId");
            DropColumn("dbo.Reservations", "Phone");
            DropColumn("dbo.Reservations", "Name");
            DropColumn("dbo.Orders", "CustomerId");
            DropColumn("dbo.Orders", "DeliveryAddress");
            DropColumn("dbo.Orders", "CustomerPhone");
            DropColumn("dbo.Orders", "Name");
            DropColumn("dbo.Foods", "Cart_Id");
            DropTable("dbo.Carts");
            RenameTable(name: "dbo.Customers", newName: "Users");
        }
    }
}
