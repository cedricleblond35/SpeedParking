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
        
        public DbSet<Evenement> Evenements { get; set; }

        public DbSet<Convive> Convives { get; set;}

        public DbSet<Organisateur> Organisateurs { get; set; }
        public DbSet<Parking> Parkings { get; set; }
    }
}
