using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Evenement : IIdentifiable
    {
        public int Id { get; set; }
        public int NbParticipants { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public DateTime DebutEvenement { get; set; }
        public DateTime FinEvenement { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public string CodePostal { get; set; }
        public virtual List<Convive> Convives { get; set; }
        public virtual Organisateur Organisateur { get; set; }
        public Evenement()
        {
            Convives = new List<Convive>();
        }

        public Evenement(int id, int nbParticipants, string nom, string description, DateTime debutEvenement,
            DateTime finEvenement, string adresse, string ville, string codePostal)
        {
            this.Id = id;
            this.NbParticipants = nbParticipants;
            this.Nom = nom;
            this.Description = description;
            this.DebutEvenement = debutEvenement;
            this.FinEvenement = finEvenement;
            this.Adresse = adresse;
            this.Ville = ville;
            this.CodePostal = codePostal;
        }
    }
}
