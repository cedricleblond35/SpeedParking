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
        public string Nom { get; set; }
        public string Description { get; set; }
        public DateTime DebutEvenement { get; set; }
        public DateTime FinEvenement { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public string CodePostal { get; set; }
        public virtual List<Participant> Participants { get; set; }
        public virtual Organizer Organizer { get; set; }
        public Evenement()
        {
            Participants = new List<Participant>();
        }
    }
}
