namespace PlantSpaCrud.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cls : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Plants",
                c => new
                    {
                        PlantId = c.Int(nullable: false, identity: true),
                        PlantName = c.String(),
                        FlowerBearing = c.Boolean(nullable: false),
                        PlantPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        ImageUrl = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlantId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.PlantCareTypes",
                c => new
                    {
                        PlantCareTypeId = c.Int(nullable: false, identity: true),
                        InsecticideName = c.String(),
                        Fertilizer = c.String(),
                        PlantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlantCareTypeId)
                .ForeignKey("dbo.Plants", t => t.PlantId, cascadeDelete: true)
                .Index(t => t.PlantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlantCareTypes", "PlantId", "dbo.Plants");
            DropForeignKey("dbo.Plants", "CategoryId", "dbo.Categories");
            DropIndex("dbo.PlantCareTypes", new[] { "PlantId" });
            DropIndex("dbo.Plants", new[] { "CategoryId" });
            DropTable("dbo.PlantCareTypes");
            DropTable("dbo.Plants");
            DropTable("dbo.Categories");
        }
    }
}
