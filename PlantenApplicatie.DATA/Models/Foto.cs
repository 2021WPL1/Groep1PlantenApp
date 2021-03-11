using System;
using System.Collections.Generic;

namespace PlantenApplicatie.DATA.Models
{
    public partial class Foto
    {
        public long Fotoid { get; set; }
        public long Plant { get; set; }
        public string Eigenschap { get; set; }
        public string UrlLocatie { get; set; }
        public byte[] Tumbnail { get; set; }

        public virtual Plant PlantNavigation { get; set; }
    }
}
