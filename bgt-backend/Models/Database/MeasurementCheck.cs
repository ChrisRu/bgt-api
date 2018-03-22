using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class MeasurementCheck
    {
        [Column("controle_meting_code")] public int Id { get; set; }

        [Column("meting_code")] public int MeasurementId { get; set; }

        [Column("naam")] public string Name { get; set; }

        [Column("einddatum")] public DateTimeOffset? EndDate { get; set; }

        [Column("uren")] public decimal? Hours { get; set; }

        [Column("voorlopige_geleverde_punten")]
        public int Points { get; set; }
    }
}