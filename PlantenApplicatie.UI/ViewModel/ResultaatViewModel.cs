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
        //Jelle & Hemen
        //Plant voor in labels
        public Plant _plantenResultaat;
        //Command maken voor form te sluiten
        public RelayCommand<Window> CloseResultCommand { get; private set; }
        public ResultaatViewModel(PlantenDataService plantenDataService)
        {//DAO

            PlantenResultaat = new Plant();
            this.CloseResultCommand = new RelayCommand<Window>(this.CloseResult);
        }


        //Jelle & Hemen
        //Command die gelinkt is aan close button om form te sluiten
        public void CloseResult(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }


        //Command om labels op te vullen
        public void fillLabels(Plant plant)
        {
            PlantenResultaat = plant;
        }

        //Geeft de data van de plant door
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
