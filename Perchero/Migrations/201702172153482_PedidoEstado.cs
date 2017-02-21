namespace Perchero.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PedidoEstado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedidoes", "Estado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pedidoes", "Estado");
        }
    }
}
