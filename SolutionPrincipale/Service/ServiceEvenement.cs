using BO;
using DAL;
using SolutionPrincipale.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace SolutionPrincipale.Service
{
    public class ServiceEvenement
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private static GenericRepository<Evenement> rep = new GenericRepository<Evenement>(db);

        public static List<Evenement> GetListeEvenements()
        {
            return rep.GetAll();
        }

        public static Evenement GetOneEvenement(int? id)
        {
            if(id.HasValue){
                return null;
            }else{
                int idnn = (int)id;
                return rep.GetById(idnn);
            }
        }

        public static void AddEvenement(Evenement e)
        {
            db.Evenements.Add(e);
        }
        public static void RemoveEvenement(Evenement e)
        {
            db.Evenements.Remove(e);
        }
        public static void EntryEvenement(Evenement e)
        {
            db.Entry(e).State = EntityState.Modified;
        }
    }
}