using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BGTBackend.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool? IsAdmin { get; set; }
    }
}