namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnInFood : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Foods", "Size", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Foods", "Size");
        }
    }
}
