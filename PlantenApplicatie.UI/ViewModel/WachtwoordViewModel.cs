using GalaSoft.MvvmLight.Command;
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using PlantenApplicatie.UI.MailService.Classes;
using PlantenApplicatie.UI.MailService.Enums;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PlantenApplicatie.UI.ViewModel
{
    public class WachtwoordViewModel : ViewModelBase
    {
        //Eigen api key voor mails te sturen via sendgrid
        private static SMTPMailMessage sMTPMailMessage = new SMTPMailMessage();
        /*private static SMTPMailMessage sMTPMailMessage = new SMTPMailMessage("gunnar.fritsch31@ethereal.email",
            "9kSgGREuC3rf6N9PxJ",
            "smtp.ethereal.email");*/
        private PlantenDataService _plantenDataService;
        public RelayCommand<Window> CloseResultCommand { get; set; }
        public ICommand MailCodeSending { get; set; }
        public ICommand CodeChecking { get; set; }
        public RelayCommand<Window> PasswordChecking { get; set; }

        private bool _emailEnabled = true;
        private bool _codeEnabled = false;
        private bool _passwordEnabled = false;


        public bool EmailEnabled
        {
            get
            {
                return _emailEnabled;
            }
            set
            {
                if (_emailEnabled != value)
                {
                    _emailEnabled = value;
                    OnPropertyChanged("EmailEnabled");
                }
            }
        }
        public bool CodeEnabled
        {
            get
            {
                return _codeEnabled;
            }
            set
            {
                if (_codeEnabled != value)
                {
                    _codeEnabled = value;
                    OnPropertyChanged("CodeEnabled");
                }
            }
        }
        public bool PasswordEnabled
        {
            get
            {
                return _passwordEnabled;
            }
            set
            {
                if (_passwordEnabled != value)
                {
                    _passwordEnabled = value;
                    OnPropertyChanged("PasswordEnabled");
                }
            }
        }

        private string _emailInput;
        private string _codeInput;
        private string _passwordInput1;
        private string _passwordInput2;

        private string _code;

        public WachtwoordViewModel(PlantenDataService plantenDataService)
        {
            this.CloseResultCommand = new RelayCommand<Window>(this.CloseWindow);
            MailCodeSending = new DelegateCommand(SendMail);
            CodeChecking = new DelegateCommand(EnableNewPassword);
            this.PasswordChecking = new RelayCommand<Window>(this.CreateNewPassword);
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
        public string CodeInput
        {
            get { return _codeInput; }
            set
            {
                _codeInput = value;
            }
        }
        public string PasswordInput1
        {
            get { return _passwordInput1; }
            set
            {
                _passwordInput1 = value;
            }
        }
        public string PasswordInput2
        {
            get { return _passwordInput2; }
            set
            {
                _passwordInput2 = value;
            }
        }

        public void CloseWindow(Window window)
        {
            window.Close();
        }

        public void SendMail()
        {
            if (_plantenDataService.getGebruikerViaEmail(EmailInput) != null)
            {
                if (_code != null) { _code = null;}
                Random r = new Random();
                for (int i = 0; i < 9; i++)
                {
                    _code += r.Next(0, 9).ToString();
                }
                //string fileName = "MailMessage.html";
                //string path = Path.Combine(Environment.CurrentDirectory, fileName);
                //string html = File.ReadAllText(path);
                string body = String.Format($"Beste Meneer / Mevrouw,\r\nUw code voor uw wachtwoord te resetten is {_code}.\r\n Vriendlijke groeten, Plantify");
                var msg = sMTPMailMessage.CreateMail(EmailInput, body, "Wachtwoord reset");
                var result = sMTPMailMessage.sendMessage(msg);
                if (result.Status == MailSendingStatus.OK)
                {
                    MessageBox.Show($"Message send to {EmailInput}");
                    EmailEnabled = false;
                    CodeEnabled = true;
                    return;
                }
                else
                {
                    MessageBox.Show(result.Message);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Deze mail bestaat niet in onze database.");
            }
        }

        public void EnableNewPassword()
        {
            if (CodeInput == _code)
            {
                MessageBox.Show("De code is geaccepteerd, gelieve uw nieuw wachtwoord in te vullen.");
                CodeEnabled = false;
                PasswordEnabled = true;
            }
            else
            {
                MessageBox.Show("Dit is niet de juiste code");
            }
        }

        public void CreateNewPassword(Window window)
        {
            if (PasswordInput1 != null && PasswordInput1 != "" && PasswordInput2 != null && PasswordInput2 != null)
            {
                if (PasswordInput1 == PasswordInput2)
                {
                    var gebruiker = _plantenDataService.getGebruikerViaEmail(EmailInput);
                    using (var sha256 = SHA256.Create())
                    {
                        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(PasswordInput1));
                        if (!hashedBytes.SequenceEqual(gebruiker.HashPaswoord))
                        {
                            _plantenDataService.ChangeGebruikerPassword(gebruiker, hashedBytes);
                            MessageBox.Show("Uw wachtwoord is gewijzigd.");
                            window.Close();
                        }
                        else
                        {
                            MessageBox.Show("Dit is het oude wachtwoord.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("De wachtwoorden zijn niet hetzelfde.");
                }
            }
            else
            {
                MessageBox.Show("Gelieve beide textboxen in te vullen.");
            }
        }
    }
}
