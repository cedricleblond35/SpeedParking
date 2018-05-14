using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSport.Models
{
    public class CreateEditRaceVM
    {
        public Race Race { get; set; }

        public IEnumerable<Organizer> Organizers { get; set; }

        public int? IdSelectedOrganizer { get; set; }
    }
}