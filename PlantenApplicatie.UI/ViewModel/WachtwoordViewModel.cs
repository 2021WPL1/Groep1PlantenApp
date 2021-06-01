using GalaSoft.MvvmLight.Command;
using PlantenApplicatie.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace PlantenApplicatie.UI.ViewModel
{
    public class WachtwoordViewModel : ViewModelBase
    {
        public RelayCommand<Window> CloseResultCommand { get; set; }
        public WachtwoordViewModel(PlantenDataService plantenDataService)
        {
            this.CloseResultCommand = new RelayCommand<Window>(this.CloseWindow);
        }

        public void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
