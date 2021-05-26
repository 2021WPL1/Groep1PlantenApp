using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;

namespace PlantenApplicatie.Data
{
    public class PlantenDataService
    {
        private static readonly PlantenDataService instance = new PlantenDataService();
        private Planten2021Context context;

        public static PlantenDataService Instance()
        {
            return instance;
        }

        private PlantenDataService()
        {
            this.context = new Planten2021Context();
        }

        //Mainwindow
        public List<TfgsvType> GetTfgsvTypes()
        {
            return context.TfgsvType.ToList();
        }
        public List<TfgsvFamilie> GetTfgsvFamilies()
        {
            return context.TfgsvFamilie.ToList();
        }
        public List<TfgsvGeslacht> GetTfgsvGeslachten()
        {
            return context.TfgsvGeslacht.ToList();
        }
        public List<TfgsvSoort> GetTfgsvSoorten()
        {
            return context.TfgsvSoort.ToList();
        }
        public List<TfgsvVariant> GetTfgsvVarianten()
        {
            return context.TfgsvVariant.ToList();
        }

        public Object[] GetFilteredFamilies(long typeId)
        {
            Object[] fgsv = new object[4];
            fgsv[0] = context.TfgsvFamilie.Where(p => p.TypeTypeid == typeId).ToList();
            fgsv[1] = context.TfgsvGeslacht.Where(p => p.FamilieFamile.TypeTypeid == typeId).ToList();
            fgsv[2] = context.TfgsvSoort.Where(p => p.GeslachtGeslacht.FamilieFamile.TypeTypeid == typeId).ToList();

            //Bug variant heeft geen soort om te koppelen
            fgsv[3] = context.TfgsvVariant.ToList();

            return fgsv;
        }

        public Object[] GetFilteredGeslachten(long familieId)
        {
            Object[] gsv = new object[3];
            gsv[0] = context.TfgsvGeslacht.Where(p => p.FamilieFamileId == familieId).ToList();
            gsv[1] = context.TfgsvSoort.Where(p => p.GeslachtGeslacht.FamilieFamileId == familieId).ToList();

            //Bug variant heeft geen soort om te koppelen
            gsv[2] = context.TfgsvVariant.ToList();

            return gsv;
        }

        public Object[] GetFilteredSoorten(long geslachtId)
        {
            Object[] sv = new object[2];
            sv[0] = context.TfgsvSoort.Where(p => p.GeslachtGeslachtId == geslachtId).ToList();

            //Bug variant heeft geen soort om te koppelen
            sv[1] = context.TfgsvVariant.ToList();

            return sv;
        }

        public List<TfgsvVariant> GetFilteredVarianten(long soortId)
        {
            //Bug variant heeft geen soort om te koppelen
            return context.TfgsvVariant.ToList();
        }

        //Geeft alle planten
        public List<Plant> GetAllPlants()
        {
            return context.Plant.ToList();
        }

        //Jelle & Hemen
        //Functie filtert de planten
        public List<Plant> GetPlantResults(string type, long id, List<Plant> plantResults)
        {
            //Switch voor de juiste id te weten waarmee gefilterd moet worden
            switch (type)
            {
                case "Type":
                    return plantResults.Where(p => p.TypeId == id).ToList();
                case "Familie":
                    return plantResults.Where(p => p.FamilieId == id).ToList();
                case "Geslacht":
                    return plantResults.Where(p => p.GeslachtId == id).ToList();
                case "Soort":
                    return plantResults.Where(p => p.SoortId == id).ToList();
                case "Variant":
                    return plantResults.Where(p => p.VariantId == id).ToList();
                default:
                    return null;
            }
        }

        //Geeft de plant en al zijn informatie
        public Plant GetPlantWithId(long Id)
        {
            return context.Plant.SingleOrDefault(p => p.PlantId == Id);
        }
        //Geeft de plant en zijn uiterlijke kenmerken + habitus
        public Fenotype GetFenotype(long Id)
        {
            return context.Fenotype.SingleOrDefault(p => p.PlantId == Id);
        }
        //Geeft de plant en zijn behoeftes
        public Abiotiek GetAbiotiek(long Id)
        {
            return context.Abiotiek.SingleOrDefault(a => a.PlantId == Id);
        }

        public List<AbiotiekMulti> GetAbiotiekMulti(long Id)
        {
            return context.AbiotiekMulti.Where(a => a.PlantId == Id).ToList();
        }

        //Geeft de plant en zijn ontwikkelsnelheid & strategie
        public Commensalisme GetCommensalisme(long Id)
        {
            return context.Commensalisme.SingleOrDefault(c => c.PlantId == Id);
        }
        //Geeft de plant & zijn specifieke eigenschappen
        public ExtraEigenschap GetExtraEigenschap(long Id)
        {
            return context.ExtraEigenschap.SingleOrDefault(e => e.PlantId == Id);
        }
        //Geeft de plant en de het onnderhoud per maand
        public BeheerMaand GetBeheerMaand(long Id)
        {
            return context.BeheerMaand.SingleOrDefault(b => b.PlantId == Id);
        }

        //Editwindow

        //Filters
        public TfgsvType GetFilterType(int? plantId)
        {
            return context.TfgsvType.FirstOrDefault(f => f.Planttypeid == plantId);
        }

        public TfgsvFamilie GetFilterFamilie(int? plantId)
        {
            return context.TfgsvFamilie.FirstOrDefault(p => p.FamileId == plantId);
        }

