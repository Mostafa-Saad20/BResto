namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColumnInAdmins : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Admins", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "Password", c => c.String());
            AlterColumn("dbo.Admins", "Email", c => c.String());
        }
    }
}
