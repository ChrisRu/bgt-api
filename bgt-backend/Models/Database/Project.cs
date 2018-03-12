using System;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [Column("locatie_code")]
        public int? Location { get; set; }

        [Column("laatst_aangepast_datum")]
        public DateTimeOffset? LastEditedDate { get; set; }

        [Column("laatst_aangepast_gebruiker")]
        public int? LastEditedUser { get; set; }
    }
}