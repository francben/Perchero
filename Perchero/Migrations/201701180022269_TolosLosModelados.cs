namespace Perchero.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TolosLosModelados : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Avios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Precio = c.Int(nullable: false),
                        DetallePrenda_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DetallePrendas", t => t.DetallePrenda_Id)
                .Index(t => t.DetallePrenda_Id);
            
            CreateTable(
                "dbo.DetallePrendas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrendaId = c.Int(nullable: false),
                        MetroTela = c.Double(nullable: false),
                        CantidadAvio = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prendas", t => t.PrendaId, cascadeDelete: true)
                .Index(t => t.PrendaId);
            
            CreateTable(
                "dbo.Prendas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        TipoId = c.Int(nullable: false),
                        Nombre = c.String(nullable: false),
                        Imagen = c.String(),
                        Talle = c.Int(nullable: false),
                        Vitrina = c.Boolean(nullable: false),
                        PrecioTotal = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tipoes", t => t.TipoId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TipoId);
            
            CreateTable(
                "dbo.Tipoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Precio = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Telas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Color = c.String(nullable: false),
                        Precio = c.Int(nullable: false),
                        DetallePrenda_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DetallePrendas", t => t.DetallePrenda_Id)
                .Index(t => t.DetallePrenda_Id);
            
            CreateTable(
                "dbo.Pedidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrendaId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        FechaPedido = c.String(nullable: false),
                        FechaEntrega = c.String(nullable: false),
                        Seña = c.String(nullable: false),
                        Saldo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prendas", t => t.PrendaId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.PrendaId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedidoes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pedidoes", "PrendaId", "dbo.Prendas");
            DropForeignKey("dbo.Telas", "DetallePrenda_Id", "dbo.DetallePrendas");
            DropForeignKey("dbo.DetallePrendas", "PrendaId", "dbo.Prendas");
            DropForeignKey("dbo.Prendas", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Prendas", "TipoId", "dbo.Tipoes");
            DropForeignKey("dbo.Avios", "DetallePrenda_Id", "dbo.DetallePrendas");
            DropIndex("dbo.Pedidoes", new[] { "UserId" });
            DropIndex("dbo.Pedidoes", new[] { "PrendaId" });
            DropIndex("dbo.Telas", new[] { "DetallePrenda_Id" });
            DropIndex("dbo.Prendas", new[] { "TipoId" });
            DropIndex("dbo.Prendas", new[] { "UserId" });
            DropIndex("dbo.DetallePrendas", new[] { "PrendaId" });
            DropIndex("dbo.Avios", new[] { "DetallePrenda_Id" });
            DropTable("dbo.Pedidoes");
            DropTable("dbo.Telas");
            DropTable("dbo.Tipoes");
            DropTable("dbo.Prendas");
            DropTable("dbo.DetallePrendas");
            DropTable("dbo.Avios");
        }
    }
}
