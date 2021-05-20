using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using PlantenApplicatie.Data;
using Prism.Commands;

namespace PlantenApplicatie.UI.ViewModel
{
    public class EditViewModel :ViewModelBase
    {
        private PlantenDataService _plantenDataService;

        public ICommand OpslaanCommand { get; set; }

        public ObservableCollection<bool> BezonningSchaduw { get; set; }
        public ObservableCollection<bool> BezonningZon { get; set; }
        public ObservableCollection<bool> BezonningHalfSchaduw { get; set; }
        public ObservableCollection<bool> BezonningHalfZon { get; set; }

        private object[] _listBezonning;

        public EditViewModel(PlantenDataService plantenDataService)
        {
            this._plantenDataService = plantenDataService;

            OpslaanCommand = new DelegateCommand(Opslaan);

            BezonningSchaduw=new ObservableCollection<bool>();
            BezonningZon = new ObservableCollection<bool>();
            BezonningHalfSchaduw = new ObservableCollection<bool>();
            BezonningHalfZon = new ObservableCollection<bool>();

        }

        private void Opslaan()
        {
            string result = "";
            string[] listbezonning = new[] {"Schaduw", "Zon", "Half-Schaduw", "Half-Zon"};

            if (BezonningSchaduw.First())
            {
                result += "Schaduw - ";
            }
            if (BezonningZon.First())
            {
                result += "Zon - ";
            }
            if (BezonningHalfSchaduw.First())
            {
                result += "Half Schaduw - ";
            }
            if (BezonningHalfZon.First())
            {
                result += "Half Zon - ";
            }


            result = result.Substring(0, result.Length - 3);

            
        }
    }
}
