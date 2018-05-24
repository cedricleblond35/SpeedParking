using BO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace DAL
{
    public class ConviveRepository<T> : GenericRepository<Convive> where T : DbContext, IDbContext
    {
        public ConviveRepository(T context) : base(context)
        {

        }
        public override Convive GetById(int id)
        {
            return set.Include(c1 => c1.EvenementsInscris).SingleOrDefault(c2 => c2.Id == id);
        }
        
        public Convive GetByUserId(string id)
        {
            return set.Include(c1 => c1.EvenementsInscris).SingleOrDefault(c2 => c2.IdUser == id);
        }

        public override void Update(Convive convive)
        {
            Convive c = set.Include(c1 => c1.EvenementsInscris).SingleOrDefault(c2 => c2.Id == convive.Id);
            c.Id = convive.Id;
            c.Nom = convive.Nom;
            c.Prenom = convive.Prenom;
            c.Email = convive.Email;
            c.DateNaissance = convive.DateNaissance;
            c.Adresse = convive.Adresse;
            c.Ville = convive.Ville;
            c.CodePostal = convive.CodePostal;
            c.EvenementsInscris.Clear();
            foreach (var e in convive.EvenementsInscris)
            {
                c.EvenementsInscris.Add(e);
            }
            dbContext.SaveChanges();
        }
    }
}
