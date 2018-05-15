using BO;
using DAL;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using WebSportAUTH.Models;

namespace WebSportAUTH.Controllers
{
    public abstract class AbstractController : Controller
    {
        protected ApplicationDbContext Context => HttpContext.GetOwinContext().Get<ApplicationDbContext>();

        protected IRepository<T> GetRepository<T>() where T : class, IIdentifiable
        {
            return RepositoryFactory<ApplicationDbContext>.GetRepository<T>(Context);
        }
    }
}