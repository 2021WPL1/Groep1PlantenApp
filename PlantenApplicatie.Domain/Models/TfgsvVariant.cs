using System;
using System.Collections.Generic;

namespace PlantenApplicatie.UI.Models
{
    public partial class TfgsvVariant
    {
        public long VariantId { get; set; }
        public long SoortSoortid { get; set; }
        public string Variantnaam { get; set; }
        public string NlNaam { get; set; }
    }
}
