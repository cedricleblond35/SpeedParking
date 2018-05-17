using SolutionPrincipale.Models;

namespace SolutionPrincipale.Service
{
    public class ServiceGlobal
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static void SaveAll()
        {
            db.SaveChanges();
        }

        public static void Dispose()
        {
            db.Dispose();
        }
    }
}