using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Core.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StockMode.Domain.Tags
{
    public class Tag : Entity<int>, IAggregateRoot
    {
        public string? Name { get; set; }
        public string? Color { get; set; }

        private Tag() { }

        public Tag(string name, string? color = null)
        {
            if(name is null && color is null)
                throw new DomainException("Tag must have at least a name or a color.");
            Name = name;
            Color = color;
        }

        public void UpdateDetails(string newName, string? newColor)
        {
            if (newName is null && newColor is null)
                throw new DomainException("Tag must have at least a name or a color.");
            Name = newName;
            Color = newColor;
        }
    }
}
