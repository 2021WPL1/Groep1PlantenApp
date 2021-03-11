using System;
using System.Collections.Generic;

namespace PlantenApplicatie.DATA.Models
{
    public partial class TfgsvFamilie
    {
        public TfgsvFamilie()
        {
            TfgsvGeslacht = new HashSet<TfgsvGeslacht>();
        }

        public long FamileId { get; set; }
        public long TypeTypeid { get; set; }
        public string Familienaam { get; set; }
        public string NlNaam { get; set; }

        public virtual TfgsvType TypeType { get; set; }
        public virtual ICollection<TfgsvGeslacht> TfgsvGeslacht { get; set; }
    }
}
