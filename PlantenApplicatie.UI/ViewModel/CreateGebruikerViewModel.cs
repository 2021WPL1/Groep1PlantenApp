using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private Rol _selectedRol;

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
        //hemen & maarten 
        public void addGebruiker()
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(WachtwoordInput));
                _dataservice.addGebruiker(SelectedRol.Omschrijving, EmailInput, hashedBytes);
            }
        }
    }
}
