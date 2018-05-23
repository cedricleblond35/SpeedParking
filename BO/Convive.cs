using System.Collections.Generic;

namespace BO
{
    public class Convive : Personne
    {
        public virtual List<Evenement> EvenementsInscris { get; set; } = new List<Evenement>();

        public Convive() : base()
        {

        }
        public Convive(string email, string idUser) : base(email, idUser)
        {

        }
    }
}