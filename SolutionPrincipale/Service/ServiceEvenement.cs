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
        public static List<Evenement> GetListeEvenements()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                GenericRepository<Evenement> rep = new GenericRepository<Evenement>(db);
                return rep.GetAll();
            }
        }

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
                rep.Insert(e);
            }
        }
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

        public static void EditEvenement(CreateEditEvenementVM vm, Organisateur o)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                GenericRepository<Evenement> rep = new GenericRepository<Evenement>(db);
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