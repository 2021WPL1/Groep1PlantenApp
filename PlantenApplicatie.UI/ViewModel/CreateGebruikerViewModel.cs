using GalaSoft.MvvmLight.Command;
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using PlantenApplicatie.UI.View;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PlantenApplicatie.UI.ViewModel
{
    //Maarten & Hemen 
    public class CreateGebruikerViewModel : ViewModelBase
    {
        public RelayCommand<Window> addGebruikerCommand { get; set; }
        public RelayCommand<Window> closeAddGebruikerCommand { get; set; }
       
        private PlantenDataService _dataservice;
        public ObservableCollection<Rol> Rollen { get; set; }
        private string _emailInput;
        private string _wachtwoordInput;
        private string _wachtwoordBevestigen;
        private string _voornaam;
        private string _achternaam;
        private string _vivesnr;
        private Rol _selectedRol;
        private string _error;

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

        //Hemen &maarten 
        public CreateGebruikerViewModel(PlantenDataService plantenDataService)
        {
            closeAddGebruikerCommand = new RelayCommand<Window>(this.closeAddGebruiker);
            addGebruikerCommand = new RelayCommand<Window>(this.addGebruiker);
            Rollen = new ObservableCollection<Rol>();
            this._dataservice = plantenDataService;
        }
        public void addRollen()
        {
            var rollen = _dataservice.GetRollen();
            foreach (var rol in rollen)
            {
                Rollen.Add(rol);
            }

        }
      
     
        public Rol SelectedRol
        {
            get { return _selectedRol; }
            set
            {
                _selectedRol = value;
                OnPropertyChanged();
            }
        }
        [Required]
        [EmailAddress]
        public string EmailInput
        {
            get
            {
                return _emailInput;
            }
            set
            {
                _emailInput = value;
                OnPropertyChanged();
            }
        }
        public string WachtwoordInput
        {
            get
            {
                return _wachtwoordInput;
            }
            set
            {
                _wachtwoordInput = value;
                OnPropertyChanged();
            }
        }
        public string WachtwoordBevestigen
        {
            get { return _wachtwoordBevestigen; }
            set { _wachtwoordBevestigen = value; }
        }
        public string VoorNaamInput
        {
            get
            {
                return _voornaam;
            }
            set
            {
                _voornaam = value;
                OnPropertyChanged();
            }
        }

        public string AchterNaamInput
        {
            get
            {
                return _achternaam;
            }
            set
            {
                _achternaam = value;
                OnPropertyChanged();
            }
        }

        public string VivesNrInput
        {
            get
            {
                return _vivesnr;
            }
            set
            {
                _vivesnr = value;
                OnPropertyChanged();
            }
        }
        public string SelectedError
        {
            get { return _error; }
            set
            {
                _error = value;
                OnPropertyChanged();
            }
        }
        private void closeAddGebruiker(Window window)
        {
            GebruikersBeheer beheer = new GebruikersBeheer(LoggedInGebruiker);
            window.Close();
            beheer.ShowDialog();
            
        }
        //hemen & maarten 
        public void addGebruiker(Window closeWindow)
        {
            try
            {
                if (EmailInput.Contains("vives.be") && EmailInput.Contains("@") && VoorNaamInput != null && AchterNaamInput != null && VivesNrInput != null)
                {
                    var gebruiker = _dataservice.getGebruikerViaEmail(EmailInput);
                    if (gebruiker == null)
                    {
                        if (WachtwoordBevestigen == WachtwoordInput)
                        {
                            using (var sha256 = SHA256.Create())
                            {
                                GebruikersBeheer beheer = new GebruikersBeheer(LoggedInGebruiker);
                                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(WachtwoordInput));
                                _dataservice.addGebruiker(SelectedRol.Omschrijving, EmailInput, hashedBytes, VoorNaamInput, AchterNaamInput, VivesNrInput);
                                closeWindow.Close();
                                beheer.ShowDialog();
                            }
                        }
                        else
                        {
                            SelectedError = "wachtwoord is niet hetzelfde";
                        }
                    }
                    else
                    {
                        SelectedError = "Het emailadres bestaat al";
                    }


                }
                else
                {
                    SelectedError = "emailadres moet 'vives.be' bevatten";
                }
            }
            catch (Exception)
            {



                SelectedError = "oei, er is iets fout";
            }
        }
    }
}
