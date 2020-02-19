using AybCommerce.Domain.Entities;
using AybCommerce.UI.ViewModels.User;
using AybCommerce.UI.ViewModels.Util;

namespace AybCommerce.UI.ViewModels.Cart
{
    public class AddToCartViewModel
    {
        public string ErpCode { get; set; }
        public int Quantity { get; set; }
    }

    public class AddToCartViewModelBuilder : GenericViewModelBuilder<CartItem>
    {
        AddToCartViewModel _model;

        public AddToCartViewModelBuilder(AddToCartViewModel model)
        {
            _model = model;
        }

        public override CartItem Build()
        {
            return new CartItem
            {
                Currency = "TL",
                ProductCode = _model.ErpCode,
                Quantity = _model.Quantity
            };
        }
    }
}