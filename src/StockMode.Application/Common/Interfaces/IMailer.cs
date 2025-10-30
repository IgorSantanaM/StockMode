using StockMode.Application.Common.Messaging;
using StockMode.Application.Features.Products.Dtos;
using StockMode.Application.Features.Sales.Dtos;
using StockMode.Domain.Sales.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Common.Interfaces
{
    public interface IMailer
    {
        /// <summary>
        /// Sends email using a JSON model - allows flexible template rendering
        /// </summary>
        Task SendGenericAsync(string to, string subject, string templateName, string modelJson, Type modelType, CancellationToken token );
    }
}
