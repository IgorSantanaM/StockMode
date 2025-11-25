using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Core.Model;
using StockMode.Domain.ValueObjects;

namespace StockMode.Domain.Customers
{
    public class Customer : Entity<int>, IAggregateRoot
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
        private readonly List<int> _tagIds = new();
        public IReadOnlyCollection<int> TagIds => _tagIds.AsReadOnly();
        public string? Notes { get; set; }

        private Customer() { }

        public Customer(string name, string email, string phoneNumber, Address address, string? notes)
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
            Address = address;
            Notes = notes;
        }

        public void UpdateDetails(string? newName, string? newEmail, string? newPhoneNumber, Address? newAddress, string? notes)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new DomainException("Customer name cannot be empty.");

            if (string.IsNullOrWhiteSpace(newEmail))
                throw new DomainException("Email cannot be empty.");

            if (string.IsNullOrWhiteSpace(newPhoneNumber))
                throw new DomainException("Phone number cannot be empty.");

            if (newAddress == null)
                throw new DomainException("Address cannot be null");

            Name = newName;
            Email = newEmail;
            PhoneNumber = newPhoneNumber;
            Address = newAddress;
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

        public void UpdateTags(IEnumerable<int> newTagIds)
        {
            _tagIds.Clear();
            foreach (var tagId in newTagIds.Distinct())
            {
                _tagIds.Add(tagId);
            }
        }
    }
}
