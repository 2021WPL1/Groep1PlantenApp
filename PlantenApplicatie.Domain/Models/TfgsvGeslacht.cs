using System;
using System.Collections.Generic;

namespace PlantenApplicatie.UI.Models
{
    public partial class TfgsvGeslacht
    {
        public TfgsvGeslacht()
        {
            TfgsvSoort = new HashSet<TfgsvSoort>();
        }

        public long GeslachtId { get; set; }
        public long FamilieFamileId { get; set; }
        public string Geslachtnaam { get; set; }
        public string NlNaam { get; set; }

        public virtual TfgsvFamilie FamilieFamile { get; set; }
        public virtual ICollection<TfgsvSoort> TfgsvSoort { get; set; }
    }
}
