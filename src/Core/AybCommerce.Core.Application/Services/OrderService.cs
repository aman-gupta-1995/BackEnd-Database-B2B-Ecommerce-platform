using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AybCommerce.Common.Models;
using AybCommerce.Core.Interfaces.Services;
using AybCommerce.Domain.Entities;
using AybCommerce.Domain.Enumerations;
using AybCommerce.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace AybCommerce.Core.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly AybCommerceDbContext _dbContext;

        public OrderService(AybCommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateOrderAsync(string cartId, string userId, string orderNote)
        {
            var cartItems = _dbContext.CartItems.Where(x => x.CartId == cartId);
            var user = _dbContext.Users.Include(x => x.Address).FirstOrDefault(x => x.Id == userId);
            if (!cartItems.Any() || user == null) { return 0; }

            var order = CreateOrder(orderNote, user, cartItems);
            foreach (var item in cartItems) { CreateOrderItem(order, item); }

            await _dbContext.Orders.AddAsync(order);
            var saveResult = _dbContext.SaveChanges() > 0;
            return saveResult ? order.Id : 0;
        }

        public async Task<Order> RetrieveOrder(int orderId, string userId)
        {
            return await _dbContext.Orders.Include(x => x.OrderItems)
                    .Include(x => x.User)
                    .Include(x => x.Address)
                    .FirstOrDefaultAsync(x => x.Id == orderId && x.UserId == userId);
        }

        public async Task<Order> RetrieveOrderByAdmin(int orderId)
        {
            return await _dbContext.Orders.Include(x => x.OrderItems)
                .Include(x => x.User)
                .Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Id == orderId);
        }
        public DataTableResponseModel<List<Order>> RetrieveFilteredOrders(DataTableFormRequest request, string userId)
        {
            //var response = DataTableResponseModel<List<User>>();
            IQueryable<Order> orders = string.IsNullOrEmpty(userId) ? _dbContext.Orders : _dbContext.Orders.Where(x => x.UserId == userId);

            int totalCount = orders.Count();

            #region FILTER

            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                orders = orders.Where(x => x.FirstName.ToLower().Contains(request.SearchValue.ToLower())
                                || x.LastName.ToLower().Contains(request.SearchValue.ToLower())
                                || x.Email.ToLower().Contains(request.SearchValue.ToLower()));
            }

            int totalRowsAfterFiltering = orders.Count();

            #endregion

            #region SORTING

                orders = orders.OrderBy(request.SortColumnName + " " + request.SortDirection);            

            #endregion

            #region PAGING

            orders = orders.Skip(request.Start).Take(request.Length);

            #endregion

            return new DataTableResponseModel<List<Order>>(orders.ToList(), request.Draw, totalCount, totalRowsAfterFiltering);
        }

        public bool UpdateOrderStatus(int orderId, OrderStatus status)
        {
            var currentOrder = _dbContext.Orders.FirstOrDefault(x => x.Id == orderId);

            if (currentOrder == null) return false;

            currentOrder.OrderStatus = status;
            return _dbContext.SaveChanges() > 0;
        }


        #region Helpers

        private static Order CreateOrder(string orderNote, User user, IQueryable<CartItem> cartItems)
        {
            return new Order
            {
                UserId = user.Id,
                AddressId = user.Address.Id,
                FirstName = user.Name,
                LastName = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                OrderNote = orderNote,
                TotalAmount = cartItems.Sum(x => x.SalePrice * x.Quantity),
                IsSuccess = true, // depends on your implementation temp success
                OrderStatus = Domain.Enumerations.OrderStatus.InProgress // depends on your implementation for github pass success
            };
        }

        private static void CreateOrderItem(Order order, CartItem item)
        {
            order.OrderItems.Add(new OrderItem
            {
                ProductCode = item.ProductCode,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                Amount = item.SalePrice,
                Currency = item.Currency
            });
        }

        #endregion

    }
}
