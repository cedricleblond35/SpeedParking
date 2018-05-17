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

namespace SolutionPrincipale.Controllers
{
    public class EvenementsController : Controller
    {

        // GET: Evenements
        public ActionResult Index()
        {
            /**
            List<Evenement> liste = new List<Evenement>();
            Evenement e = new Evenement(1,1000,"Event1","Description1",DateTime.Today.AddDays(5), DateTime.Today.AddDays(6),"30 rue ENI","RENNES","35000");
            Evenement e1 = new Evenement(2, 2000, "Event2", "Description2", DateTime.Today.AddDays(-5), DateTime.Today.AddDays(-4), "40 rue ENI", "RENNES", "35000");
            liste.Add(e);
            liste.Add(e1);
            return View(liste);
            **/

            return View(ServiceEvenement.GetListeEvenements());
        }

        // GET: Evenements/Details/5
        public ActionResult Details(int? id)
        {
            Evenement evenement = null;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evenement = ServiceEvenement.GetOneEvenement(id);
            if (evenement.Adresse != "")
            {
                Cartographie.geocoder(evenement);
            }
            if (evenement == null)
            {
                return HttpNotFound();
            }
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
        public ActionResult Create([Bind(Include = "Id,NbParticipants,Nom,Description,DebutEvenement,FinEvenement,Adresse,Ville,CodePostal")] Evenement evenement)
        {
            if (ModelState.IsValid)
            {
                ServiceEvenement.AddEvenement(evenement);
                ServiceGlobal.SaveAll();
                return RedirectToAction("Index");
            }

            return View(evenement);
        }

        // GET: Evenements/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Evenements/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NbParticipants,Nom,Description,DebutEvenement,FinEvenement,Adresse,Ville,CodePostal")] Evenement evenement)
        {
            if (ModelState.IsValid)
            {
                ServiceEvenement.EntryEvenement(evenement);
                ServiceGlobal.SaveAll();
                return RedirectToAction("Index");
            }
            return View(evenement);
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
            ServiceGlobal.SaveAll();
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
