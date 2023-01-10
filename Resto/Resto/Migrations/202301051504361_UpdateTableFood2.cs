namespace Resto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableFood2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Foods", "CartId", "dbo.Carts");
            DropIndex("dbo.Foods", new[] { "CartId" });
            AlterColumn("dbo.Foods", "CartId", c => c.Int());
            CreateIndex("dbo.Foods", "CartId");
            AddForeignKey("dbo.Foods", "CartId", "dbo.Carts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Foods", "CartId", "dbo.Carts");
            DropIndex("dbo.Foods", new[] { "CartId" });
            AlterColumn("dbo.Foods", "CartId", c => c.Int(nullable: false));
            CreateIndex("dbo.Foods", "CartId");
            AddForeignKey("dbo.Foods", "CartId", "dbo.Carts", "Id", cascadeDelete: true);
        }
    }
}
