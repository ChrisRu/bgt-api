using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class MeasurementCheck
    {
        public int Id { get; set; }

        public int MeasurementId { get; set; }

        public string Name { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public decimal? Hours { get; set; }

        public int Points { get; set; }
    }
}