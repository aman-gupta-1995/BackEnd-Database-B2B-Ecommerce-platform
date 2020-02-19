using AybCommerce.Domain.Entities;
using AybCommerce.UI.ViewModels.Util;

namespace AybCommerce.UI.ViewModels.Cart
{
    public class RemoveFromCartViewModel
    {
        public string ProductCode { get; set; }
    }

    public class RemoveFromCartViewBuilder : GenericViewModelBuilder<CartItem>
    {
        RemoveFromCartViewModel _model;

        public RemoveFromCartViewBuilder(RemoveFromCartViewModel model)
        {
            _model = model;
        }

        public override CartItem Build()
        {
            return new CartItem
            {
                ProductCode = _model.ProductCode
            };
        }
    }
}
