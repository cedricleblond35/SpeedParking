using BO;
using DAL;
using System;
using System.Diagnostics;

using System.Web.Mvc;
using WebSport.Models;


namespace WebSport.Controllers
{
    public class ConviveController : Controller
    {
        private IRepository<Convive> repo;
        Context dbContext = new Context();
        public ConviveController()
        {
            repo = RepositoryFactory.GetConviveRepository(dbContext);
        }

        // GET: Convive
        public ActionResult Index()
        {
            return View(repo.GetAll());
        }

        // GET: Convive/Details/5
        public ActionResult Details(int id)
        {
            return View(repo.GetById(id));
        }

        // GET: Convive/Create
        public ActionResult Create()
        {
            var vm = new CreateEditConviveVM();
            vm.Races = raceRepo.GetAll();
            return View(vm);
        }

        // POST: Convive/Create
        [HttpPost]
        public ActionResult Create(CreateEditConviveVM conviveVM)
        {
            try
            {
                if (conviveVM.IdSelectedRace.HasValue)
                    conviveVM.Convive.Race = raceRepo.GetById(conviveVM.IdSelectedRace.Value);

                repo.Insert(conviveVM.Convive);
                return RedirectToAction("Index");
            }
            catch(Exception e )
            {
                Debug.WriteLine(e.Message);
                return View();
            }
        }

        // GET: Convive/Edit/5
        public ActionResult Edit(int id)
        {
            var vm = new CreateEditConviveVM();
            vm.Convive = repo.GetById(id);
            if (vm.Convive.Race != null)
            {
                vm.IdSelectedRace = vm.Convive.Race.Id;
            }

            vm.Races = raceRepo.GetAll();
            return View(vm);
        }

        // POST: Convive/Edit/5
        [HttpPost]
        public ActionResult Edit(CreateEditConviveVM vm)
        {
            try
            {
                if (vm.IdSelectedRace.HasValue)
                {
                    vm.Convive.Race = raceRepo.GetById(vm.IdSelectedRace.Value);
                }
                repo.Update(vm.Convive);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Convive/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repo.GetById(id));
        }

        // POST: Convive/Delete/5
        [HttpPost]
        public ActionResult Delete(Convive convive)
        {
            try
            {
                repo.Delete(convive.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
