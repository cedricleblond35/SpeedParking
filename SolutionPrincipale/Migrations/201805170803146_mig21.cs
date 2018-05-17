namespace SolutionPrincipale.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Convives", "IdUser", c => c.String());
            AddColumn("dbo.Organisateurs", "IdUser", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organisateurs", "IdUser");
            DropColumn("dbo.Convives", "IdUser");
        }
    }
}
