using BO;
using System.Collections.Generic;

namespace SolutionPrincipale.Models
{
    public class CreateEditEvenementVM
    {
        public Evenement Evenement { get; set; }
        public virtual List<Theme> Themes { get; set; }
        public virtual List<Image> Images { get; set; }
        public List<int> IdSelectedThemes { get; set; }
        public List<int> IdSelectedImages { get; set; }
    }
}