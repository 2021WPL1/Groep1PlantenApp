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

        //Jelle
        //Maken van gebruiker
        public Gebruiker LoggedInGebruiker { get; set; }
        //Jelle
        //functie om gebruiker info te geven om te gebruiken doorheen de viewmodel
        public void LoadLoggedInUser(Gebruiker gebruiker)
        {
            LoggedInGebruiker = gebruiker;
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
     
        public Rol SelectedRol { get; set; }
        [Required]
        [EmailAddress]
        public string EmailInput { get; set; }
        public string WachtwoordInput { get; set; }
        public string WachtwoordBevestigen { get; set; }
        public string VoorNaamInput { get; set; }

        public string AchterNaamInput { get; set; }

        public string VivesNrInput { get; set; }
        private string _selectedError;
        public string SelectedError 
        { get { return _selectedError; }
            set
            {
                _selectedError = value;
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
                if (EmailInput != null && VoorNaamInput != null && AchterNaamInput != null && VivesNrInput != null)
                {
                    if (EmailInput.Contains("vives.be") && EmailInput.Contains("@"))
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
                        SelectedError = "Het emailadres moet 'vives.be' bevatten";
                    }
                }
                else
                {
                    SelectedError = "Er zijn velden niet ingevuld";
                }
            }
            catch (Exception)
            {
                SelectedError = "oei, er is iets fout";
            }
        }
    }
}
