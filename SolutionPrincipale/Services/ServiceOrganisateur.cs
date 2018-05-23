using BO;
using DAL;
using Microsoft.AspNet.Identity;
using SolutionPrincipale.Models;
using System;
using System.Linq;

namespace SolutionPrincipale.Service
{
    public class ServiceOrganisateur
    {
        /// <summary>
        /// Récupération d'un organisateur par son userId
        /// </summary>
        /// <param name="id">UserId de l'organisateur à récupérer</param>
        /// <returns>Organisateur</returns>
        public static Organisateur GetOneOrganisateur(string id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                OrganisateurRepository<ApplicationDbContext> rep = new OrganisateurRepository<ApplicationDbContext>(db);
                if (String.IsNullOrWhiteSpace(id))
                {
                    return null;
                }
                else
                {
                    return db.Organisateurs.FirstOrDefault(p => p.IdUser == id);
                }
            }
        }
    }
}