using System;
using System.Collections.Generic;

namespace PlantenApplicatie.DATA.Models
{
    public partial class ExtraEigenschap
    {
        public long Id { get; set; }
        public long PlantId { get; set; }
        public string Nectarwaarde { get; set; }
        public string Pollenwaarde { get; set; }
        public bool? Bijvriendelijke { get; set; }
        public bool? Vlindervriendelijk { get; set; }
        public bool? Eetbaar { get; set; }
        public bool? Kruidgebruik { get; set; }
        public bool? Geurend { get; set; }
        public bool? Vorstgevoelig { get; set; }

        public virtual Plant Plant { get; set; }
    }
}
