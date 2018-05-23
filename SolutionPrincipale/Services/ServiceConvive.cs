using BO;
using DAL;
using SolutionPrincipale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolutionPrincipale.Services
{
    public class ServiceConvive
    {
        /// <summary>
        /// Récupération d'un convive
        /// </summary>
        /// <param name="id">Id du convive à récupérer</param>
        /// <returns>Convive</returns>
        public static Convive GetOneConvive(int? id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                GenericRepository<Convive> rep = new GenericRepository<Convive>(db);
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

        /// <summary>
        /// Récupération d'un convive par son userId
        /// </summary>
        /// <param name="id">UserId du convive à récupérer</param>
        /// <returns>Convive</returns>
        public static Convive GetOneConvive(string id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (String.IsNullOrWhiteSpace(id))
                {
                    return null;
                }
                else
                {
                    return db.Convives.FirstOrDefault(p => p.IdUser == id);
                }
            }
        }

        /// <summary>
        /// Récupèration de la liste des événements d'un convive pour lesquels celui-ci s'est inscrit
        /// </summary>
        /// <param name="id">Id du convive dont on souhaite récupérer la liste des événements inscris</param>
        /// <returns>List<Evenement></returns>
        public static List<Evenement> GetListeEvenementsInscris(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Convives.FirstOrDefault(p => p.Id == id).EvenementsInscris;
            }
        }

        /// <summary>
        /// Récupèration de la liste des événements d'un convive pour lesquels celui-ci s'est inscrit
        /// </summary>
        /// <param name="c">Convive dont on souhaite récupérer la liste des événements inscris</param>
        /// <returns>List<Evenement></returns>
        public static List<Evenement> GetListeEvenementsInscris(Convive c)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Convives.FirstOrDefault(p => p.Id == c.Id).EvenementsInscris;
            }
        }
    }
}