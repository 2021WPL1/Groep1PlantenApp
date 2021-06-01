using GalaSoft.MvvmLight.Command;
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using PlantenApplicatie.UI.View;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PlantenApplicatie.UI.ViewModel

{
    public class GebruikersBeheerViewModel : ViewModelBase
    {
       public RelayCommand<Window> schermGebruikerToevoegenCommand { get; set; }
        public RelayCommand<Window> CloseGebruikersBeheerCommand { get; set; }




        public GebruikersBeheerViewModel(PlantenDataService plantenDataService)
        {//DAO instantiëren
    

            //Hemen en Maarten
            schermGebruikerToevoegenCommand = new RelayCommand<Window>(this.SchermGebruikerToevoegenCommand);
            CloseGebruikersBeheerCommand = new RelayCommand<Window>(this.CloseGebruikersBeheer);


        }

        private void CloseGebruikersBeheer(Window window)
        {
            MainWindow main = new MainWindow();
            window.Close();
            main.ShowDialog();
        }

        //Hemen en Maarten
        private void SchermGebruikerToevoegenCommand(Window window)
        {
            CreateGebruiker create = new CreateGebruiker();

            window.Close();
            create.ShowDialog();

        }

    }
}