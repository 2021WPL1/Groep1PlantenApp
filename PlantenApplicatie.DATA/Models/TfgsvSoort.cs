using System;
using System.Collections.Generic;

namespace PlantenApplicatie.DATA.Models
{
    public partial class TfgsvSoort
    {
        public long Soortid { get; set; }
        public long GeslachtGeslachtId { get; set; }
        public string Soortnaam { get; set; }
        public string NlNaam { get; set; }
    }
}
