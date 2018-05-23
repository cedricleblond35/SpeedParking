using BO;
using System.Collections.Generic;

namespace SolutionPrincipale.Models
{
    public class ListeEvenementsVM
    {
        public List<Evenement> ListeEvenements { get; set; } = new List<Evenement>();
        public List<Evenement> ListeEvenementsInscris { get; set; } = new List<Evenement>();
    }
}