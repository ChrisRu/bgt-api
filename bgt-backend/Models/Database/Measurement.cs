﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGTBackend.Models
{
    public class Measurement
    {
        [Column("meting_code")]
        public int Id { get; set; }
        
        [Column("project_code")]
        public int ProjectId { get; set; }
        
        [Column("bedrijf")]
        public string Company { get; set; }
        
        [Column("oplevering")]
        public DateTimeOffset? DeliveryDate { get; set; }
        
        [Column("begindatum")]
        public DateTimeOffset? StartDate { get; set; }
        
        [Column("einddatum_minicomp")]
        public DateTimeOffset? EndDate { get; set; }
    }
}