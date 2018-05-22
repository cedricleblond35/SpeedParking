using BO;
using DAL;
using SolutionPrincipale.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace SolutionPrincipale.Service
{
    public class ServiceEvenement
    {
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Evenement> GetListeEvenements()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                GenericRepository<Evenement> rep = new GenericRepository<Evenement>(db);
                return rep.GetAll();
            }
        }

        /// <summary>
        /// 
        /// Détail d'un évement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Evenement GetOneEvenement(int? id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                GenericRepository<Evenement> rep = new GenericRepository<Evenement>(db);
                if (!id.HasValue)
                {
                    return null;
                }
                else
                {
                    int idnn = (int)id;
                    return rep.GetById(idnn);
                }
            }
        }

        public static void AddEvenement(Evenement e)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                GenericRepository<Evenement> rep = new GenericRepository<Evenement>(db);
                if (!String.IsNullOrWhiteSpace(e.Adresse))
                {
                    ServiceCartographie.geocoder(e);
                }
                db.Evenements.Add(e);
                db.SaveChanges();
            }
        }
        public static void RemoveEvenement(Evenement e)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                GenericRepository<Evenement> rep = new GenericRepository<Evenement>(db);
                db.Evenements.Remove(e);
                db.SaveChanges();
            }
        }
        public static void EntryEvenement(Evenement e)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                GenericRepository<Evenement> rep = new GenericRepository<Evenement>(db);
                db.Entry(e).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}