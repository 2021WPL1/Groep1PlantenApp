using PlantenApplicatie.Data;
using PlantenApplicatie.UI.View;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;

namespace PlantenApplicatie.UI.ViewModel
{
    class LoginSchermViewModel : ViewModelBase
    {

        //Maarten &Hemen 
        public ICommand LoginCommand { get; set; }
        private PlantenDataService _plantenDataService;
        private string _emailInput;
        private string _wachtwoordInput;
        private string _selectedError;
        public LoginSchermViewModel(PlantenDataService plantenDataService)
        {
            LoginCommand = new DelegateCommand(LogIn);
            this._plantenDataService = plantenDataService;
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
        private void LogIn()
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
                                MainWindow window = new MainWindow();
                                window.ShowDialog();
                            }
                            else
                            {
                                SelectedError = "Wachtwoord is onjuist";
                            }
                        }
                    }
                }            
            }
            catch (Exception e)
            {
                SelectedError = "er is iets mis. Check uw paswoord of emailadres";
            }
        }
    }
}
