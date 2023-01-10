namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnInCartItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartItems", "FoodId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CartItems", "FoodId");
        }
    }
}
