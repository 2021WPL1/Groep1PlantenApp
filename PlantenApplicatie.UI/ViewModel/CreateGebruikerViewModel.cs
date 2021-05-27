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
    class CreateGebruikerViewModel : ViewModelBase
    {
        public RelayCommand<Window> addGebruikerCommand { get; set; }
        private PlantenDataService _dataservice;
        public ObservableCollection<Rol> Rollen { get; set; }
        private string emailInput;
        private string wachtwoordInput;
        private string wachtwoordBevestigen;
        private Rol _selectedRol;
        private string _error;

        //Hemen &maarten 
        public CreateGebruikerViewModel(PlantenDataService plantenDataService)
        {
            this.addGebruikerCommand = new RelayCommand<Window>(this.addGebruiker);
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
                return emailInput;
            }
            set
            {
                emailInput = value;
                OnPropertyChanged();
            }
        }
        public string WachtwoordInput
        {
            get
            {
                return wachtwoordInput;
            }
            set
            {
                wachtwoordInput = value;
                OnPropertyChanged();
            }
        }
        public string WachtwoordBevestigen
        {
            get { return wachtwoordBevestigen; }
            set { wachtwoordBevestigen = value; }
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
        //hemen & maarten 
        public void addGebruiker(Window closeWindow)
        {
            try
            {
                if (EmailInput.Contains("vives.be")&&EmailInput.Contains("@"))
                {
                    if (WachtwoordBevestigen == WachtwoordInput)
                    {
                        using (var sha256 = SHA256.Create())
                        {   
                            MainWindow window = new MainWindow();
                            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(WachtwoordInput));
                            _dataservice.addGebruiker(SelectedRol.Omschrijving, EmailInput, hashedBytes);
                            closeWindow.Close();
                            window.ShowDialog();
                        }
                    }
                    else
                    {
                        SelectedError = "wachtwoord is niet hetzelfde";
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
