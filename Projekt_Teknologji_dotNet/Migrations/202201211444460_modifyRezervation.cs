namespace Projekt_Teknologji_dotNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyRezervation : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Reservation", name: "Makinat_ID", newName: "MakinatID");
            RenameIndex(table: "dbo.Reservation", name: "IX_Makinat_ID", newName: "IX_MakinatID");
            DropColumn("dbo.Reservation", "MakineID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservation", "MakineID", c => c.Int());
            RenameIndex(table: "dbo.Reservation", name: "IX_MakinatID", newName: "IX_Makinat_ID");
            RenameColumn(table: "dbo.Reservation", name: "MakinatID", newName: "Makinat_ID");
        }
    }
}
