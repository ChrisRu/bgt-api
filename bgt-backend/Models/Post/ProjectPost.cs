using System;

namespace BGTBackend.Models
{
    public class ProjectPost
    {
        public int? Id { get; set; }

        public string BgtOnNumber { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string Location { get; set; }

        public int? LocationCode { get; set; }

        public DateTimeOffset? LastEditedDate { get; set; }

        public int LastEditedUser { get; set; }
    }
}