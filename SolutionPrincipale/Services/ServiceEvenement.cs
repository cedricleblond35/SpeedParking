using BO;
using DAL;
using SolutionPrincipale.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SolutionPrincipale.Service
{
    public class ServiceEvenement
    {
        /// <summary>
        /// Récupération de tous les événements
        /// </summary>
        /// <returns>List<Evenement></returns>
        public static List<Evenement> GetListeEvenements()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                GenericRepository<Evenement> rep = new GenericRepository<Evenement>(db);
                return rep.GetAll();
            }
        }

        /// <summary>
        /// Récupération d'un événement
        /// </summary>
        /// <param name="id">Id de l'événement à récupérer</param>
        /// <returns>Evenement</returns>
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

        /// <summary>
        /// Ajout d'un événement
        /// </summary>
        /// <param name="e">Evénement à ajouter</param>
        public static void AddEvenement(Evenement e)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                GenericRepository<Evenement> rep = new GenericRepository<Evenement>(db);
                if (!String.IsNullOrWhiteSpace(e.Adresse))
                {
                    ServiceCartographie.geocoder(e);
                }
                rep.Insert(e);
            }
        }

        /// <summary>
        /// Ajout d'un événement via le viewModel
        /// </summary>
        /// <param name="e">Evénement à ajouter</param>
        /// <param name="vm">ViewModel servant de base à la création du nouvel événement</param>
        public static void AddEvenement(Evenement e, CreateEditEvenementVM vm)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                GenericRepository<Evenement> rep = new GenericRepository<Evenement>(db);
                if (!String.IsNullOrWhiteSpace(e.Adresse))
                {
                    ServiceCartographie.geocoder(e);
                }
                List<Theme> liste = new List<Theme>();
                if (vm.IdSelectedThemes != null)
                {
                    foreach (var i in vm.IdSelectedThemes)
                    {
                        liste.Add(ServiceTheme.GetOneTheme(i, db));
                    }
                    e.Themes = liste;
                }
                rep.Insert(e);
            }
        }

        /// <summary>
        /// Edition d'un élément
        /// </summary>
        /// <param name="vm">ViewModel de base servant à la modification de l'élément existant</param>
        /// <param name="o">Organisateur de l'événement</param>
        public static void EditEvenement(CreateEditEvenementVM vm, Organisateur o)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                EvenementRepository<ApplicationDbContext> rep = new EvenementRepository<ApplicationDbContext>(db);
                if (!String.IsNullOrWhiteSpace(vm.Evenement.Adresse))
                {
                    ServiceCartographie.geocoder(vm.Evenement);
                }
                List<Theme> liste = new List<Theme>();
                if (vm.IdSelectedThemes != null)
                {
                    foreach (var i in vm.IdSelectedThemes)
                    {
                        liste.Add(ServiceTheme.GetOneTheme(i, db));
                    }
                    vm.Evenement.Themes = liste;
                }
                vm.Evenement.Organisateur = o;
                rep.Update(vm.Evenement);
            }
        }

        /// <summary>
        /// Suppression d'un événement
        /// </summary>
        /// <param name="e">Evénement à supprimer</param>
        public static void RemoveEvenement(Evenement e)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                GenericRepository<Evenement> rep = new GenericRepository<Evenement>(db);
                rep.Delete(e.Id);
            }
        }
    }
}