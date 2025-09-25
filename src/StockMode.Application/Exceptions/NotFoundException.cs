using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Exceptionns
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key) : base($"Entity (\"{name}\") with key({ key }) was not found.")
        {
        }
    }
}
