using BO;
using System.Data.Entity;

namespace DAL
{
    class ConviveRepository<T> : GenericRepository<Convive> where T : DbContext, IDbContext
    {
        public ConviveRepository(T context) : base(context)
        {

        }

        public override void Update(Convive convive)
        {
            Convive c = GetById(convive.Id);
            c.Id = convive.Id;
            c.Nom = convive.Nom;
            c.Prenom = convive.Prenom;
            c.Email = convive.Email;
            c.DateNaissance = convive.DateNaissance;
            c.Adresse = convive.Adresse;
            c.Ville = convive.Ville;
            c.CodePostal = convive.CodePostal;
            //List<Evenement>
            dbContext.SaveChanges();
        }
    }
}
