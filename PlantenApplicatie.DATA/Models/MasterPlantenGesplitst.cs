using System;
using System.Collections.Generic;

namespace PlantenApplicatie.DATA.Models
{
    public partial class MasterPlantenGesplitst
    {
        public string Id { get; set; }
        public string Plantnaam { get; set; }
        public string Familienaam { get; set; }
        public string Groep { get; set; }
        public string Familie { get; set; }
        public string Geslacht { get; set; }
        public string Soort { get; set; }
        public string Variant { get; set; }
        public int? GroepId { get; set; }
        public int? FamilieId { get; set; }
        public int? GeslachtId { get; set; }
        public int? SoortId { get; set; }
    }
}
