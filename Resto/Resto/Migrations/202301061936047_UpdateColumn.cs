namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Carts", "CustomerId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Carts", "CustomerId", c => c.Int(nullable: false));
        }
    }
}
