using BO;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class EvenementRepository<T> : GenericRepository<Evenement> where T : DbContext, IDbContext
    {
        public EvenementRepository(T context) : base(context)
        {

        }

        public override void Update(Evenement evenement)
        {
            Evenement o = set.Include(a => a.Themes).SingleOrDefault(e => e.Id == evenement.Id);
            o.Id = evenement.Id;
            o.Nom = evenement.Nom;
            o.NbParticipants = evenement.NbParticipants;
            o.Description = evenement.Description;
            o.DebutEvenement = evenement.DebutEvenement;
            o.DureeMinutes = evenement.DureeMinutes;
            o.Images = evenement.Images;
            o.Adresse = evenement.Adresse;
            o.Ville = evenement.Ville;
            o.CodePostal = evenement.CodePostal;
            o.Organisateur = evenement.Organisateur;
            o.Themes.Clear();
            foreach (var t in evenement.Themes)
            {
                o.Themes.Add(t);
            }
            //List<Convive>
            dbContext.SaveChanges();
        }

    }
}
