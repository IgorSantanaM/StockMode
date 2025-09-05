using StockMode.Application.Common.Interfaces;
using StockMode.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Infra.Services.Email
{
    public class EmbeddedResourceMailTemplateProvider : IMailTemplateProvider
    {
        public string GetEmailTemplate(string templateName)
        {
            return EmbeddedResource.ReadAllText($"{templateName}.csmjml");
        }
    }
}
