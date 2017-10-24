namespace Perchero.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aviosNoReq : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DetallePrendas", "AvioId", "dbo.Avios");
            DropForeignKey("dbo.DetallePrendas", "TelaId", "dbo.Telas");
            DropIndex("dbo.DetallePrendas", new[] { "AvioId" });
            DropIndex("dbo.DetallePrendas", new[] { "TelaId" });
            AlterColumn("dbo.DetallePrendas", "AvioId", c => c.Int());
            AlterColumn("dbo.DetallePrendas", "TelaId", c => c.Int());
            CreateIndex("dbo.DetallePrendas", "AvioId");
            CreateIndex("dbo.DetallePrendas", "TelaId");
            AddForeignKey("dbo.DetallePrendas", "AvioId", "dbo.Avios", "Id");
            AddForeignKey("dbo.DetallePrendas", "TelaId", "dbo.Telas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetallePrendas", "TelaId", "dbo.Telas");
            DropForeignKey("dbo.DetallePrendas", "AvioId", "dbo.Avios");
            DropIndex("dbo.DetallePrendas", new[] { "TelaId" });
            DropIndex("dbo.DetallePrendas", new[] { "AvioId" });
            AlterColumn("dbo.DetallePrendas", "TelaId", c => c.Int(nullable: false));
            AlterColumn("dbo.DetallePrendas", "AvioId", c => c.Int(nullable: false));
            CreateIndex("dbo.DetallePrendas", "TelaId");
            CreateIndex("dbo.DetallePrendas", "AvioId");
            AddForeignKey("dbo.DetallePrendas", "TelaId", "dbo.Telas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DetallePrendas", "AvioId", "dbo.Avios", "Id", cascadeDelete: true);
        }
    }
}
