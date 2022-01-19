namespace Projekt_Teknologji_dotNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rezervuar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Car", "ERezervuar", c => c.Boolean(nullable: false));
            DropColumn("dbo.Reservation", "ERezervuar");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservation", "ERezervuar", c => c.Boolean(nullable: false));
            DropColumn("dbo.Car", "ERezervuar");
        }
    }
}
