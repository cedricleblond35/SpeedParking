using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSport.Models
{
    public class CreateEditCompetitorVM
    {
        public Convive Competitor { get; set; }

        [Display(Name = "La course")]
        public IEnumerable<Race> Races { get; set; }
        public int? IdSelectedRace { get; set; }
    }
}