using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BO
{
    public class Evenement : IIdentifiable, IAdresse
    {
        public int Id { get; set; }
        [DisplayName("Nombre de participants")]
        public int NbParticipants { get; set; }
        [DisplayName("Nom")]
        public string Nom { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Date/Heure début événement")]
        public DateTime DebutEvenement { get; set; }
        [DisplayName("Durée événement (en min)")]
        public int DureeMinutes { get; set; }
        [DisplayName("Liste des thèmes")]
        public virtual List<Theme> Themes { get; set; }
        [DisplayName("Liste des images")]
        public virtual List<Image> Images { get; set; }
        [DisplayName("Adresse")]
        public string Adresse { get; set; }
        [DisplayName("Ville")]
        public string Ville { get; set; }
        [DisplayName("Code postal")]
        public string CodePostal { get; set; }
        [DisplayName("Liste des participants")]
        public virtual List<Convive> Convives { get; set; }
        [DisplayName("Organisateur")]
        public virtual Organisateur Organisateur { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Evenement()
        {
            Convives = new List<Convive>();
            Themes = new List<Theme>();
            Images = new List<Image>();
        }

        public Evenement(int id, int nbParticipants, string nom, string description, DateTime debutEvenement,
            int dureeMinutes, List<Theme> themes, List<Image> images, string adresse, string ville, string codePostal)
        {
            Id = id;
            NbParticipants = nbParticipants;
            Nom = nom;
            Description = description;
            DebutEvenement = debutEvenement;
            DureeMinutes = dureeMinutes;
            Themes = themes;
            Images = images;
            Adresse = adresse;
            Ville = ville;
            CodePostal = codePostal;
        }
    }
}
