using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class User
    {
        [Column("gebruiker_code")]
        public int Id { get; set; }

        [Column("gebruikersnaam")]
        public string Username { get; set; }

        [Column("wachtwoord")]
        public string Password { get; set; }

        [Column("admin")]
        public bool? IsAdmin { get; set; }
    }
}