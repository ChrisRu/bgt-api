using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class EndCheck
    {
        [Column("eind_controle_code")]
        public int Id { get; set; }
        
        [Column("project_code")]
        public int ProjectId { get; set; }
        
        [Column("naam")]
        public string Name { get; set; }
        
        [Column("uren")]
        public decimal? Hours { get; set; }
                
        [Column("einddatum")]
        public DateTimeOffset? EndDate { get; set; }
    }
}