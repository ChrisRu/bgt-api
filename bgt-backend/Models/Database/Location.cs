using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class Location
    {
        [Column("locatie_code")]
        public int Id { get; set; }
        
        [Column("longtitude")]
        public string Longtitude { get; set; }
        
        [Column("latitude")]
        public string Latitude { get; set; }
    }
}