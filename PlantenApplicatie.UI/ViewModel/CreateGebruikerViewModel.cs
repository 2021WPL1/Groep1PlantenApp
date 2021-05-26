using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;

namespace PlantenApplicatie.UI.ViewModel
{
    //Maarten & Hemen 
    class CreateGebruikerViewModel : ViewModelBase
    {
        private PlantenDataService _dataservice;
        public ICommand addGebruikerCommand { get; set; }
        public ObservableCollection<Rol> Rollen { get; set; }
        private string emailInput;
        private string wachtwoordInput;
        private string wachtwoordBevestigen;
        private Rol _selectedRol;
        private string _error;

        //Hemen &maarten 
        public CreateGebruikerViewModel(PlantenDataService plantenDataService)
        {
            addGebruikerCommand = new DelegateCommand(addGebruiker);
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
        [Required(ErrorMessage = " Password is required")]
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
        [Required(ErrorMessage = "Confirm Password is required")]
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
        public void addGebruiker()
        {
            try
            {
                if (WachtwoordBevestigen == WachtwoordInput)
                {
                    using (var sha256 = SHA256.Create())
                    {
                        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(WachtwoordInput));
                        _dataservice.addGebruiker(SelectedRol.Omschrijving, EmailInput, hashedBytes);
                    }

                }
                else
                {
                    SelectedError = "wachtwoord is niet zelfde";
                }
            }
            catch (Exception)
            {

                SelectedError = "er is iets fout";
            }
          
          
        }
    }
}
