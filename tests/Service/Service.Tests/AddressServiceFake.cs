using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AybCommerce.Core.Interfaces.Services;
using AybCommerce.Domain.Entities;
 
namespace Service.Tests
{
    public class AddressServiceFake : IAddressService 
    {
        private readonly List<Address> _address;

        public AddressServiceFake()
        {
            _address = new List<Address>
            {
                new Address
                {
                    Id = 1,
                    AddressLine1 = "a",
                    AddressLine2 = "a",
                    City = "a",
                    State = "a",
                    Country = "a",
                    ZipCode = "07",
                    UserId = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200",
                    CreatedId = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200",
                    Created = DateTime.Now,
                },
                new Address
                {
                    Id = 2,
                    AddressLine1 = "b",
                    AddressLine2 = "b",
                    City = "b",
                    State = "b",
                    Country = "b",
                    ZipCode = "07",
                    UserId = "33704c4a-5b87-464c-bfb6-51971b4d18ad",
                    CreatedId = "33704c4a-5b87-464c-bfb6-51971b4d18ad",
                    Created = DateTime.Now,
                }
            };
        }

        public Address RetrieveAddress(int addressId)
        {
            return _address.FirstOrDefault(x => x.Id == addressId);
        }

        public bool UpsertAddress(Address address)
        {
            var currentAddress = _address.FirstOrDefault(x => x.Id == address.Id);

            if (currentAddress != null)
            {
                currentAddress.AddressLine1 = address.AddressLine1;
                currentAddress.AddressLine2 = address.AddressLine2;
                currentAddress.ZipCode = address.ZipCode;
                currentAddress.City = address.City;
                currentAddress.State = address.State;
                return true;
            }

            address.Id = _address.Max(x => x.Id) + 1;
            _address.Add(address);
            return true;
        }
    }
}
