using System.Collections.Generic;
using AybCommerce.Domain.Entities;
using AybCommerce.UI.ViewModels.Util;

namespace AybCommerce.UI.ViewModels.Cart
{
    public class CartItemViewModel
    {
        public string ProductCode { get; set; }

        public int Quantity { get; set; }

        public int CartItemId { get; set; }

        public string CartId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public decimal SalePrice { get; set; }

        public string Currency { get; set; }

        public string ImageUrl { get; set; }
    }

    public class CartItemViewModelBuilder : GenericViewModelBuilder<List<CartItemViewModel>>
    {
        readonly List<CartItem> _cartItems;

        public CartItemViewModelBuilder(List<CartItem> cartItems)
        {
            _cartItems = cartItems;
        }

        public override List<CartItemViewModel> Build()
        {
            if (_cartItems == null) return null;

            var response = new List<CartItemViewModel>();
            foreach (var cartItem in _cartItems)
            {
                response.Add(new CartItemViewModel
                {
                    CartItemId = cartItem.Id,
                    CartId = cartItem.CartId,
                    Currency = cartItem.Currency,
                    ProductName = cartItem.ProductName,
                    Price = cartItem.Price,
                    SalePrice = cartItem.SalePrice,
                    ProductCode = cartItem.ProductCode,
                    Quantity = cartItem.Quantity,
                    ImageUrl = "images/"+ cartItem.ProductCode+".png"
                });
            }

            return response;
        }
    }
}
