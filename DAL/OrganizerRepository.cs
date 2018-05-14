using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Migrations
{
    class OrganizerRepository : IRepository<Organizer>
    {
        private Context dbContext;

        public OrganizerRepository(Context context)
        {
            dbContext = context;
        }
        public void Delete(int id)
        {
            dbContext.Organizers.Remove(GetById(id));
            dbContext.SaveChanges();
        }

        public List<Organizer> GetAll()
        {
            return dbContext.Organizers.ToList();
        }

        public Organizer GetById(int id)
        {
            return dbContext.Organizers.Find(id);
        }

        public void Insert(Organizer organizer)
        {
            dbContext.Organizers.Add(organizer);
            dbContext.SaveChanges();
        }

        public void Update(Organizer organizer)
        {
            Organizer o = GetById(organizer.Id);
            o.Id = organizer.Id;
            o.Nom = organizer.Nom;
            o.Prenom = organizer.Prenom;
            o.Email = organizer.Email;
            o.DateNaissance = organizer.DateNaissance;
           // o.Races = organizer.Races;
            dbContext.SaveChanges();
        }

    }
}
