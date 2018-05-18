using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Theme : IIdentifiable
    {
        public int Id { get; set; }
        [DisplayName("Nom thème")]
        public string Nom { get; set; }
        [DisplayName("Description thème")]
        public string Description { get; set; }
        public virtual List<Evenement> Evenements { get; set; }
    }
}
