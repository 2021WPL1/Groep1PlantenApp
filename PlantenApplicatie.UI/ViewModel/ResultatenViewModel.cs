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

        public Fenotype Fenotype
        {
            get { return _fenotype; }
            set
            {
                _fenotype = value;
            }
        }

        public Abiotiek Abiotiek
        {
            get { return _abiotiek; }
            set
            {
                _abiotiek = value;
            }
        }

        public Commensalisme Commensalisme
        {
            get { return _commensalisme; }
            set
            {
                _commensalisme = value;
            }

        }

        public ExtraEigenschap ExtraEigenschap
        {
            get { return _extraEigenschap; }
            set
            {
                _extraEigenschap = value;
            }
        }

        public BeheerMaand BeheerMaand
        {
            get { return _beheerMaand; }
            set
            {
                _beheerMaand = value;
            }
        }
        
        public Plant PlantenResultaat
        {
            get { return _plantenResultaat; }
            set
            {
                _plantenResultaat = value;
            }
        }

        //Geeft elke keer de gevraagde informatie per opgezochte plant
        public void GetPlant()
        {
            PlantenResultaat = _plantenDataService.GetPlantWithId(3);
        }

        public void GetFenotype()
        {
            Fenotype = _plantenDataService.GetFenotype(3);
        }

        public void GetAbiotiek()
        {
            Abiotiek = _plantenDataService.GetAbiotiek(3);
        }

        public void GetCommensalisme()
        {
            Commensalisme = _plantenDataService.GetCommensalisme(3);
        }

        public void GetExtraEigenschap()
        {
            ExtraEigenschap = _plantenDataService.GetExtraEigenschap(3);
        }

        public void GetBeheerMaand()
        {
            BeheerMaand = _plantenDataService.GetBeheerMaand(3);
        }
    }
}
