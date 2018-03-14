﻿using System;

namespace BGTBackend.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string BgtOnNumber { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public Location Location { get; set; }

        public DateTimeOffset? LastEditedDate { get; set; }

        public User LastEditedUser { get; set; }
    }
}