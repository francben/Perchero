namespace Perchero.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovimientoCaja : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovimientoCajas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        CajaId = c.Int(nullable: false),
                        Concepto = c.String(nullable: false),
                        Movimiento = c.String(nullable: false),
                        Ingreso = c.Single(nullable: false),
                        Egreso = c.Single(nullable: false),
                        Saldo = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cajas", t => t.CajaId, cascadeDelete: true)
                .Index(t => t.CajaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovimientoCajas", "CajaId", "dbo.Cajas");
            DropIndex("dbo.MovimientoCajas", new[] { "CajaId" });
            DropTable("dbo.MovimientoCajas");
        }
    }
}
