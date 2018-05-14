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
    class CompetitorRepository : IRepository<Participant>
    {
        private Context dbContext;

        public CompetitorRepository(Context context)
        {
            dbContext = context;
        }
        public void Delete(int id)
        {
            dbContext.Competitors.Remove(GetById(id));
            dbContext.SaveChanges();
        }

        public List<Participant> GetAll()
        {
            return dbContext.Competitors.ToList();
        }

        public Participant GetById(int id)
        {
            return dbContext.Competitors.Find(id);
        }

        public void Insert(Participant competitor)
        {
            dbContext.Competitors.Add(competitor);
            dbContext.SaveChanges();
        }

        public void Update(Participant competitor)
        {
            Participant c = GetById(competitor.Id);
            c.Id = competitor.Id;
            c.Nom = competitor.Nom;
            c.Prenom = competitor.Prenom;
            c.Email = competitor.Email;
            c.DateNaissance = competitor.DateNaissance;
            c.Race = competitor.Race;
            dbContext.SaveChanges();
        }
    }
}
