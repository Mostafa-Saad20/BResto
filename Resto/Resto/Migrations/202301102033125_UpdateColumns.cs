namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Date", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Orders", "Time", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Orders", "Copun");
            DropColumn("dbo.Orders", "DateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "DateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "Copun", c => c.Int());
            DropColumn("dbo.Orders", "Time");
            DropColumn("dbo.Orders", "Date");
        }
    }
}
