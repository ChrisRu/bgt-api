using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class MeasurementMap
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Naam { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public decimal? Hours { get; set; }

        public int? Estimate { get; set; }
    }
}