using BO;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class OrganisateurRepository<T> : GenericRepository<Organisateur> where T : DbContext, IDbContext
    {
        public OrganisateurRepository(T context) : base(context)
        {
        }

        public override void Update(Organisateur organisateur)
        {
            Organisateur o = GetById(organisateur.Id);
            o.Id = organisateur.Id;
            o.Nom = organisateur.Nom;
            o.Prenom = organisateur.Prenom;
            o.Email = organisateur.Email;
            o.DateNaissance = organisateur.DateNaissance;
            o.Adresse = organisateur.Adresse;
            o.Ville = organisateur.Ville;
            o.CodePostal = organisateur.CodePostal;
            //List<Evenement>
            dbContext.SaveChanges();
        }

    }
}
