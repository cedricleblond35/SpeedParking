//using BO;
using BO;
using System.Data.Entity;

namespace DAL
{
    public class RepositoryFactory<C> where C : DbContext, IDbContext
    {

        public static IRepository<T> GetRepository<T>(DbContext context) where T : class, IIdentifiable
        {
            return new GenericRepository<T>(context);
        }

        public static IRepository<Convive> GetConviveRepository(C context)
        {
            return new ConviveRepository<C>(context);
        }
        public static IRepository<Evenement> GetEvenementRepository(C context)
        {
            return new EvenementRepository<C>(context);
        }

        public static IRepository<Organisateur> GetOrganisateurRepository(C context)
        {
            return new OrganisateurRepository<C>(context);
        }
    }
}
