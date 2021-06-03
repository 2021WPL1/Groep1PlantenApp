using PlantenApplicatie.UI.MailService.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantenApplicatie.UI.MailService.Data
{
    public class MailResult
    {
        //geeft de status door of de mail kon verstuurd worden of niet
        public MailSendingStatus Status { get; set; }
        public string Message { get; set; }
    }
}
