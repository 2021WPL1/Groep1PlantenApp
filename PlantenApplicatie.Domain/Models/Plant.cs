using System;
using System.Collections.Generic;

namespace PlantenApplicatie.Domain.Models
{
    public partial class Plant
    {
        public Plant()
        {
            Abiotiek = new HashSet<Abiotiek>();
            AbiotiekMulti = new HashSet<AbiotiekMulti>();
            BeheerMaand = new HashSet<BeheerMaand>();
            Commensalisme = new HashSet<Commensalisme>();
            CommensalismeMulti = new HashSet<CommensalismeMulti>();
            ExtraEigenschap = new HashSet<ExtraEigenschap>();
            Fenotype = new HashSet<Fenotype>();
            Foto = new HashSet<Foto>();
            UpdatePlant = new HashSet<UpdatePlant>();
        }

        public long PlantId { get; set; }
        public string Type { get; set; }
        public string Familie { get; set; }
        public string Geslacht { get; set; }
        public string Soort { get; set; }
        public string Variant { get; set; }
        public string Fgsv { get; set; }
        public string NederlandsNaam { get; set; }
        public short? PlantdichtheidMin { get; set; }
        public short? PlantdichtheidMax { get; set; }
        public short? Status { get; set; }
        public int? IdAccess { get; set; }
        public int? TypeId { get; set; }
        public int? FamilieId { get; set; }
        public int? GeslachtId { get; set; }
        public int? SoortId { get; set; }
        public int? VariantId { get; set; }

        public virtual ICollection<Abiotiek> Abiotiek { get; set; }
        public virtual ICollection<AbiotiekMulti> AbiotiekMulti { get; set; }
        public virtual ICollection<BeheerMaand> BeheerMaand { get; set; }
        public virtual ICollection<Commensalisme> Commensalisme { get; set; }
        public virtual ICollection<CommensalismeMulti> CommensalismeMulti { get; set; }
        public virtual ICollection<ExtraEigenschap> ExtraEigenschap { get; set; }
        public virtual ICollection<Fenotype> Fenotype { get; set; }
        public virtual ICollection<Foto> Foto { get; set; }
        public virtual ICollection<UpdatePlant> UpdatePlant { get; set; }
    }
}
