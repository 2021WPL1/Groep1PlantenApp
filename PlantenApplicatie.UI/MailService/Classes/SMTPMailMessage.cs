using PlantenApplicatie.UI.MailService.Data;
using PlantenApplicatie.UI.MailService.Enums;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace PlantenApplicatie.UI.MailService
{
    public class SMTPMailService
    {
        private SmtpClient smtpClient { get; set; }
        public SMTPMailService(string username, string wachtwoord, string host)
        {
           smtpClient = new SmtpClient();
            //
          //  smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(username, wachtwoord);
            smtpClient.Host = host;
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            
        }//
        //Maakt een normale mail
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

        //Verstuurt de mail
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
