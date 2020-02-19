using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AybCommerce.UI.ViewModels.User;
using AybCommerce.UI.ViewModels.Util;

namespace AybCommerce.UI.ViewModels.Order
{
    public class OrdersViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string Created { get; set; }
    }

    public class OrdersViewModelBuilder : GenericViewModelBuilder<List<OrdersViewModel>>
    {
        List<Domain.Entities.Order> _orders;

        public OrdersViewModelBuilder(List<Domain.Entities.Order> orders)
        {
            _orders = orders;
        }

        public override List<OrdersViewModel> Build()
        {

            return _orders.Select(order => new OrdersViewModel
            {
                Id = order.Id,
                FirstName = order.FirstName,
                LastName = order.LastName,
                Email = order.Email,
                Status = order.OrderStatus.ToString(),
                TotalAmount = order.TotalAmount,
                Created = order.Created.ToShortDateString()
            }).ToList();

            var model = new List<OrdersViewModel>();

            foreach (var order in _orders)
            {
                model.Add(new OrdersViewModel
                {
                    Id = order.Id,
                    FirstName = order.FirstName,
                    LastName = order.LastName,
                    Email = order.Email,
                    Status = order.OrderStatus.ToString(),
                    TotalAmount = order.TotalAmount,
                    Created = order.Created.ToShortDateString()
                });
            }

            return model;
        }
    }
}
