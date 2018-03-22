using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class Foundation
    {
        [Column("grondslag_code")] public int Id { get; set; }

        [Column("project_code")] public int ProjectId { get; set; }

        [Column("naam_meten")] public string MeasurementName { get; set; }

        [Column("naam_rekenen")] public string MeasurementCalculation { get; set; }

        [Column("begindatum")] public DateTimeOffset? StartDate { get; set; }

        [Column("einddatum")] public DateTimeOffset? EndDate { get; set; }

        [Column("uren_meten")] public decimal HoursMeasuring { get; set; }

        [Column("uren_rekenen")] public decimal HoursCalculating { get; set; }
    }
}