using PlantenApplicatie.UI.MailService.Data;
using PlantenApplicatie.UI.MailService.Enums;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace PlantenApplicatie.UI.MailService.Classes
{
    //Jelle & Stephanie
    public class SMTPMailMessage
    {
        private SmtpClient smtpClient { get; set; }
        public SMTPMailMessage()
        {
            smtpClient = new SmtpClient();
            //De Ethereal server is een beveiligde server; dus er moet worden ingelogd
            //daarvoor zijn de credentials nodig om de gebruiker te authenticeren
            smtpClient.Credentials = new NetworkCredential("gunnar.fritsch31@ethereal.email", "9kSgGREuC3rf6N9PxJ");
            //De server die we gebruiken om onze mail "te versturen"
            smtpClient.Host = "smtp.ethereal.email";
            //TCP/IP Port
            smtpClient.Port = 587;
            //SMTP server om te verzenden
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //Best practice om SSL te gebruiken
            smtpClient.EnableSsl = true;
            //Authenticatie gebruiken
            smtpClient.UseDefaultCredentials = false;
        }

        //Het aanmaken van een gewone Email
        //We geven de ontvanger mee, de html content en een onderwerp
        public MailMessage CreateMail(string ReceiverAdress, string htmlContent, string subject)
        {
            var from = new MailAddress("jelle.dispersyn@student.vives.be", "Plantify");
            var to = new MailAddress(ReceiverAdress);
            var message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = htmlContent;
            message.IsBodyHtml = true;

            return message;
        }

        //Bericht versturen
        public MailResult sendMessage(MailMessage msg)
        {
            var result = new MailResult() { Status = MailSendingStatus.OK };

            try
            {
                smtpClient.Credentials = new NetworkCredential("gunnar.fritsch31@ethereal.email", "9kSgGREuC3rf6N9PxJ");
                smtpClient.Send(msg);
            }
            catch (Exception e)
            {
                result.Status = MailSendingStatus.HasError;
                result.Message = e.Message;
            }
            return result;
        }
    }
}
