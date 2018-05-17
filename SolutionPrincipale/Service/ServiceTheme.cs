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
        private static ApplicationDbContext db = new ApplicationDbContext();
        private static GenericRepository<Theme> rep = new GenericRepository<Theme>(db);

        public static List<Theme> GetListeThemes()
        {
            return rep.GetAll();
        }
    }
}