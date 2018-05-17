using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BO;
using SolutionPrincipale.Models;
using SolutionPrincipale.Service;
using System.Collections.Generic;

namespace SolutionPrincipale.Controllers
{
    public class EvenementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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

            return View(db.Evenements.ToList());
        }

        // GET: Evenements/Details/5
        public ActionResult Details(int? id)
        {
            Evenement evenement = null;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evenement = db.Evenements.Find(id);
            if (evenement.Adresse != "")
            {
                ServiceCartographie.geocoder(evenement);
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
            List<Theme> themes = db.Themes.ToList();
            CreateEditEvenementVM eve = new CreateEditEvenementVM();
            eve.Themes = themes;
            return View(eve);
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
                //si une adresse existe on là géolocalise
                if (!String.IsNullOrWhiteSpace(evenement.Adresse))
                {
                    ServiceCartographie.geocoder(evenement);
                }
                db.Evenements.Add(evenement);
                db.SaveChanges();
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
            Evenement evenement = db.Evenements.Find(id);
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
                db.Entry(evenement).State = EntityState.Modified;
                db.SaveChanges();
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
            Evenement evenement = db.Evenements.Find(id);
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
            Evenement evenement = db.Evenements.Find(id);
            db.Evenements.Remove(evenement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
