namespace Perchero.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrecionPedido : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pedidoes", "FechaPedido", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Pedidoes", "FechaEntrega", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Pedidoes", "Seña", c => c.Double(nullable: false));
            AlterColumn("dbo.Pedidoes", "Saldo", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pedidoes", "Saldo", c => c.String(nullable: false));
            AlterColumn("dbo.Pedidoes", "Seña", c => c.String(nullable: false));
            AlterColumn("dbo.Pedidoes", "FechaEntrega", c => c.String(nullable: false));
            AlterColumn("dbo.Pedidoes", "FechaPedido", c => c.String(nullable: false));
        }
    }
}
