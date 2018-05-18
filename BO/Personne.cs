using System;
using System.ComponentModel;

namespace BO
{
    public class Personne : IIdentifiable, IAdresse
    {
        public int Id { get; set; }
        [DisplayName("Nom")]
        public string Nom { get; set; }
        [DisplayName("Prénom")]
        public string Prenom { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Date de naissance")]
        public DateTime? DateNaissance { get; set; }
        [DisplayName("Adresse")]
        public string Adresse { get; set; }
        [DisplayName("Ville")]
        public string Ville { get; set; }
        [DisplayName("Code Postal")]
        public string CodePostal { get; set; }
        [DisplayName("IdUser")]
        public string IdUser { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public Personne()
        {

        }

        public Personne(string email, string idUser)
        {
            Email = email;
            IdUser = idUser;
        }
    }
}