using System;
using System.Collections.Generic;

namespace PlantenApplicatie.Domain.Models
{
    public partial class FenoHabitus
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public byte[] Figuur { get; set; }
        public string UrlLocatie { get; set; }
    }
}
