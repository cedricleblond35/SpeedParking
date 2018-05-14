using BO;
using DAL;
using System;
using System.Diagnostics;

using System.Web.Mvc;
using WebSport.Models;


namespace WebSport.Controllers
{
    public class CompetitorController : Controller
    {
        private IRepository<Convive> repo;
        private IRepository<Race> raceRepo;
        Context dbContext = new Context();
        public CompetitorController()
        {
            repo = RepositoryFactory.GetCompetitorRepository(dbContext);
            raceRepo = RepositoryFactory.GetRaceRepository(dbContext);
        }

        // GET: Competitor
        public ActionResult Index()
        {
            return View(repo.GetAll());
        }

        // GET: Competitor/Details/5
        public ActionResult Details(int id)
        {
            return View(repo.GetById(id));
        }

        // GET: Competitor/Create
        public ActionResult Create()
        {
            var vm = new CreateEditCompetitorVM();
            vm.Races = raceRepo.GetAll();
            return View(vm);
        }

        // POST: Competitor/Create
        [HttpPost]
        public ActionResult Create(CreateEditCompetitorVM competitorVM)
        {
            try
            {
                if (competitorVM.IdSelectedRace.HasValue)
                    competitorVM.Competitor.Race = raceRepo.GetById(competitorVM.IdSelectedRace.Value);

                repo.Insert(competitorVM.Competitor);
                return RedirectToAction("Index");
            }
            catch(Exception e )
            {
                Debug.WriteLine(e.Message);
                return View();
            }
        }

        // GET: Competitor/Edit/5
        public ActionResult Edit(int id)
        {
            var vm = new CreateEditCompetitorVM();
            vm.Competitor = repo.GetById(id);
            if (vm.Competitor.Race != null)
            {
                vm.IdSelectedRace = vm.Competitor.Race.Id;
            }

            vm.Races = raceRepo.GetAll();
            return View(vm);
        }

        // POST: Competitor/Edit/5
        [HttpPost]
        public ActionResult Edit(CreateEditCompetitorVM vm)
        {
            try
            {
                if (vm.IdSelectedRace.HasValue)
                {
                    vm.Competitor.Race = raceRepo.GetById(vm.IdSelectedRace.Value);
                }
                repo.Update(vm.Competitor);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Competitor/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repo.GetById(id));
        }

        // POST: Competitor/Delete/5
        [HttpPost]
        public ActionResult Delete(Convive competitor)
        {
            try
            {
                repo.Delete(competitor.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
