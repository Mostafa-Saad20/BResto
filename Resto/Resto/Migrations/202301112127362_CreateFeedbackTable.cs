namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Web.Services.Description;

    public partial class CreateFeedbackTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedback",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Email = c.String(),
                    Message = c.String(),
                })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.Feedback");
        }
    }
}
