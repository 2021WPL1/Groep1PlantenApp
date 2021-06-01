using PlantenApplicatie.UI.MailService.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantenApplicatie.UI.MailService.Interfaces
{
    interface IDeserializer
    {
        public ConverterResult<T> DeserializeObjectFromFile<T>(string absoluteFolderPath, string fileName, string fileType);
    }
}
