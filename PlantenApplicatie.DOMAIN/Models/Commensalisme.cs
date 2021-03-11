using System;
using System.Collections.Generic;

namespace PlantenApplicatie.DOMAIN.Models
{
    public partial class Commensalisme
    {
        public long Id { get; set; }
        public long PlantId { get; set; }
        public string Ontwikkelsnelheid { get; set; }
        public string Strategie { get; set; }

        public virtual Plant Plant { get; set; }
    }
}
