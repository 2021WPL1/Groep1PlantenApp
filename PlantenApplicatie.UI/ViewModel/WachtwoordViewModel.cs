using GalaSoft.MvvmLight.Command;
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using PlantenApplicatie.UI.MailService.Classes;
using PlantenApplicatie.UI.MailService.Enums;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PlantenApplicatie.UI.ViewModel
{
    public class WachtwoordViewModel : ViewModelBase
    {
        //Eigen api key voor mails te sturen via sendgrid
        private static SMTPMailMessage sMTPMailMessage = new SMTPMailMessage("apikey",
            "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",
            "smtp.sendgrid.net");
        public RelayCommand<Window> CloseResultCommand { get; set; }
        public ICommand MailCodeSending { get; set; }

        public WachtwoordViewModel(PlantenDataService plantenDataService)
        {
            this.CloseResultCommand = new RelayCommand<Window>(this.CloseWindow);
            MailCodeSending = new DelegateCommand(SendMail);
        }

        public void CloseWindow(Window window)
        {
            window.Close();
        }

        static void SendMail()
        {
            string mail = "jelle.dispersyn@student.vives.be";
            Random r = new Random();
            string code = string.Empty;
            for (int i = 0; i < 9; i++)
            {
                code += r.Next(0, 9).ToString();
            }
            string html = File.ReadAllText(@"D:\Vives\Kwartaal 3\Werkplekleren1\PlantenApp\PlantenApplicatie.UI\MailService\Files\MailMessage.html");
            string body = String.Format(html, code);
            var msg = sMTPMailMessage.CreateMail(mail, body, "Wachtwoord reset");
            var result = sMTPMailMessage.sendMessage(msg);
            if (result.Status == MailSendingStatus.OK)
            {
                MessageBox.Show($"Message send to {mail}");
                return;
            }
            else
            {
                MessageBox.Show(result.Message);
                return;
            }


        }
    }
}
