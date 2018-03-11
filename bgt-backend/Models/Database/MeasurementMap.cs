using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class MeasurementMap
    {
        [Column("meetmap_code")]
        public int Id { get; set; }
        
        [Column("project_code")]
        public int ProjectId { get; set; }
        
        [Column("naam")]
        public string Naam { get; set; }
        
        [Column("einddatum")]
        public DateTimeOffset? EndDate { get; set; }
        
        [Column("uren")]
        public decimal? Hours { get; set; }
        
        [Column("geschatte_te_meten")]
        public int? Estimate { get; set; }
    }
}