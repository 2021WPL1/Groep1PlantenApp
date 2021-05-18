using System;
using System.Collections.Generic;

namespace PlantenApplicatie.Domain.Models
{
    public partial class Fenotype
    {
        public long Id { get; set; }
        public long PlantId { get; set; }
        public int? Bladgrootte { get; set; }
        public string Bladvorm { get; set; }
        public string RatioBloeiBlad { get; set; }
        public string Spruitfenologie { get; set; }
        public string Bloeiwijze { get; set; }
        public string Habitus { get; set; }
        public string Levensvorm { get; set; }

        public virtual Plant Plant { get; set; }
    }
}
