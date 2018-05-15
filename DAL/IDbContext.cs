using BO;
using System.Data.Entity;

namespace DAL
{
    public interface IDbContext
    {
        DbSet<Evenement> Evenements { get; set; }
        DbSet<Convive> Convives { get; set; }
        DbSet<Organisateur> Organisateurs { get; set; }
    }
}