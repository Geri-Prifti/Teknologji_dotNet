namespace Projekt_Teknologji_dotNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhoneNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Client", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Client", "PhoneNumber");
        }
    }
}
