using System;
using System.Collections.Generic;

namespace PlantenApplicatie.UI.Models
{
    public partial class Abiotiek
    {
        public long Id { get; set; }
        public long PlantId { get; set; }
        public string Bezonning { get; set; }
        public string Grondsoort { get; set; }
        public string Vochtbehoefte { get; set; }
        public string Voedingsbehoefte { get; set; }
        public string AntagonischeOmgeving { get; set; }

        public virtual Plant Plant { get; set; }
    }
}
