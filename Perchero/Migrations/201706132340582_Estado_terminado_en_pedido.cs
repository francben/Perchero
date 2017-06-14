namespace Perchero.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Estado_terminado_en_pedido : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedidoes", "Terminado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pedidoes", "Terminado");
        }
    }
}
