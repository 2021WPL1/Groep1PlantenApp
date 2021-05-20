using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using Prism.Commands;

namespace PlantenApplicatie.UI.ViewModel
{
    class ResultaatViewModel : ViewModelBase
    {
        public Plant _plantenResultaat;
        public ResultaatViewModel(PlantenDataService plantenDataService)
        {
            PlantenResultaat = new Plant();
        }
        public void fillLabels(Plant plant)
        {
            PlantenResultaat = plant;
        }
        public Plant PlantenResultaat
        {
            get { return _plantenResultaat; }
            set
            {
                _plantenResultaat = value;
            }
        }
    }
}
