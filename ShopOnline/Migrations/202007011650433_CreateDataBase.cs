namespace ShopOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                        RegistrationDate = c.DateTime(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductCount = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Client_ID = c.Int(),
                        Product_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.Client_ID)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .Index(t => t.Client_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        DateAdd = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.Orders", "Client_ID", "dbo.Clients");
            DropIndex("dbo.Orders", new[] { "Product_ID" });
            DropIndex("dbo.Orders", new[] { "Client_ID" });
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.Clients");
        }
    }
}
