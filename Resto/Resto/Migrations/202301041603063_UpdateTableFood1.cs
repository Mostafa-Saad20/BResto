namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableFood1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Foods", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Foods", "Description", c => c.String(nullable: false));
        }
    }
}
