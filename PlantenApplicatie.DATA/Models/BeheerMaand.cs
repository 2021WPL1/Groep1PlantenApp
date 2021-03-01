using System;
using System.Collections.Generic;

namespace PlantenApplicatie.DATA.Models
{
    public partial class BeheerMaand
    {
        public long Id { get; set; }
        public long PlantId { get; set; }
        public string Beheerdaad { get; set; }
        public string Omschrijving { get; set; }
        public bool? Jan { get; set; }
        public bool? Feb { get; set; }
        public bool? Mrt { get; set; }
        public bool? Apr { get; set; }
        public bool? Mei { get; set; }
        public bool? Jun { get; set; }
        public bool? Jul { get; set; }
        public bool? Aug { get; set; }
        public bool? Sept { get; set; }
        public bool? Okt { get; set; }
        public bool? Nov { get; set; }
        public bool? Dec { get; set; }

        public virtual Plant Plant { get; set; }
    }
}
