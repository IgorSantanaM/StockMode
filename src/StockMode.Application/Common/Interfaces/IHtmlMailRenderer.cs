using StockMode.Application.Features.Sales.Dtos;
using StockMode.Domain.Sales.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Common.Interfaces
{
    public interface IHtmlMailRenderer
    {
        Task<string> RenderAsync<TModel>(string templateName, TModel saleCompletedEmail);
    }
}
