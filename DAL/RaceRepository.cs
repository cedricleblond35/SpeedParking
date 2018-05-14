using BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DAL
{
    class RaceRepository : IRepository<Race>
    {
        private Context dbContext;


        public RaceRepository(Context context)
        {
            dbContext = context;
        }
        public void Delete(int id)
        {
            try
            {
                var race = dbContext.Races.Include(r => r.Organizer).Include(r => r.Competitors).FirstOrDefault(r => r.Id == id);
                race.Competitors.Clear();
                race.Organizer = null;
                dbContext.Races.Remove(race);
                dbContext.SaveChanges();
            } catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            } 
            
        }

        public List<Race> GetAll()
        {
            return dbContext.Races.ToList();
        }

        public Race GetById(int id)
        {
            return dbContext.Races.Find(id);
        }

        public void Insert(Race race)
        {
            dbContext.Races.Add(race);
            dbContext.SaveChanges();
        }

        public void Update(Race race)
        {
            Race r = GetById(race.Id);
            r.Id = race.Id;
            r.Title = race.Title;
            r.Description = race.Description;
            r.DateStart = race.DateStart;
            r.DateEnd = race.DateEnd;
            r.Organizer = race.Organizer;
            // TODO : race competitors
            dbContext.SaveChanges();
        }
    }
}
