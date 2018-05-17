namespace SolutionPrincipale.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig4LongLat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Convives", "Latitude", c => c.String());
            AddColumn("dbo.Convives", "Longitude", c => c.String());
            AddColumn("dbo.Evenements", "Latitude", c => c.String());
            AddColumn("dbo.Evenements", "Longitude", c => c.String());
            AddColumn("dbo.Organisateurs", "Latitude", c => c.String());
            AddColumn("dbo.Organisateurs", "Longitude", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organisateurs", "Longitude");
            DropColumn("dbo.Organisateurs", "Latitude");
            DropColumn("dbo.Evenements", "Longitude");
            DropColumn("dbo.Evenements", "Latitude");
            DropColumn("dbo.Convives", "Longitude");
            DropColumn("dbo.Convives", "Latitude");
        }
    }
}
