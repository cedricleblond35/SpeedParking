namespace SolutionPrincipale.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Themes", "Evenement_Id", "dbo.Evenements");
            DropIndex("dbo.Themes", new[] { "Evenement_Id" });
            CreateTable(
                "dbo.ThemeEvenements",
                c => new
                    {
                        Theme_Id = c.Int(nullable: false),
                        Evenement_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Theme_Id, t.Evenement_Id })
                .ForeignKey("dbo.Themes", t => t.Theme_Id, cascadeDelete: true)
                .ForeignKey("dbo.Evenements", t => t.Evenement_Id, cascadeDelete: true)
                .Index(t => t.Theme_Id)
                .Index(t => t.Evenement_Id);
            
            AlterColumn("dbo.Convives", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Convives", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Evenements", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Evenements", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Organisateurs", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Organisateurs", "Longitude", c => c.Double(nullable: false));
            DropColumn("dbo.Themes", "Evenement_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Themes", "Evenement_Id", c => c.Int());
            DropForeignKey("dbo.ThemeEvenements", "Evenement_Id", "dbo.Evenements");
            DropForeignKey("dbo.ThemeEvenements", "Theme_Id", "dbo.Themes");
            DropIndex("dbo.ThemeEvenements", new[] { "Evenement_Id" });
            DropIndex("dbo.ThemeEvenements", new[] { "Theme_Id" });
            AlterColumn("dbo.Organisateurs", "Longitude", c => c.String());
            AlterColumn("dbo.Organisateurs", "Latitude", c => c.String());
            AlterColumn("dbo.Evenements", "Longitude", c => c.String());
            AlterColumn("dbo.Evenements", "Latitude", c => c.String());
            AlterColumn("dbo.Convives", "Longitude", c => c.String());
            AlterColumn("dbo.Convives", "Latitude", c => c.String());
            DropTable("dbo.ThemeEvenements");
            CreateIndex("dbo.Themes", "Evenement_Id");
            AddForeignKey("dbo.Themes", "Evenement_Id", "dbo.Evenements", "Id");
        }
    }
}
