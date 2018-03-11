using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class ExploringCheck
    {
        [Column("controle_verkenning_code")]
        public int Id { get; set; }
        
        [Column("verken_code")]
        public int ExploreId { get; set; }
        
        [Column("einddatum")]
        public DateTimeOffset? EndDate { get; set; }
        
        [Column("uren")]
        public decimal? Hours { get; set; }
        
        [Column("naam")]
        public string Name { get; set; }
    }
}