using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;
using SolutionPrincipale.Models;

namespace SolutionPrincipale.Controllers
{
    public class OrganisateursController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Organisateurs
        public ActionResult Index()
        {
            return View(db.Organisateurs.ToList());
        }

        // GET: Organisateurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organisateur organisateur = db.Organisateurs.Find(id);
            if (organisateur == null)
            {
                return HttpNotFound();
            }
            return View(organisateur);
        }

        // GET: Organisateurs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Organisateurs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom,Prenom,Email,DateNaissance,Adresse,Ville,CodePostal")] Organisateur organisateur)
        {
            if (ModelState.IsValid)
            {
                db.Organisateurs.Add(organisateur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(organisateur);
        }

        // GET: Organisateurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organisateur organisateur = db.Organisateurs.Find(id);
            if (organisateur == null)
            {
                return HttpNotFound();
            }
            return View(organisateur);
        }

        // POST: Organisateurs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,Prenom,Email,DateNaissance,Adresse,Ville,CodePostal")] Organisateur organisateur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organisateur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(organisateur);
        }

        // GET: Organisateurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organisateur organisateur = db.Organisateurs.Find(id);
            if (organisateur == null)
            {
                return HttpNotFound();
            }
            return View(organisateur);
        }

        // POST: Organisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Organisateur organisateur = db.Organisateurs.Find(id);
            db.Organisateurs.Remove(organisateur);
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
