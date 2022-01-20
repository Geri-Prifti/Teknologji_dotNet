namespace Projekt_Teknologji_dotNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Car", "IMG", c => c.String(nullable: false));
            DropColumn("dbo.Car", "IMG1");
            DropColumn("dbo.Car", "IMG2");
            DropColumn("dbo.Car", "IMG3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Car", "IMG3", c => c.String());
            AddColumn("dbo.Car", "IMG2", c => c.String());
            AddColumn("dbo.Car", "IMG1", c => c.String(nullable: false));
            DropColumn("dbo.Car", "IMG");
        }
    }
}
