namespace SolutionPrincipale.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Convives", "Latitude", c => c.Double());
            AlterColumn("dbo.Convives", "Longitude", c => c.Double());
            AlterColumn("dbo.Evenements", "Latitude", c => c.Double());
            AlterColumn("dbo.Evenements", "Longitude", c => c.Double());
            AlterColumn("dbo.Organisateurs", "Latitude", c => c.Double());
            AlterColumn("dbo.Organisateurs", "Longitude", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Organisateurs", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Organisateurs", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Evenements", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Evenements", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Convives", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Convives", "Latitude", c => c.Double(nullable: false));
        }
    }
}
