using System;
using System.Collections.Generic;

namespace PlantenApplicatie.DATA.Models
{
    public partial class TfgsvFamilie
    {
        public long FamileId { get; set; }
        public long TypeTypeid { get; set; }
        public string Familienaam { get; set; }
        public string NlNaam { get; set; }
    }
}
