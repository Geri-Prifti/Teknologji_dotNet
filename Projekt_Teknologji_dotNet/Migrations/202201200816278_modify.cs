namespace Projekt_Teknologji_dotNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Car", "IMG", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Car", "IMG", c => c.String(nullable: false));
        }
    }
}
