using System;
using System.Collections.Generic;

namespace PlantenApplicatie.DATA.Models
{
    public partial class UpdatePlant
    {
        public int Id { get; set; }
        public long Plantid { get; set; }
        public int Userid { get; set; }
        public DateTime? Updatedatum { get; set; }

        public virtual Plant Plant { get; set; }
        public virtual Gebruiker User { get; set; }
    }
}
