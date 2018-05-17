namespace SolutionPrincipale.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Description = c.String(),
                        Url = c.String(),
                        Evenement_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Evenements", t => t.Evenement_Id)
                .Index(t => t.Evenement_Id);
            
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Description = c.String(),
                        Evenement_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Evenements", t => t.Evenement_Id)
                .Index(t => t.Evenement_Id);
            
            AddColumn("dbo.Evenements", "DureeMinutes", c => c.Int(nullable: false));
            DropColumn("dbo.Evenements", "FinEvenement");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Evenements", "FinEvenement", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Themes", "Evenement_Id", "dbo.Evenements");
            DropForeignKey("dbo.Images", "Evenement_Id", "dbo.Evenements");
            DropIndex("dbo.Themes", new[] { "Evenement_Id" });
            DropIndex("dbo.Images", new[] { "Evenement_Id" });
            DropColumn("dbo.Evenements", "DureeMinutes");
            DropTable("dbo.Themes");
            DropTable("dbo.Images");
        }
    }
}
