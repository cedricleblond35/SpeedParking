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
                ConviveRepository<ApplicationDbContext> rep = new ConviveRepository<ApplicationDbContext>(db);
                if (String.IsNullOrWhiteSpace(id))
                {
                    return null;
                }
                else
                {
                    return rep.GetByUserId(id);
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
                var liste = db.Convives.FirstOrDefault(p => p.Id == id).EvenementsInscris;
                return liste;
            }
        }

        /// <summary>
        /// Récupèration de la liste des événements d'un convive pour lesquels celui-ci s'est inscrit
        /// </summary>
        /// <param name="c">Convive dont on souhaite récupérer la liste des événements inscris</param>
        /// <returns>List<Evenement></returns>
        public static List<Evenement> GetListeEvenementsInscris(Convive convive)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ConviveRepository<ApplicationDbContext> rep = new ConviveRepository<ApplicationDbContext>(db);
                return rep.GetById(convive.Id).EvenementsInscris;
            }
        }

        /// <summary>
        /// Inscription d'un convive à un événement
        /// </summary>
        /// <param name="c">Convive qui s'inscrit</param>
        /// <param name="e">Evénement auquel il s'inscrit</param>
        /// <returns>bool</returns>
        public static bool Inscription(Convive convive, Evenement evenement)
        {
            List<Evenement> eveInscris = GetListeEvenementsInscris(convive);
            if (eveInscris != null && eveInscris.Any(e1=>e1.Id == evenement.Id))
            {
                return false;
            }
            convive.EvenementsInscris = eveInscris;
            convive.EvenementsInscris.Add(evenement);
            EditConvive(convive);
            return true;
        }

        /// <summary>
        /// Désinscription d'un convive à un événement
        /// </summary>
        /// <param name="convive">Convive qui se désinscrit</param>
        /// <param name="evenement">Evénement duquel il se désinscrit</param>
        /// <returns>bool</returns>
        public static bool Desinscription(Convive convive, Evenement evenement)
        {
            List<Evenement> eveInscris = GetListeEvenementsInscris(convive);
            if (eveInscris == null || !eveInscris.Any(e1 => e1.Id == evenement.Id))
            {
                return false;
            }
            var eve = eveInscris.SingleOrDefault(e => e.Id == evenement.Id);
            eveInscris.Remove(eve);
            convive.EvenementsInscris = eveInscris;
            EditConvive(convive);
            return true;
        }

        /// <summary>
        /// Edition d'un convive
        /// </summary>
        /// <param name="convive">Convive modifié à sauvegarder</param>
        public static void EditConvive(Convive convive)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ConviveRepository<ApplicationDbContext> rep = new ConviveRepository<ApplicationDbContext>(db);
                List<Evenement> liste = new List<Evenement>();
                foreach (var evenement in convive.EvenementsInscris)
                {
                    liste.Add(db.Evenements.Find(evenement.Id));
                }
                convive.EvenementsInscris = liste;
                rep.Update(convive);
            }
        }
    }
}