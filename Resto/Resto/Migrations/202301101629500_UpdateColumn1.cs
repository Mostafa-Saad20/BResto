namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColumn1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "CardNumber", c => c.String());
            AlterColumn("dbo.Reservations", "CardNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "CardNumber", c => c.Int());
            AlterColumn("dbo.Orders", "CardNumber", c => c.Int());
        }
    }
}
