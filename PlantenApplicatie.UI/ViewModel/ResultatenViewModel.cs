using System;
using System.Collections.Generic;
using System.Text;
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;

namespace PlantenApplicatie.UI.ViewModel
{

    //Maarten & Stephanie
    class ResultatenViewModel : ViewModelBase
    {
        private PlantenDataService _plantenDataService;
        private Plant _plantenResultaat;
        private Fenotype _fenotype;
        private Abiotiek _abiotiek;
        private Commensalisme _commensalisme;
        private ExtraEigenschap _extraEigenschap;
        private BeheerMaand _beheerMaand;

        public ResultatenViewModel(PlantenDataService plantenDataService)
        {
            this._plantenDataService = plantenDataService;
        }

        public Plant PlantenResultaat
        {
            get { return _plantenResultaat; }
            set
            {
                _plantenResultaat = value;
            }
        }

        public void GetPlant()
        {
            PlantenResultaat = _plantenDataService.GetPlantWithId(3);
        }

    }
}
