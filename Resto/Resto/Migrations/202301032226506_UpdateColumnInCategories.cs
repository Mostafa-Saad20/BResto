namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColumnInCategories : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Image", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Image", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
        }
    }
}
