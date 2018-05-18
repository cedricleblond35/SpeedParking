using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BO;
using SolutionPrincipale.Models;
using SolutionPrincipale.Service;
using System.Collections.Generic;
using DAL;
using Microsoft.AspNet.Identity;

namespace SolutionPrincipale.Controllers
{
    public class EvenementsController : Controller
    {
        

        // GET: Evenements
        public ActionResult Index()
        {
            return View(ServiceEvenement.GetListeEvenements());
        }

        // GET: Evenements/Details/5
        public ActionResult Details(int? id)
        {
            Evenement evenement;
            List<Parking> parking;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            evenement = ServiceEvenement.GetOneEvenement(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }

            parking = ServiceParking.GetListEventCarpark(evenement);

            return View(evenement);
        }

        // GET: Evenements/Create
        public ActionResult Create()
        {
            List<Theme> themes = ServiceTheme.GetListeThemes();
            var vm = new CreateEditEvenementVM();
            vm.Evenement = new Evenement();
            vm.Themes = themes;
            return View(vm);
        }

        // POST: Evenements/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateEditEvenementVM vm)
        {
            if (vm?.Evenement != null)
            {
                Evenement eve = vm.Evenement;
                eve.Organisateur= ServiceOrganisateur.GetOneOrganisateur(User.Identity.GetUserId());
                if (vm.IdSelectedThemes != null)
                {
                    List<Theme> liste = new List<Theme>();
                    foreach (var i in vm.IdSelectedThemes)
                    {
                        liste.Add(ServiceTheme.GetOneTheme(i));
                        eve.Themes = liste;
                    }
                }
                ServiceEvenement.AddEvenement(eve);
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Evenements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenement evenement = ServiceEvenement.GetOneEvenement(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }
            return View(evenement);
        }

        // POST: Evenements/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateEditEvenementVM vm)
        {
            if (vm?.Evenement != null)
            {
                Evenement eve = vm.Evenement;
                if (vm.IdSelectedThemes != null)
                {
                    List<Theme> liste = new List<Theme>();
                    foreach (var i in vm.IdSelectedThemes)
                    {
                        liste.Add(ServiceTheme.GetOneTheme(i));
                        eve.Themes = liste;
                    }
                }
                ServiceEvenement.AddEvenement(eve);
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Evenements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenement evenement = ServiceEvenement.GetOneEvenement(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }
            return View(evenement);
        }

        // POST: Evenements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evenement evenement = ServiceEvenement.GetOneEvenement(id);
            ServiceEvenement.RemoveEvenement(evenement);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServiceGlobal.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
