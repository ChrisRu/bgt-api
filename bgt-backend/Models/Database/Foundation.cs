using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class Foundation
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string MeasurementName { get; set; }

        public string MeasurementCalculation { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public decimal HoursMeasuring { get; set; }

        public decimal HoursCalculating { get; set; }
    }
}