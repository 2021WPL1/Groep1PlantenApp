using PlantenApplicatie.UI.MailService.Data;
using PlantenApplicatie.UI.MailService.Enums;
using PlantenApplicatie.UI.MailService.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlantenApplicatie.UI.MailService.Classes
{
    public class Deserialize : IDeserializer
    {
        public ConverterResult<T> DeserializeObjectFromFile<T>(string absoluteFolderPath, string fileName, string fileType)
        {
            var result = new ConverterResult<T>() { Status = ConverterStatus.Ok };
            var fullFilePath = Path.Combine(absoluteFolderPath, fileName);
            string JSONString = File.ReadAllText(fullFilePath);
            result.ReturnValue = System.Text.Json.JsonSerializer.Deserialize<T>(JSONString);

            return result;
        }
    }
}
