using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class Preparation
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Name { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public decimal? Hours { get; set; }
    }
}