using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class Processing
    {
        [Column("verwerk_code")] public int Id { get; set; }

        [Column("project_code")] public int ProjectId { get; set; }

        [Column("naam")] public string Name { get; set; }

        [Column("begindatum")] public DateTimeOffset? StartDate { get; set; }

        [Column("einddatum")] public DateTimeOffset? EndDate { get; set; }

        [Column("uren")] public decimal? Hours { get; set; }
    }
}