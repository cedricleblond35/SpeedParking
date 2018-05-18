using SolutionPrincipale.Models;

namespace SolutionPrincipale.Service
{
    public class ServiceGlobal
    {

        public static void Dispose()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Dispose();
            }
        }
    }
}