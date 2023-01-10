namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColumn2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "CVV", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "CVV", c => c.Int());
        }
    }
}
