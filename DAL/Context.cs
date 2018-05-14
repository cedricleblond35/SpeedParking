using BO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Context : DbContext 
    {
        
        public DbSet<Race> Races { get; set; }

        public DbSet<Participant> Competitors { get; set;}

        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Parking> Parkings { get; set; }
    }
}
