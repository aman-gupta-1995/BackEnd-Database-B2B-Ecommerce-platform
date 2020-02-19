using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AybCommerce.UI.ViewModels.Util;

namespace AybCommerce.UI.ViewModels.Order
{
    public class InvoiceViewModel
    {
        public int OrderId { get; set; }

        public string OrderDate { get; set; }

        public string FullName { get; set; }

        public string Address{ get; set; }

        public string CityStateZip { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        

        public List<InvoiceOrderItem> OrderItems { get; set; } = new List<InvoiceOrderItem>();

        public decimal TotalAmount { get; set; }

        public string OrderNote { get; set; }

    }

    public class InvoiceOrderItem
    {
        public int OrderItemId { get; set; }

        public string ProductName { get; set; }

        public decimal Amount { get; set; }

        public int Quantity { get; set; }

        public string Currency { get; set; }

        public string ProductCode { get; set; }

        public string ImageUrl { get; set; }

        public decimal TotalAmount => Amount * Quantity;
    }

    public class InvoiceViewModelBuilder : GenericViewModelBuilder<InvoiceViewModel>
    {
        Domain.Entities.Order _order;

        public InvoiceViewModelBuilder(Domain.Entities.Order order)
        {
            _order = order;
        }

        public override InvoiceViewModel Build()
        {
            var model = new InvoiceViewModel
            {
                OrderId = _order.Id,
                OrderDate = _order.Created.ToShortDateString(),
                FullName = _order.FirstName + " " + _order.LastName,
                Address = _order.Address.AddressLine1 + " " + _order.Address.AddressLine2,
                CityStateZip = _order.Address.City + " / " + _order.Address.State + " , " + _order.Address.ZipCode,
                Email= _order.Email,
                Phone = _order.PhoneNumber,
                TotalAmount = _order.TotalAmount,
                OrderNote = _order.OrderNote
            };

            foreach (var item in _order.OrderItems)
            {
                model.OrderItems.Add(new InvoiceOrderItem
                {
                    OrderItemId = item.Id,
                    ProductName = item.ProductName,
                    ProductCode = item.ProductCode,
                    ImageUrl = "/images/" + item.ProductCode + ".png",
                    Amount = item.Amount,
                    Quantity = item.Quantity,
                    Currency = item.Currency
                });
            }

            return model;
        }
    }
}
