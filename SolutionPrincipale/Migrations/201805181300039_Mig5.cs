namespace SolutionPrincipale.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Convives", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Convives", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Evenements", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Evenements", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Organisateurs", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Organisateurs", "Longitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Organisateurs", "Longitude", c => c.String());
            AlterColumn("dbo.Organisateurs", "Latitude", c => c.String());
            AlterColumn("dbo.Evenements", "Longitude", c => c.String());
            AlterColumn("dbo.Evenements", "Latitude", c => c.String());
            AlterColumn("dbo.Convives", "Longitude", c => c.String());
            AlterColumn("dbo.Convives", "Latitude", c => c.String());
        }
    }
}
