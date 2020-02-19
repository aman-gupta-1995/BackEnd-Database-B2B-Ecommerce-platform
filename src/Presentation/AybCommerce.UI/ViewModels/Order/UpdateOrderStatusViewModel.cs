using AybCommerce.Domain.Enumerations;

namespace AybCommerce.UI.ViewModels.Order
{
    public class UpdateOrderStatusViewModel
    {
        public int OrderId { get; set; }

        public OrderStatus StatusId { get; set; }
    }
}
