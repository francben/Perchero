namespace Perchero.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class varaible_punho_jeje_talleprenda : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TallePrendas", "PunhoAjustado", c => c.Int(nullable: false));
            AddColumn("dbo.TallePrendas", "PunhoFlojo", c => c.Int(nullable: false));
            DropColumn("dbo.TallePrendas", "PuñoAjustado");
            DropColumn("dbo.TallePrendas", "PuñoFlojo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TallePrendas", "PuñoFlojo", c => c.Int(nullable: false));
            AddColumn("dbo.TallePrendas", "PuñoAjustado", c => c.Int(nullable: false));
            DropColumn("dbo.TallePrendas", "PunhoFlojo");
            DropColumn("dbo.TallePrendas", "PunhoAjustado");
        }
    }
}
