using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Core.Model;
using StockMode.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Domain.Suppliers
{
    public class Supplier : Entity<int>, IAggregateRoot
    {
        public string Name { get; set; }
        public string CorporateName { get; set; }
        public string CNPJ { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
        public string? Notes { get; set; }
        private readonly List<int> _tagIds = new();
        public IReadOnlyCollection<int>? TagIds => _tagIds.AsReadOnly();

        private Supplier() { }

        public Supplier(string name, string corporateName, string cnpj, string contactPerson, string email, string phoneNumber, Address address, string? notes = null) => 
            ValidateAndSetProperties(name, corporateName, cnpj, contactPerson, email, phoneNumber, address, notes);

        public void UpdateDetails(string name, string corporateName, string cnpj, string contactPerson, string email, string phoneNumber, Address address, string? notes = null) =>
            ValidateAndSetProperties(name, corporateName, cnpj, contactPerson, email, phoneNumber, address, notes);

        private void ValidateAndSetProperties(string name, string corporateName, string cnpj, string contactPerson, string email, string phoneNumber, Address address, string? notes)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Supplier name cannot be empty.");

            if (string.IsNullOrWhiteSpace(corporateName))
                throw new DomainException("Corporate name cannot be empty.");

            if (string.IsNullOrWhiteSpace(cnpj))
                throw new DomainException("CNPJ cannot be empty.");

            if (string.IsNullOrWhiteSpace(contactPerson))
                throw new DomainException("Contact person cannot be empty.");

            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Email cannot be empty.");

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new DomainException("Phone number cannot be empty.");

            if (address is null)
                throw new DomainException("Address cannot be null.");
        
            Name = name;
            CorporateName = corporateName;
            CNPJ = cnpj;
            ContactPerson = contactPerson;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Notes = notes;
        }

        public void AddTag(int tagId)
        {
            if (!_tagIds.Contains(tagId))
                _tagIds.Add(tagId);
        }

        public void RemoveTag(int tagId)
        {
            if (_tagIds.Contains(tagId))
                _tagIds.Remove(tagId);
        }

        public void ClearTags()
        {
            _tagIds.Clear();
        }

        public void UpdateTags(IEnumerable<int> tagIds)
        {
            _tagIds.Clear();
            foreach (var tagId in tagIds.Distinct())
            {
                _tagIds.Add(tagId);
            }
        }
    }
}
