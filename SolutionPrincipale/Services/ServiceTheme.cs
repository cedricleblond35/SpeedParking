using BO;
using DAL;
using SolutionPrincipale.Models;
using System;
using System.Collections.Generic;

namespace SolutionPrincipale.Service
{
    public class ServiceTheme
    {

        public static List<Theme> GetListeThemes()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                GenericRepository<Theme> rep = new GenericRepository<Theme>(db);
                return rep.GetAll();
            }
        }

        public static Theme GetOneTheme(int? id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                GenericRepository<Theme> rep = new GenericRepository<Theme>(db);
                if (!id.HasValue)
                {
                    return null;
                }
                else {
                    int idnn = (int)id;
                    return rep.GetById(idnn);
                }
            }
        }

        public static Theme GetOneTheme(int? id, ApplicationDbContext db)
        {
            GenericRepository<Theme> rep = new GenericRepository<Theme>(db);
            if (!id.HasValue)
            {
                return null;
            }
            else {
                int idnn = (int)id;
                return rep.GetById(idnn);
            }
        }

        public static void RemoveTheme(Theme t)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                GenericRepository<Theme> rep = new GenericRepository<Theme>(db);
                rep.Delete(t.Id);
            }
        }
    }
}