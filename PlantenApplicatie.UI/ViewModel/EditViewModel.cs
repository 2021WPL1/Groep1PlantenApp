using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using Prism.Commands;

namespace PlantenApplicatie.UI.ViewModel
{
    public class EditViewModel :ViewModelBase
    {
        private PlantenDataService _plantenDataService;

        public ICommand OpslaanCommand { get; set; }

        public ObservableCollection<AbioBezonning> Bezonning { get; set; }

        private AbioBezonning _selectedBezonning;

        public EditViewModel(PlantenDataService plantenDataService)
        {
            this._plantenDataService = plantenDataService;

            OpslaanCommand = new DelegateCommand(Opslaan);

            Bezonning = new ObservableCollection<AbioBezonning>();
        }

        private void Opslaan()
        {
            
        }

        public AbioBezonning SelectedBezonning
        {
            get { return _selectedBezonning; }
            set
            {
                _selectedBezonning = value;
                OnPropertyChanged();
            }
        }
    }
}
