using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class EndCheck
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Name { get; set; }

        public decimal? Hours { get; set; }

        public DateTimeOffset? EndDate { get; set; }
    }
}