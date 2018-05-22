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
        /// <summary>
        /// Retourne la distance du parking par rappor au parking
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public string selectDistanceParking(string origin, string destination, string mode)
        {
            return ServiceCartographie.selectDistanceParking(origin, destination,mode);
        }
        public string selectionParking(string origin, string destination, string mode)
        {
            return ServiceCartographie.selectDistanceParking(origin, destination, mode);
        }

        // GET: Evenements
        public ActionResult Index()
        {
            return View(ServiceEvenement.GetListeEvenements());
        }

        // GET: Evenements/Details/5
        public ActionResult Details(int? id)
        {
            Evenement evenement;
            List<Parking> parkings;
            List<Parking> parkingsPlusProche;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //initialiser l'objet événement
            evenement = ServiceEvenement.GetOneEvenement(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }

            //liste de l'ensemble des parkings
            parkings = ServiceParking.GetListEventCarpark(evenement);

            // evenement = ServiceParking.SelectNearestCarPark(evenement, parkings);



            //ajouter la liste de tous les parkings à l'évément
            evenement.Parkings = parkings;

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
        public ActionResult Edit([Bind(Include = "Id,NbParticipants,Nom,Description,DebutEvenement,FinEvenement,Adresse,Ville,CodePostal")] Evenement evenement)
        {
            if (ModelState.IsValid)
            {
                ServiceEvenement.EntryEvenement(evenement);
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
