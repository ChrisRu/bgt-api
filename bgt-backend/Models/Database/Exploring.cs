using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class Exploring
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public int? M2 { get; set; }

        public string Name { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public DateTimeOffset? EndDateDelivered { get; set; }

        public string Remarks { get; set; }
    }
}