using System;

namespace BO
{
    public class Personne : IIdentifiable, IAdresse
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public DateTime? DateNaissance { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public string CodePostal { get; set; }
        public string IdUser { get; set; }

        public Personne(string email, string idUser)
        {
            Email = email;
            IdUser = idUser;
        }
    }
}