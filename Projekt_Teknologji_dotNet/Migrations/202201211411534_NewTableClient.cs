namespace Projekt_Teknologji_dotNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTableClient : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Reservation", "KlientID", c => c.Int());
            CreateIndex("dbo.Reservation", "KlientID");
            AddForeignKey("dbo.Reservation", "KlientID", "dbo.Client", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservation", "KlientID", "dbo.Client");
            DropIndex("dbo.Reservation", new[] { "KlientID" });
            DropColumn("dbo.Reservation", "KlientID");
            DropTable("dbo.Client");
        }
    }
}
