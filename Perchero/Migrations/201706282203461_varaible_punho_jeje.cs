namespace Perchero.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class varaible_punho_jeje : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Talles", "PunhoAjustado", c => c.Int(nullable: false));
            AddColumn("dbo.Talles", "PunhoFlojo", c => c.Int(nullable: false));
            DropColumn("dbo.Talles", "PuñoAjustado");
            DropColumn("dbo.Talles", "PuñoFlojo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Talles", "PuñoFlojo", c => c.Int(nullable: false));
            AddColumn("dbo.Talles", "PuñoAjustado", c => c.Int(nullable: false));
            DropColumn("dbo.Talles", "PunhoFlojo");
            DropColumn("dbo.Talles", "PunhoAjustado");
        }
    }
}
