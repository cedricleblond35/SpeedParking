using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public interface IAdresse
    {
        string Adresse { get; set; }
        string CodePostal { get; set; }
        string Ville { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
    }
}
