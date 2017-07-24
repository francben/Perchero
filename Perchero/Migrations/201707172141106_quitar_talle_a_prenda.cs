namespace Perchero.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quitar_talle_a_prenda : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Prendas", "Talle");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prendas", "Talle", c => c.Int(nullable: false));
        }
    }
}
