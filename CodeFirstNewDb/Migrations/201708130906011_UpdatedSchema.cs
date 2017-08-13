namespace CodeFirstNewDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Category = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductPositions",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PurchaseId, t.ProductId })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Purchases", t => t.PurchaseId, cascadeDelete: true)
                .Index(t => t.PurchaseId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductPositions", "PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.ProductPositions", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductPositions", new[] { "ProductId" });
            DropIndex("dbo.ProductPositions", new[] { "PurchaseId" });
            DropTable("dbo.Purchases");
            DropTable("dbo.ProductPositions");
            DropTable("dbo.Products");
        }
    }
}
