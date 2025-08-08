using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Core.Model;
using StockMode.Domain.Sales;
using StockMode.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Domain.Customers
{
    public class Customer : Entity<int>, IAggregateRoot
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }

        private Customer() { }

        public Customer(string name, string email, string phoneNumber, Address address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Customer name cannot be empty.");

            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Email cannot be empty.");

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new DomainException("Phone number cannot be empty.");

            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public void UpdateDetails(string newName, string newEmail, string newPhoneNumber, Address newAddress)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new DomainException("Customer name cannot be empty.");
            Name = newName;
            Email = newEmail ?? throw new DomainException("Email cannot be null");
            PhoneNumber = newPhoneNumber ?? throw new DomainException("Phone number cannot be null");
        }
    }
}
