using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string BGTonNumber { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        [Required]
        public int? Location { get; set; }

        public DateTimeOffset? LastEditedDate { get; set; }

        public int? LastEditedUser { get; set; }
    }
}