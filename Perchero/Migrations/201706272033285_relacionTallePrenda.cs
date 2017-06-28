namespace Perchero.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relacionTallePrenda : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TallePrendas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TalleId = c.Int(nullable: false),
                        PrendaId = c.Int(nullable: false),
                        NUmeroTalle = c.Int(nullable: false),
                        Busto = c.Int(nullable: false),
                        Cintura = c.Int(nullable: false),
                        Cadera = c.Int(nullable: false),
                        AnchoEspalda = c.Int(nullable: false),
                        AnchoTorax = c.Int(nullable: false),
                        Hombro = c.Int(nullable: false),
                        Cuello = c.Int(nullable: false),
                        AlturaBusto = c.Int(nullable: false),
                        Pinza = c.Int(nullable: false),
                        SeparacionBusto = c.Int(nullable: false),
                        ContornoBrazo = c.Int(nullable: false),
                        PuñoAjustado = c.Int(nullable: false),
                        PuñoFlojo = c.Int(nullable: false),
                        TalleEspalda = c.Int(nullable: false),
                        TalleDelantero = c.Int(nullable: false),
                        AlturaAxila = c.Int(nullable: false),
                        AlturaRodilla = c.Int(nullable: false),
                        AlturaCCadera = c.Int(nullable: false),
                        LargoCinturaSuelo = c.Int(nullable: false),
                        TiroPaantalonDelantero = c.Int(nullable: false),
                        LargoManga = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prendas", t => t.PrendaId, cascadeDelete: true)
                .ForeignKey("dbo.Talles", t => t.TalleId, cascadeDelete: true)
                .Index(t => t.TalleId)
                .Index(t => t.PrendaId);
            
            CreateTable(
                "dbo.Talles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroTalle = c.Int(nullable: false),
                        Busto = c.Int(nullable: false),
                        Cintura = c.Int(nullable: false),
                        Cadera = c.Int(nullable: false),
                        AnchoEspalda = c.Int(nullable: false),
                        AnchoTorax = c.Int(nullable: false),
                        Hombro = c.Int(nullable: false),
                        Cuello = c.Int(nullable: false),
                        AlturaBusto = c.Int(nullable: false),
                        Pinza = c.Int(nullable: false),
                        SeparacionBusto = c.Int(nullable: false),
                        ContornoBrazo = c.Int(nullable: false),
                        PuñoAjustado = c.Int(nullable: false),
                        PuñoFlojo = c.Int(nullable: false),
                        TalleEspalda = c.Int(nullable: false),
                        TalleDelantero = c.Int(nullable: false),
                        AlturaAxila = c.Int(nullable: false),
                        AlturaRodilla = c.Int(nullable: false),
                        AlturaCCadera = c.Int(nullable: false),
                        LargoCinturaSuelo = c.Int(nullable: false),
                        TiroPaantalonDelantero = c.Int(nullable: false),
                        LargoManga = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TallePrendas", "TalleId", "dbo.Talles");
            DropForeignKey("dbo.TallePrendas", "PrendaId", "dbo.Prendas");
            DropIndex("dbo.TallePrendas", new[] { "PrendaId" });
            DropIndex("dbo.TallePrendas", new[] { "TalleId" });
            DropTable("dbo.Talles");
            DropTable("dbo.TallePrendas");
        }
    }
}
