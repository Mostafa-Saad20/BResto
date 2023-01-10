namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableFood : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Foods", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Foods", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Foods", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Foods", "Price", c => c.String());
            AlterColumn("dbo.Foods", "Description", c => c.String());
            AlterColumn("dbo.Foods", "Name", c => c.String());
        }
    }
}
