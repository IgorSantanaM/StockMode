using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Core.Model;

namespace StockMode.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public int Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public Address(int number, string street, string city, string state, string zipCode)
        {
            Number = number;
            Street = street ?? throw new DomainException("Street cannot be null");
            City = city ?? throw new DomainException("City cannot be null");
            State = state ?? throw new DomainException("State cannot be null");
            ZipCode = zipCode ?? throw new DomainException("ZipCode cannot be null");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ZipCode;
            yield return Number;
        }
    }
}
