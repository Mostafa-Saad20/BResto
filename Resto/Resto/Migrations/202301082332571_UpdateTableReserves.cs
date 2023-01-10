namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableReserves : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "Date", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Reservations", "Time", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "Time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Reservations", "Date", c => c.DateTime(nullable: false));
        }
    }
}
