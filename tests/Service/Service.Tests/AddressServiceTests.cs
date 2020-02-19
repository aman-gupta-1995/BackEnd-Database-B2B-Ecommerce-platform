using System;
using AybCommerce.Domain.Entities;
using Xunit;

namespace Service.Tests
{
    public class AddressServiceTests
    {
        readonly AddressServiceFake _addressServiceFake;

        public AddressServiceTests()
        {
            _addressServiceFake = new AddressServiceFake();
        }

        [Fact]
        public void Can_Retrieve_Address()
        {
            // Arrange 

            // Act
            var address = _addressServiceFake.RetrieveAddress(1);

            // Assert
            Assert.Equal(1, address.Id);
        }

        [Fact]
        public void Can_Insert_Address()
        {
            // Arrange 
            var address = new Address
            {
                AddressLine1 = "c",
                AddressLine2 = "c",
                City = "c",
                State = "c",
                Country = "c",
                ZipCode = "02",
                UserId = "1b2bd817-98cd-4cf3-a80a-53ea0cd9c200",
                CreatedId = "1b2bd817-98cd-4cf3-a80a-53ea0cd9c200",
                Created = DateTime.Now
            };

            // Act 
            var result = _addressServiceFake.UpsertAddress(address); // method smell

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Can_Update_Address()
        {
            // Arrange 
            var address = new Address
            {
                Id = 1,
                AddressLine1 = "inserted",
                AddressLine2 = "c",
                City = "c",
                State = "c",
                Country = "c",
                ZipCode = "02",
                UserId = "1b2bd817-98cd-4cf3-a80a-53ea0cd9c200",
                CreatedId = "1b2bd817-98cd-4cf3-a80a-53ea0cd9c200",
                Created = DateTime.Now
            };

            // Act
            var result = _addressServiceFake.UpsertAddress(address);

            // Assert
            Assert.True(result);
        }
    }
}
