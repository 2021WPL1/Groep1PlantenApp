using PlantenApplicatie.UI.MailService.Data;
using PlantenApplicatie.UI.MailService.Enums;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace PlantenApplicatie.UI.MailService
{
    //Jelle & Stephanie
    public class SMTPMailMessage
    {
        private SmtpClient smtpClient { get; set; }
        public SMTPMailService(string username, string wachtwoord, string host)
        {
            smtpClient = new SmtpClient();

            //Authenticatie voor de Email verstuurd wordt namens de "Client"
            //"True" als de default credentials gebruikt worden, anders false. de standaard waarde is "false"
            //smtpClient.UseDefaultCredentials = false;

            //De Ethereal server is een beveiligde server; dus er moet worden ingelogd
            //daarvoor zijn de credentials nodig om de gebruiker te authenticeren
            smtpClient.Credentials = new NetworkCredential(username, wachtwoord);
            //De server die we gebruiken om onze mail "te versturen"
            smtpClient.Host = host
            //SMTP server om te verzenden
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //Best practice om SSL te gebruiken
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;

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
                //smtpClient.Credentials = new NetworkCredential("gunnar.fritsch31@ethereal.email", "9kSgGREuC3rf6N9PxJ");
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
