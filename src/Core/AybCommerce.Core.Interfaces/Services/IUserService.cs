using System.Collections.Generic;
using AybCommerce.Common.Models;
using AybCommerce.Domain.Entities;
using AybCommerce.Domain.Enumerations;

namespace AybCommerce.Core.Interfaces.Services
{
    public interface IUserService
    {
        List<User> RetrieveUsers();

        DataTableResponseModel<List<User>> RetrieveFilteredUsers(DataTableFormRequest request);

        User RetrieveUserWithAddress(string userId);

        bool UpdateUserStatus(string userId, EntityStatus status);

        bool UpdateUserInfo(User user);
    }
}
