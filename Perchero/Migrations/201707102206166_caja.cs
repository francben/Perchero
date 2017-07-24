namespace Perchero.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class caja : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cajas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha_Apertura = c.DateTime(nullable: false),
                        Inicial = c.Single(nullable: false),
                        Fecha_Cierre = c.DateTime(),
                        Entrada = c.Single(),
                        Salida = c.Single(),
                        Cierre = c.Single(),
                        Operaciones = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cajas", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Cajas", new[] { "UserId" });
            DropTable("dbo.Cajas");
        }
    }
}
