using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class Exploring
    {
        [Column("verken_code")]
        public int Id { get; set; }
        
        [Column("project_code")]
        public int ProjectId { get; set; }
        
        [Column("m2_code")]
        public int? M2 { get; set; }
        
        [Column("naam")]
        public string Name { get; set; }
        
        [Column("begindatum")]
        public DateTimeOffset? StartDate { get; set; }
        
        [Column("einddatum")]
        public DateTimeOffset? EndDate { get; set; }
        
        [Column("einddatum_opgeleverd")]
        public DateTimeOffset? EndDateDelivered { get; set; }
        
        [Column("opmerking")]
        public string Remarks { get; set; }
    }
}