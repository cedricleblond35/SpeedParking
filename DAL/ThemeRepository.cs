using BO;
using System.Data.Entity;

namespace DAL
{
    public class ThemeRepository<T> : GenericRepository<Theme> where T : DbContext, IDbContext
    {
        public ThemeRepository(T context) : base(context)
        {

        }
        public override void Update(Theme theme)
        {
            Theme o = GetById(theme.Id);
            o.Id = theme.Id;
            o.Nom = theme.Nom;
            o.Description = theme.Description;
            dbContext.SaveChanges();
        }

    }
}
