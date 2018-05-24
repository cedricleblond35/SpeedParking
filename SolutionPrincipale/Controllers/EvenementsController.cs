using System.Net;
using System.Web.Mvc;
using BO;
using SolutionPrincipale.Models;
using SolutionPrincipale.Service;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using SolutionPrincipale.Services;

namespace SolutionPrincipale.Controllers
{
    public class EvenementsController : Controller
    {
        private const int FREE = 10;
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
            return ServiceCartographie.selectDistanceParking(origin, destination, mode);
        }
        public string selectionParking(string origin, string destination, string mode)
        {
            return ServiceCartographie.selectDistanceParking(origin, destination, mode);
        }

        // GET: Evenements
        public ActionResult Index()
        {
            ListeEvenementsVM vm = new ListeEvenementsVM();
            vm.ListeEvenements = ServiceEvenement.GetListeEvenements();
            Convive c = null;
            if (User.IsInRole("Convive"))
            {
                c = ServiceConvive.GetOneConvive(User.Identity.GetUserId());
                vm.ListeEvenementsInscris = ServiceConvive.GetListeEvenementsInscris(c);
            }
            System.Console.WriteLine(c);
            System.Console.WriteLine(vm);
            return View(vm);
        }

        // GET: Evenements/Details/5
        public ActionResult Details(int? id)
        {
            Evenement evenement;
            List<Parking> parkings;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            evenement = ServiceEvenement.GetOneEvenement(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }



            //liste de l'ensemble des parkings
            parkings = ServiceParking.GetListEventCarpark(evenement);

            //liste des parkings selectionnés selon filtre
            parkings = ServiceParking.SelectNearestCarPark(evenement, parkings, FREE);

            parkings = ServiceParking.FindAdress(parkings);

            //ajouter la liste de tous les parkings à l'évément
            evenement.Parkings = parkings;

            return View(evenement);
        }

        // GET: Evenements/Create
        public ActionResult Create()
        {
            var vm = new CreateEditEvenementVM();
            vm.Evenement = new Evenement();
            vm.Themes = ServiceTheme.GetListeThemes();
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
                eve.Organisateur = ServiceOrganisateur.GetOneOrganisateur(User.Identity.GetUserId());
                ServiceEvenement.AddEvenement(eve, vm);
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
            CreateEditEvenementVM vm = new CreateEditEvenementVM();
            vm.Evenement = evenement;
            vm.Themes = ServiceTheme.GetListeThemes();
            List<int> liste = new List<int>();
            foreach (var i in evenement.Themes)
            {
                liste.Add(i.Id);
            }
            if (evenement == null)
            {
                return HttpNotFound();
            }
            return View(vm);
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
                Organisateur o = ServiceOrganisateur.GetOneOrganisateur(User.Identity.GetUserId());
                ServiceEvenement.EditEvenement(vm, o);
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

        // GET: Evenements/Inscription/5
        public ActionResult Inscription(int id)
        {
            Evenement e = ServiceEvenement.GetOneEvenement(id);
            Convive c = ServiceConvive.GetOneConvive(User.Identity.GetUserId());
            ServiceConvive.Inscription(c, e);
            return RedirectToAction("Index");
        }


        // GET: Evenements/Desinscription/5
        public ActionResult Desinscription(int id)
        {
            Evenement e = ServiceEvenement.GetOneEvenement(id);
            Convive c = ServiceConvive.GetOneConvive(User.Identity.GetUserId());
            ServiceConvive.Desinscription(c, e);
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
