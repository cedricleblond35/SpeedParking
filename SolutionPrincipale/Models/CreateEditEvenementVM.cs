﻿using BO;
using System.Collections.Generic;

namespace SolutionPrincipale.Models
{
    public class CreateEditEvenementVM
    {
        public Evenement Evenement { get; set; }
        public Organisateur Organisateur { get; set; }
        public IEnumerable<Theme> Themes { get; set; }
        public IEnumerable<int> IdSelectedThemes { get; set; }
        public IEnumerable<int> IdSelectedImages { get; set; }

        public CreateEditEvenementVM()
        {
            Evenement = new Evenement();
        }
    }
}