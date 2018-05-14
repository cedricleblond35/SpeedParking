using System.Collections.Generic;

namespace BO
{

    public class Organizer : Personne
    {
        public virtual List<Evenement> Evenements { get; set; }
    }
}