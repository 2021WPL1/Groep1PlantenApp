using System;
using System.Collections.Generic;

namespace PlantenApplicatie.DATA.Models
{
    public partial class FenoBloeiwijze
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public byte[] Figuur { get; set; }
        public string UrlLocatie { get; set; }
    }
}
