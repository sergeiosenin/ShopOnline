namespace ShopOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06072020 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Client_ID", "dbo.Clients");
            DropForeignKey("dbo.Orders", "Product_ID", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "Client_ID" });
            DropIndex("dbo.Orders", new[] { "Product_ID" });
            AlterColumn("dbo.Orders", "Client_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "Product_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.Orders", "Client_ID");
            CreateIndex("dbo.Orders", "Product_ID");
            AddForeignKey("dbo.Orders", "Client_ID", "dbo.Clients", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "Product_ID", "dbo.Products", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.Orders", "Client_ID", "dbo.Clients");
            DropIndex("dbo.Orders", new[] { "Product_ID" });
            DropIndex("dbo.Orders", new[] { "Client_ID" });
            AlterColumn("dbo.Products", "Name", c => c.String());
            AlterColumn("dbo.Orders", "Product_ID", c => c.Int());
            AlterColumn("dbo.Orders", "Client_ID", c => c.Int());
            CreateIndex("dbo.Orders", "Product_ID");
            CreateIndex("dbo.Orders", "Client_ID");
            AddForeignKey("dbo.Orders", "Product_ID", "dbo.Products", "ID");
            AddForeignKey("dbo.Orders", "Client_ID", "dbo.Clients", "ID");
        }
    }
}
