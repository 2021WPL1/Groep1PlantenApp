using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using PlantenApplicatie.UI.View;
using Prism.Commands;

namespace PlantenApplicatie.UI.ViewModel
{
    class ResultaatViewModel : ViewModelBase
    {
        public Plant _plantenResultaat;
        public RelayCommand<Window> CloseResultCommand { get; private set; }
        public ResultaatViewModel(PlantenDataService plantenDataService)
        {
            PlantenResultaat = new Plant();
            this.CloseResultCommand = new RelayCommand<Window>(this.CloseResult);
        }

        public void CloseResult(Window window)
        {
            window.ShowDialog();
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
