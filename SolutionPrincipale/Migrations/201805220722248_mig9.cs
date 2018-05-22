namespace SolutionPrincipale.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Parkings",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nom = c.String(),
                        Statut = c.String(),
                        NbPlaces = c.Int(nullable: false),
                        NbPlacesLibres = c.Int(nullable: false),
                        GeometrieType = c.String(),
                        CrsType = c.String(),
                        CrsNom = c.String(),
                        Adresse = c.String(),
                        Ville = c.String(),
                        CodePostal = c.String(),
                        Horaires = c.String(),
                        Tarifs = c.String(),
                        Latitude = c.Double(),
                        Longitude = c.Double(),
                        Evenement_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Evenements", t => t.Evenement_Id)
                .Index(t => t.Evenement_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Parkings", "Evenement_Id", "dbo.Evenements");
            DropIndex("dbo.Parkings", new[] { "Evenement_Id" });
            DropTable("dbo.Parkings");
        }
    }
}
