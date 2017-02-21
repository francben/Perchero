namespace Perchero.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizarTipo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tipoes", "Categoria", c => c.String(nullable: false));
            DropColumn("dbo.Tipoes", "Nombre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tipoes", "Nombre", c => c.String(nullable: false));
            DropColumn("dbo.Tipoes", "Categoria");
        }
    }
}
