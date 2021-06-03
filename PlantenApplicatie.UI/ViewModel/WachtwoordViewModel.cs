using GalaSoft.MvvmLight.Command;
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using PlantenApplicatie.UI.MailService;
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
    //Jelle & Stephanie
    public class WachtwoordViewModel : ViewModelBase
    {
        //Eigen api key om mails te sturen via Ethereal
        private SMTPMailService sMTPMailService = new SMTPMailService("gunnar.fritsch31@ethereal.email",
            "9kSgGREuC3rf6N9PxJ",
            "smtp.ethereal.email");
        //Link naar databank om uiteindelijk het wachtwoord aan te passen
        private PlantenDataService _plantenDataService;
        //command gelinkt naar gui om venster te sluiten
        public RelayCommand<Window> CloseResultCommand { get; set; }
        //Command om boxes van mail aan en uit te leggen
        public ICommand MailCodeSending { get; set; }
        //Command om boxes van code aan en uit te leggen
        public ICommand CodeChecking { get; set; }
        //Command om boxes van wachtwoord aan en uit te leggen
        public RelayCommand<Window> PasswordChecking { get; set; }
        //Startwaardes
        private bool _emailEnabled = true;
        private bool _codeEnabled = false;
        private bool _passwordEnabled = false;

        //Kijkt of email boxes aan of uit moeten zijn
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
        //Kijkt of code boxes aan of uit moeten zijn
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
        //Kijkt of wachtwoord boxes aan of uit moeten zijn
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


        //constructor
        public WachtwoordViewModel(PlantenDataService plantenDataService)
        {
            this.CloseResultCommand = new RelayCommand<Window>(this.CloseWindow);
            MailCodeSending = new DelegateCommand(SendMail);
            CodeChecking = new DelegateCommand(EnableNewPassword);
            this.PasswordChecking = new RelayCommand<Window>(this.CreateNewPassword);
            this._plantenDataService = plantenDataService;
        }

        //Email properties een waarde geven
        public string EmailInput { get; set; }
        public string CodeInput { get; set; }
        public string PasswordInput1 { get; set; }
        public string PasswordInput2 { get; set; }

        //venster sluiten
        public void CloseWindow(Window window)
        {
            window.Close();
        }

        public void SendMail()
        {
            if (_plantenDataService.getGebruikerViaEmail(EmailInput) != null)
            {
                if (CodeInput != null) { CodeInput = null;}
                Random r = new Random();
                for (int i = 0; i < 9; i++)
                {
                    CodeInput += r.Next(0, 9).ToString();
                }
                string fileName = "MailMessage.html";
                string path = Environment.CurrentDirectory.Replace("\\bin\\Debug\\netcoreapp3.1", "") + $"\\MailService\\Files\\{fileName}";
                string html = File.ReadAllText(path);
                string body = String.Format(html, CodeInput);
                var msg = sMTPMailService.CreateMail(EmailInput, body, "Wachtwoord reset");
                var result = sMTPMailService.sendMessage(msg);
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
            if (CodeInput == CodeInput)
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

        //Wachtwoord check voor het wijzigen van het "vergeten" wachtwoord
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
