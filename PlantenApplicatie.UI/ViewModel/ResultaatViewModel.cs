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
        public Plant PlantenResultaat;
        public ResultaatViewModel(PlantenDataService plantenDataService)
        {
            PlantenResultaat = new Plant();
        }
        public void fillLabels(Plant plant)
        {
            PlantenResultaat = plant;
        }
    }
}
