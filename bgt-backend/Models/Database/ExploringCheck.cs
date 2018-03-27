using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class ExploringCheck
    {
        public int Id { get; set; }

        public int ExploringId { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public decimal? Hours { get; set; }

        public string Name { get; set; }
    }
}