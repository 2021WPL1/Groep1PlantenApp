using GalaSoft.MvvmLight.Command;
using PlantenApplicatie.Data;
using PlantenApplicatie.UI.View;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PlantenApplicatie.UI.ViewModel
{
    class LoginSchermViewModel : ViewModelBase
    {

        //Maarten &Hemen 
        public RelayCommand<Window> CloseWindowCommand { get; set; }

        public ICommand OpenWachtwoordVergetenWindow { get; set; }

        private PlantenDataService _plantenDataService;
        private string _emailInput;
        private string _wachtwoordInput;
        private string _selectedError;
        public LoginSchermViewModel(PlantenDataService plantenDataService)
        {
            this.CloseWindowCommand = new RelayCommand<Window>(this.CloseWindow);
            OpenWachtwoordVergetenWindow = new DelegateCommand(WachtwoordVergetenScherm);
            this._plantenDataService = plantenDataService;
        }

        public void WachtwoordVergetenScherm()
        {
            WachtwoordVergetenWindow window = new WachtwoordVergetenWindow();
            window.ShowDialog();
        }

        public string EmailInput
        {
            get { return _emailInput; }
            set
            {
                _emailInput = value.Trim();
            }
        }
        public string WachtwoordInput
        {
            get { return _wachtwoordInput; }
            set
            {
                _wachtwoordInput = value;
            }
        }
        public string SelectedError
        {
            get { return _selectedError; }
            set
            {
                _selectedError = value;
                OnPropertyChanged();
            }
        }
        //Maarten &Hemen 
        private void CloseWindow(Window windowClose)
        {
            try
            {

                if (EmailInput != null && WachtwoordInput != null)
                {
                    using (var sha256 = SHA256.Create())
                    {
                        var gebruiker = _plantenDataService.getGebruikerViaEmail(EmailInput);
                        if (gebruiker != null)
                        {
                            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(WachtwoordInput));
                            if (hashedBytes.SequenceEqual(gebruiker.HashPaswoord))
                            {
                                MainWindow window = new MainWindow(gebruiker);
                                windowClose.Close();
                                window.ShowDialog();              
                            }
                            else
                            {
                                SelectedError = "Wachtwoord is onjuist";
                            }
                        }
                        else
                        {
                            SelectedError = "Emailadres of wachtwoord is niet gekend";
                        }
                    }
                }
                else
                {
                    SelectedError = "Er is een veld niet ingevuld";
                }
            }
            catch (Exception e)
            {
                SelectedError = "er is iets mis. Check uw paswoord of emailadres";
            }
        }
    }
}
