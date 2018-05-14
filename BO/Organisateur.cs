using System.Collections.Generic;

namespace BO
{

    public class Organisateur : Personne
    {
        public virtual List<Evenement> Evenements { get; set; }
    }
}