        public TfgsvGeslacht GetFilterGeslacht(int? plantId)
        {
            return context.TfgsvGeslacht.FirstOrDefault(p => p.GeslachtId == plantId);
        }

        public TfgsvSoort GetFilterSoort(int? plantId)
        {
            return context.TfgsvSoort.FirstOrDefault(p => p.Soortid == plantId);
        }

        public TfgsvVariant GetFilterVariant(int? plantId)
        {
            return context.TfgsvVariant.FirstOrDefault(p => p.VariantId == plantId);
        }
        //Fenotype
        public string GetFenoMaxBladHoogte(long id)
        {
            return context.Fenotype.FirstOrDefault(f => f.PlantId == id).MaxBladhoogte.ToString();
        }
        public string GetFenoMaxBloeiHoogte(long id)
        {
            return context.Fenotype.FirstOrDefault(f => f.PlantId == id).MaxBloeihoogte.ToString();
        }
        public string GetFenoMinBloeiHoogte(long id)
        {
            return context.Fenotype.FirstOrDefault(f => f.PlantId == id).MinBloeihoogte.ToString();
        }
        public FenotypeMulti GetFenotypeMulti(long id)
        {
            return context.FenotypeMulti.FirstOrDefault(f => f.PlantId == id);
        }
        public FenoMaand GetFenoMaxBladHoogteMaand(long id)
        {
            return context.FenoMaand.FirstOrDefault(f =>
                f.Maand == context.FenotypeMulti.Where(m => m.Eigenschap == "blad-max")
                    .FirstOrDefault(i => i.PlantId == id).Maand);
        }
        public FenoMaand GetFenoMaxBloeiHoogteMaand(long id)
        {
            return context.FenoMaand.FirstOrDefault(f =>
                f.Maand == context.FenotypeMulti.Where(m => m.Eigenschap == "bloei-max")
                    .FirstOrDefault(i => i.PlantId == id).Maand);
        }
        public FenoMaand GetFenoMinBloeiHoogteMaand(long id)
        {
            return context.FenoMaand.FirstOrDefault(f =>
                f.Maand == context.FenotypeMulti.Where(m => m.Eigenschap == "bloei-min")
                    .FirstOrDefault(i => i.PlantId == id).Maand);
        }
        public FenoKleur GetFenoBladKleur(long id)
        {
            return context.FenoKleur.FirstOrDefault(f =>
                f.NaamKleur == context.FenotypeMulti.Where(m => m.Eigenschap == "blad")
                    .FirstOrDefault(i => i.PlantId == id).Waarde);
        }
        public FenoMaand GetFenoBladMaand(long id)
        {
            return context.FenoMaand.FirstOrDefault(f =>
                f.Maand == context.FenotypeMulti.Where(m => m.Eigenschap == "blad")
                    .FirstOrDefault(i => i.PlantId == id).Maand);
        }
        public FenoKleur GetFenoBloeiKleur(long id)
        {
            return context.FenoKleur.FirstOrDefault(f =>
                f.NaamKleur == context.FenotypeMulti.Where(m => m.Eigenschap == "bloei")
                    .FirstOrDefault(i => i.PlantId == id).Waarde);
        }
        public FenoMaand GetFenoBloeiMaand(long id)
        {
            return context.FenoMaand.FirstOrDefault(f =>
                f.Maand == context.FenotypeMulti.Where(m => m.Eigenschap == "bloei")
                    .FirstOrDefault(i => i.PlantId == id).Maand);
        }
        public List<FenoBloeiwijze> GetFenoBloeiwijze()
        {
            return context.FenoBloeiwijze.ToList();
        }
        public List<FenoHabitus> GetFenoHabitus()
        {
            return context.FenoHabitus.ToList();
        }
        public List<FenoBladgrootte> GetFenoBladgrootte()
        {
            return context.FenoBladgrootte.ToList();
        }
        public List<FenoKleur> GetFenoKleur()
        {
            return context.FenoKleur.ToList();
        }
        public List<FenoMaand> GetFenoMaand()
        {
            return context.FenoMaand.ToList();
        }
        public List<FenoBladvorm> GetFenoBladvorm()
        {
            return context.FenoBladvorm.ToList();
        }
        public List<FenoRatioBloeiBlad> GetFenoRatio()
        {
            return context.FenoRatioBloeiBlad.ToList();
        }
        public List<FenoSpruitfenologie> GetFenoSpruit()
        {
            return context.FenoSpruitfenologie.ToList();
        }
        public List<FenoLevensvorm> GetFenoLevensvorm()
        {
            return context.FenoLevensvorm.ToList();
        }

        //Abio
        public List<AbioBezonning> GetAbioBezonning()
        {
            return context.AbioBezonning.ToList();
        }
        public List<AbioGrondsoort> GetAbioGrondsoort()
        {
            return context.AbioGrondsoort.ToList();
        }
        public List<AbioVoedingsbehoefte> GetAbioVoedingsbehoefte()
        {
            return context.AbioVoedingsbehoefte.ToList();
        }
        public List<AbioVochtbehoefte> GetAbioVochtbehoefte()
        {
            return context.AbioVochtbehoefte.ToList();
        }
        public List<AbioReactieAntagonischeOmg> GetAbioReactieAntagonischeOmg()
        {
            return context.AbioReactieAntagonischeOmg.ToList();
        }
        public List<AbioHabitat> GetHabitats()
        {
            return context.AbioHabitat.ToList();
        }
        //Commersialisme
        //Extra Eigenschappen
        //Beheer Eigenschappen
    }
}