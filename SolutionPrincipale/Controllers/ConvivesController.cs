using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;
using SolutionPrincipale.Models;

namespace SolutionPrincipale.Controllers
{
    public class ConvivesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Convives
        public ActionResult Index()
        {
            return View(db.Convives.ToList());
        }

        // GET: Convives/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Convive convive = db.Convives.Find(id);
            if (convive == null)
            {
                return HttpNotFound();
            }
            return View(convive);
        }

        // GET: Convives/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Convives/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom,Prenom,Email,DateNaissance,Adresse,Ville,CodePostal")] Convive convive)
        {
            if (ModelState.IsValid)
            {
                db.Convives.Add(convive);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(convive);
        }

        // GET: Convives/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Convive convive = db.Convives.Find(id);
            if (convive == null)
            {
                return HttpNotFound();
            }
            return View(convive);
        }

        // POST: Convives/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,Prenom,Email,DateNaissance,Adresse,Ville,CodePostal")] Convive convive)
        {
            if (ModelState.IsValid)
            {
                db.Entry(convive).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(convive);
        }

        // GET: Convives/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Convive convive = db.Convives.Find(id);
            if (convive == null)
            {
                return HttpNotFound();
            }
            return View(convive);
        }

        // POST: Convives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Convive convive = db.Convives.Find(id);
            db.Convives.Remove(convive);
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
