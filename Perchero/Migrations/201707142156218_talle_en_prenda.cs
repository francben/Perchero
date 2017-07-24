namespace Perchero.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class talle_en_prenda : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prendas", "Talle", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prendas", "Talle");
        }
    }
}
