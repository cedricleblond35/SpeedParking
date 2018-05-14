using BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class ConviveRepository : IRepository<Convive>
    {
        private Context dbContext;

        public ConviveRepository(Context context)
        {
            dbContext = context;
        }
        public void Delete(int id)
        {
            dbContext.Convives.Remove(GetById(id));
            dbContext.SaveChanges();
        }

        public List<Convive> GetAll()
        {
            return dbContext.Convives.ToList();
        }

        public Convive GetById(int id)
        {
            return dbContext.Convives.Find(id);
        }

        public void Insert(Convive convive)
        {
            dbContext.Convives.Add(convive);
            dbContext.SaveChanges();
        }

        public void Update(Convive convive)
        {
            Convive c = GetById(convive.Id);
            c.Id = convive.Id;
            c.Nom = convive.Nom;
            c.Prenom = convive.Prenom;
            c.Email = convive.Email;
            c.DateNaissance = convive.DateNaissance;
            c.Evenement = convive.Evenement;
            dbContext.SaveChanges();
        }
    }
}
