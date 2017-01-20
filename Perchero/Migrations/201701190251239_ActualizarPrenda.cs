namespace Perchero.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizarPrenda : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Avios", "DetallePrenda_Id", "dbo.DetallePrendas");
            DropForeignKey("dbo.Telas", "DetallePrenda_Id", "dbo.DetallePrendas");
            DropIndex("dbo.Avios", new[] { "DetallePrenda_Id" });
            DropIndex("dbo.Telas", new[] { "DetallePrenda_Id" });
            AddColumn("dbo.DetallePrendas", "AvioId", c => c.Int(nullable: false));
            AddColumn("dbo.DetallePrendas", "TelaId", c => c.Int(nullable: false));
            CreateIndex("dbo.DetallePrendas", "AvioId");
            CreateIndex("dbo.DetallePrendas", "TelaId");
            AddForeignKey("dbo.DetallePrendas", "AvioId", "dbo.Avios", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DetallePrendas", "TelaId", "dbo.Telas", "Id", cascadeDelete: true);
            DropColumn("dbo.Avios", "DetallePrenda_Id");
            DropColumn("dbo.Telas", "DetallePrenda_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Telas", "DetallePrenda_Id", c => c.Int());
            AddColumn("dbo.Avios", "DetallePrenda_Id", c => c.Int());
            DropForeignKey("dbo.DetallePrendas", "TelaId", "dbo.Telas");
            DropForeignKey("dbo.DetallePrendas", "AvioId", "dbo.Avios");
            DropIndex("dbo.DetallePrendas", new[] { "TelaId" });
            DropIndex("dbo.DetallePrendas", new[] { "AvioId" });
            DropColumn("dbo.DetallePrendas", "TelaId");
            DropColumn("dbo.DetallePrendas", "AvioId");
            CreateIndex("dbo.Telas", "DetallePrenda_Id");
            CreateIndex("dbo.Avios", "DetallePrenda_Id");
            AddForeignKey("dbo.Telas", "DetallePrenda_Id", "dbo.DetallePrendas", "Id");
            AddForeignKey("dbo.Avios", "DetallePrenda_Id", "dbo.DetallePrendas", "Id");
        }
    }
}
