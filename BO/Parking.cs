using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Parking : IIdentifiable
    {
        public int Id { get; set; }
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
        public DateTime? HeureOuverture { get; set; }
        public DateTime? HeureFermeture { get; set; }
        public int PrixHoraire { get; set; }

    }
}
