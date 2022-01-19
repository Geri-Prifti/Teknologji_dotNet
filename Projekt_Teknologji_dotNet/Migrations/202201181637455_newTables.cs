namespace Projekt_Teknologji_dotNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Car",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Modeli = c.String(nullable: false),
                        Pershkrimi = c.String(),
                        Vit_Prodhimi = c.Int(nullable: false),
                        Kosto1Dite = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IMG1 = c.String(nullable: false),
                        IMG2 = c.String(),
                        IMG3 = c.String(),
                        TipiID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Type", t => t.TipiID)
                .Index(t => t.TipiID);
            
            CreateTable(
                "dbo.Type",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Emri = c.String(nullable: false),
                        Ikona = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date_Rezervimi = c.DateTime(nullable: false),
                        Date_kthimi = c.DateTime(nullable: false),
                        ERezervuar = c.Boolean(nullable: false),
                        Pagesa_totale = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Car", "TipiID", "dbo.Type");
            DropIndex("dbo.Car", new[] { "TipiID" });
            DropTable("dbo.Reservation");
            DropTable("dbo.Type");
            DropTable("dbo.Car");
        }
    }
}
