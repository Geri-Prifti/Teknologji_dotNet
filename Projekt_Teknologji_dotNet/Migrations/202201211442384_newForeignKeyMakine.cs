namespace Projekt_Teknologji_dotNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newForeignKeyMakine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservation", "MakineID", c => c.Int());
            AddColumn("dbo.Reservation", "Makinat_ID", c => c.Int());
            CreateIndex("dbo.Reservation", "Makinat_ID");
            AddForeignKey("dbo.Reservation", "Makinat_ID", "dbo.Car", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservation", "Makinat_ID", "dbo.Car");
            DropIndex("dbo.Reservation", new[] { "Makinat_ID" });
            DropColumn("dbo.Reservation", "Makinat_ID");
            DropColumn("dbo.Reservation", "MakineID");
        }
    }
}
