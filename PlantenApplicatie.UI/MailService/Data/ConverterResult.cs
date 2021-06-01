using PlantenApplicatie.UI.MailService.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantenApplicatie.UI.MailService.Data
{
    public class ConverterResult<TResultObject>
    {
        public ConverterStatus Status { get; set; }
        public Exception Error { get; set; }
        public TResultObject ReturnValue { get; set; }
    }
}
