using System.Collections.Generic;

namespace PlantenApplicatie.Domain.Models
{
    public partial class TfgsvSoort
    {
        public TfgsvSoort()
        {
            TfgsvVariant = new HashSet<TfgsvVariant>();
        }

        public long Soortid { get; set; }
        public long GeslachtGeslachtId { get; set; }
        public string Soortnaam { get; set; }
        public string NlNaam { get; set; }

        public virtual TfgsvGeslacht GeslachtGeslacht { get; set; }
        public virtual ICollection<TfgsvVariant> TfgsvVariant { get; set; }
    }
}
