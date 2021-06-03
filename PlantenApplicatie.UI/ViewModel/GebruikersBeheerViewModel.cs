using GalaSoft.MvvmLight.Command;
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using PlantenApplicatie.UI.View;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PlantenApplicatie.UI.ViewModel

{
    public class GebruikersBeheerViewModel : ViewModelBase
    {
        public ObservableCollection<Gebruiker> Users { get; set; }
        private PlantenDataService _plantenDataService;
        public ICommand GebruikerVerwijderenCommand { get; set; }
        public RelayCommand<Window> schermGebruikerToevoegenCommand { get; set; }
        public RelayCommand<Window> CloseGebruikersBeheerCommand { get; set; }
        private Gebruiker _selectedGebruiker;

        //Jelle
        public Gebruiker LoggedInGebruiker { get; set; }
        public void LoadLoggedInUser(Gebruiker gebruiker)
        {
            LoggedInGebruiker = gebruiker;
        }
        public Visibility RolButtonsVisibility { get; set; }
        public void EnableRolButtons()
        {
            switch (LoggedInGebruiker.Rol)
            {
                case "Gebruiker":
                    RolButtonsVisibility = Visibility.Hidden;
                    break;
                case "Data-collector":
                    RolButtonsVisibility = Visibility.Hidden;
                    break;
                case "Manager":
                    RolButtonsVisibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        public GebruikersBeheerViewModel(PlantenDataService plantenDataService)
        {//DAO instantiëren
    

            //Hemen en Maarten
            schermGebruikerToevoegenCommand = new RelayCommand<Window>(this.SchermGebruikerToevoegenCommand);
            CloseGebruikersBeheerCommand = new RelayCommand<Window>(this.CloseGebruikersBeheer);
            GebruikerVerwijderenCommand = new DelegateCommand(VerwijderGebruiker);
            Users = new ObservableCollection<Gebruiker>();
            this._plantenDataService = plantenDataService;


        }
        public Gebruiker SelectedGebruiker
        {
            get { return _selectedGebruiker; }
            set
            {
                _selectedGebruiker = value;
                OnPropertyChanged();
            }
        }

        private void VerwijderGebruiker()
        {
            if (SelectedGebruiker != null)
            {
                var gebruiker = _plantenDataService.verwijderGebruiker(SelectedGebruiker.Emailadres);
                ShowAllUser();
            }
        }

        private void CloseGebruikersBeheer(Window window)
        {
            MainWindow main = new MainWindow(LoggedInGebruiker);
            window.Close();
            main.ShowDialog();
        }

        //Hemen en Maarten
        private void SchermGebruikerToevoegenCommand(Window window)
        {
            CreateGebruiker create = new CreateGebruiker(LoggedInGebruiker);
            
            window.Close();
            create.ShowDialog();
            
        }
        public  void ShowAllUser()
        {
            Users.Clear();
            var Gebruikers = _plantenDataService.GetAllUsers();
            foreach (var gebruiker in Gebruikers)
            {
                Users.Add(gebruiker);
            }

        }

    }
}