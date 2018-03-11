using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class Project
    {
        [Column("project_code")]
        public string Id { get; set; }
        
        [Column("bgton_nummer")]
        public string BGTonNumber { get; set; }
        
        [Column("status")]
        public string Status { get; set; }
        
        [Column("omschrijving")]
        public string Description { get; set; }
        
        [Column("categorie")]
        public string Category { get; set; }
        
        [Column("locatie_code")]
        public int? Location { get; set; }
    }
}