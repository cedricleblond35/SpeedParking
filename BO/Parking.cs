using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Parking
    {
        public string Id { get; set; }
        public string Nom { get; set; }
        public string Statut { get; set; }
        public int NbPlaces { get; set; }
        public int NbPlacesLibres { get; set; }
        public string GeometrieType { get; set; }
        public List<double> Coordonnees { get; set; }
        public string CrsType { get; set; }
        public string CrsNom { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public string CodePostal { get; set; }
        public string Horaires { get; set; }
        public string Tarifs { get; set; }

        public Parking(string id, string nom, string statut, int nbPlaces, int nbPlacesLibres)
        {
            this.Id = id;
            this.Nom = nom;
            this.Statut = statut;
            this.NbPlaces = nbPlaces;
            this.NbPlacesLibres = nbPlacesLibres;
        }
    }
}
