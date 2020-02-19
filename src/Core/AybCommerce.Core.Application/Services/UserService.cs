using System.Collections.Generic;
using AybCommerce.Core.Interfaces.Services;
using AybCommerce.Domain.Entities;
using AybCommerce.Domain.Enumerations;
using AybCommerce.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AybCommerce.Common.Models;
using System.Linq.Dynamic.Core;


namespace AybCommerce.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly AybCommerceDbContext _dbContext;

        public UserService(AybCommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<User> RetrieveUsers()
        {
            return _dbContext.Users.ToList();
        }

        public DataTableResponseModel<List<User>> RetrieveFilteredUsers(DataTableFormRequest request)
        {
            //var response = DataTableResponseModel<List<User>>();
            IQueryable<User> users = _dbContext.Users;

            int totalCount = users.Count();

            #region FILTER

            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                users = users.Where(x => x.Name.ToLower().Contains(request.SearchValue.ToLower())
                                || x.Surname.ToLower().Contains(request.SearchValue.ToLower())
                                || x.Email.ToLower().Contains(request.SearchValue.ToLower()));
            }

            int totalRowsAfterFiltering = users.Count();

            #endregion

            #region SORTING

            users = users.OrderBy(request.SortColumnName + " " + request.SortDirection);

            #endregion

            #region PAGING

            users = users.Skip(request.Start).Take(request.Length);

            #endregion

            return
                new DataTableResponseModel<List<User>>(users.ToList(), request.Draw, totalCount, totalRowsAfterFiltering);
        }

        public User RetrieveUserWithAddress(string userId)
        {
            return _dbContext.Users.Include(x => x.Address).FirstOrDefault(u => u.Id == userId
                                                                                && u.Status == EntityStatus.Active);
        }

        public bool UpdateUserStatus(string userId, EntityStatus status)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(x => x.Id == userId);

            if (currentUser == null) return false;

            currentUser.Status = status;
            return _dbContext.SaveChanges() > 0;
        }

        public bool UpdateUserInfo(User user)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(u => u.Id == user.Id && u.Status == EntityStatus.Active);

            if (currentUser == null) { return false; }

            currentUser.Name = user.Name;
            currentUser.Surname = user.Surname;
            currentUser.IdentityNumber = user.IdentityNumber;

            return _dbContext.SaveChanges() > 0;
        }
    }
}
