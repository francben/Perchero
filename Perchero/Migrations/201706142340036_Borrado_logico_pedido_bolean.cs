namespace Perchero.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Borrado_logico_pedido_bolean : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedidoes", "Eliminado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pedidoes", "Eliminado");
        }
    }
}
