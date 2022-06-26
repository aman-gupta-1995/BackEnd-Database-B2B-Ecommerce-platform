 ﻿using AybCommerce.Domain.Entities;

namespace AybCommerce.Core.Interfaces.Services
{
    public interface IAddressService
    {
        // normally they all should return some response model 
        Address RetrieveAddress(int addressId);

        bool UpsertAddress(Address address);

    }
}
