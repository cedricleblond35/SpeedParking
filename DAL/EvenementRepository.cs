using BO;
using System.Data.Entity;

namespace DAL
{
    public class EvenementRepository<T> : GenericRepository<Evenement> where T : DbContext, IDbContext
    {
        public EvenementRepository(T context) : base(context)
        {

        }
        public override void Update(Evenement evenement)
        {
            Evenement o = GetById(evenement.Id);
            o.Id = evenement.Id;
            o.Nom = evenement.Nom;
            o.NbParticipants = evenement.NbParticipants;
            o.Description = evenement.Description;
            o.DebutEvenement = evenement.DebutEvenement;
            o.DureeMinutes = evenement.DureeMinutes;
            o.Themes = evenement.Themes;
            o.Images = evenement.Images;
            o.Adresse = evenement.Adresse;
            o.Ville = evenement.Ville;
            o.CodePostal = evenement.CodePostal;
            o.Organisateur = evenement.Organisateur;
            //List<Convive>
            dbContext.SaveChanges();
        }

    }
}
