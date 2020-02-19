using System.Collections.Generic;
using System.Threading.Tasks;
using AybCommerce.Common.Models;
using AybCommerce.Domain.Entities;
using AybCommerce.Domain.Enumerations;

namespace AybCommerce.Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(string cartId, string userId, string orderNote);

        Task<Order> RetrieveOrder(int orderId, string userId);

        Task<Order> RetrieveOrderByAdmin(int orderId);

        DataTableResponseModel<List<Order>> RetrieveFilteredOrders(DataTableFormRequest request, string userId);

        bool UpdateOrderStatus(int orderId, OrderStatus status);


    }
}
