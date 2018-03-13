using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class Measurement
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Company { get; set; }

        public DateTimeOffset? DeliveryDate { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }
    }
}