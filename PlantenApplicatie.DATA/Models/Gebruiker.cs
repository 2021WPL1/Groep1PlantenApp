using System;
using System.Collections.Generic;

namespace PlantenApplicatie.DATA.Models
{
    public partial class Gebruiker
    {
        public Gebruiker()
        {
            UpdatePlant = new HashSet<UpdatePlant>();
        }

        public int Id { get; set; }
        public string Vivesnr { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Rol { get; set; }
        public string Emailadres { get; set; }
        public DateTime? LastLogin { get; set; }
        public byte[] HashPaswoord { get; set; }

        public virtual ICollection<UpdatePlant> UpdatePlant { get; set; }
    }
}
