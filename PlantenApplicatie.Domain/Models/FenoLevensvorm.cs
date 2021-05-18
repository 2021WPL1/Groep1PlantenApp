using System;
using System.Collections.Generic;

namespace PlantenApplicatie.Domain.Models
{
    public partial class FenoLevensvorm
    {
        public int Id { get; set; }
        public string Levensvorm { get; set; }
        public byte[] Figuur { get; set; }
        public string UrlLocatie { get; set; }
    }
}
