using System;
using AybCommerce.Core.Interfaces.Services;
using AybCommerce.Domain.Entities; 
using AybCommerce.Persistance.Data; 
using System.Linq; 
  
namespace AybCommerce.Core.Application.Services 
{
    public class AddressService : IAddressService
    {
        private readonly AybCommerceDbContext _dbContext;

        public AddressService(AybCommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Address RetrieveAddress(int addressId)
        {
            return _dbContext.Addresses.FirstOrDefault(x => x.Id == addressId);
        }

        public bool UpsertAddress(Address address)
        {
            var currentAddress = RetrieveAddress(address.Id);

            if (currentAddress != null)
            {
                currentAddress.AddressLine1 = address.AddressLine1;
                currentAddress.AddressLine2 = address.AddressLine2;
                currentAddress.ZipCode = address.ZipCode;
                currentAddress.City = address.City; 
                currentAddress.State = address.State;

                return _dbContext.SaveChanges() > 0;
            } 
 
            _dbContext.Addresses.Add(address);
            return _dbContext.SaveChanges() > 0; 
        }
    }
}
