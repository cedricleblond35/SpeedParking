using System.Collections.Generic;

namespace BO
{
    public class Convive : Personne
    {
        public List<Evenement> EvenementsInscris { get; set; }

        public Convive() : base()
        {

        }
        public Convive(string email, string idUser) : base(email, idUser)
        {

        }
    }
}