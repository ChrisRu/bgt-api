using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class Stereokarteren
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Name { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public decimal? Hours { get; set; }

        public int Points { get; set; }
    }
}