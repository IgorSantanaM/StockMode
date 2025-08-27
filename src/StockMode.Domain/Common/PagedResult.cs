using MediatR;
using StockMode.Domain.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Domain.Common
{
    public class PagedResult<T> where T : class
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

    }
}
