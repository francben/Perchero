namespace Perchero.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PedidoFechas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pedidoes", "FechaPedido", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Pedidoes", "FechaEntrega", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pedidoes", "FechaEntrega", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Pedidoes", "FechaPedido", c => c.DateTime(nullable: false));
        }
    }
}
