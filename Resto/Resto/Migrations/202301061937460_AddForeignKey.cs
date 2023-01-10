namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "CustomerId", "dbo.Customers");
        }
        
        public override void Down()
        {
           
        }
    }
}
