using System.Collections.Generic;

namespace BO
{
    public class Convive : Personne
    {
        public List<Evenement> Evenements { get; set; }

        public Convive(string email, string idUser) : base(email, idUser)
        {

        }
    }
}