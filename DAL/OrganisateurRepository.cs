using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Migrations
{
    class OrganisateurRepository : IRepository<Organisateur>
    {
        private Context dbContext;

        public OrganisateurRepository(Context context)
        {
            dbContext = context;
        }
        public void Delete(int id)
        {
            dbContext.Organisateurs.Remove(GetById(id));
            dbContext.SaveChanges();
        }

        public List<Organisateur> GetAll()
        {
            return dbContext.Organisateurs.ToList();
        }

        public Organisateur GetById(int id)
        {
            return dbContext.Organisateurs.Find(id);
        }

        public void Insert(Organisateur organizer)
        {
            dbContext.Organisateurs.Add(organisateur);
            dbContext.SaveChanges();
        }

        public void Update(Organisateur organisateur)
        {
            Organisateur o = GetById(organisateur.Id);
            o.Id = organisateur.Id;
            o.Nom = organisateur.Nom;
            o.Prenom = organisateur.Prenom;
            o.Email = organisateur.Email;
            o.DateNaissance = organisateur.DateNaissance;
            dbContext.SaveChanges();
        }

    }
}
