using GalaSoft.MvvmLight.Command;
using PlantenApplicatie.Data;
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
        private static Deserialize _deserialize = new Deserialize();
        //Eigen api key voor mails te sturen via sendgrid
        private static SMTPMailMessage sMTPMailMessage = new SMTPMailMessage("apikey",
            "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",
            "smtp.sendgrid.net");
        public RelayCommand<Window> CloseResultCommand { get; set; }

        public WachtwoordViewModel(PlantenDataService plantenDataService)
        {
            this.CloseResultCommand = new RelayCommand<Window>(this.CloseWindow);
        }

        public void CloseWindow(Window window)
        {
            window.Close();
        }

        static void SendSubscriptionConfirmation<T>(string personName, string personEmail, string htmlPath, string htmlFileName)
        {
            string html = File.ReadAllText(htmlPath);
            string body = String.Format(html, personName, personEmail);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Sending mail via Sendgrif SMTP Relay {personEmail}");
            var msg = sMTPMailMessage.CreateMail(personEmail, body, "Subscription");
            var result = sMTPMailMessage.sendMessage(msg);
            if (result.Status == MailSendingStatus.OK)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Message send to {personEmail}");
                return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(result.Message);
                return;
            }


        }
    }
}
