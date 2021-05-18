using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}