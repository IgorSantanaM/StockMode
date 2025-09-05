using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Common.Interfaces
{
    public interface IMailTemplateProvider
    {
        string GetEmailTemplate(string templateName);
    }
}
