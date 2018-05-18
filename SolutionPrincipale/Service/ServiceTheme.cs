using BO;
using DAL;
using SolutionPrincipale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                if (id.HasValue)
                {
                    return null;
                }
                else {
                    int idnn = (int)id;
                    return rep.GetById(idnn);
                }
            }
        }
    }
}