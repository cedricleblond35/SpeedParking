using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSport.Models;

namespace WebSport.Controllers
{
    public class RaceController : Controller
    {
        Context dbContext = new Context();
        private IRepository<Race> repo;
        private IRepository<Organisateur> organizerRepo;
        public RaceController()
        {
            repo = RepositoryFactory.GetRaceRepository(dbContext);
            organizerRepo = RepositoryFactory.GetOrganizerRepository(dbContext);
        }

        

        // GET: Race
        public ActionResult Index()
        {
            return View(repo.GetAll());
        }

        // GET: Race/Details/5
        public ActionResult Details(int id)
        {
            return View(repo.GetById(id));
        }

        // GET: Race/Create
        public ActionResult Create()
        {
            var vm = new CreateEditRaceVM();
            vm.Organizers = organizerRepo.GetAll();
            return View(vm);
        }

        // POST: Race/Create
        [HttpPost]
        public ActionResult Create(CreateEditRaceVM raceVm)
        {
            try
            {
                if (raceVm.IdSelectedOrganizer.HasValue)
                    raceVm.Race.Organizer = organizerRepo.GetById(raceVm.IdSelectedOrganizer.Value);
                repo.Insert(raceVm.Race);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Race/Edit/5
        public ActionResult Edit(int id)
        {
            var vm = new CreateEditRaceVM();
            vm.Race = repo.GetById(id);
            vm.Organizers = organizerRepo.GetAll();
            return View(vm);
        }

        // POST: Race/Edit/5
        [HttpPost]
        public ActionResult Edit(CreateEditRaceVM raceVm)
        {
            try
            {
                // TODO: Add update logic here
                if (raceVm.IdSelectedOrganizer.HasValue)
                {
                    raceVm.Race.Organizer = organizerRepo.GetById(raceVm.IdSelectedOrganizer.Value);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Race/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repo.GetById(id));
        }

        // POST: Race/Delete/5
        [HttpPost]
        public ActionResult Delete(Race r)
        {
            try
            {
                // TODO: Add delete logic here
                repo.Delete(r.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
