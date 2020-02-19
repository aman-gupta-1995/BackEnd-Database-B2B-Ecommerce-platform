using AybCommerce.Core.Interfaces.Services;
using AybCommerce.UI.Resources;
using AybCommerce.UI.ViewModels.Cart;
using AybCommerce.UI.ViewModels.JsonResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AybCommerce.UI.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartItemService _cartItemService;
        private readonly LocalizationService _localizer;

        public CartController(ICartItemService cartItemService, LocalizationService localizer, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _cartItemService = cartItemService;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            var cartItems = _cartItemService.RetrieveCartItems(CartId);
            return View(new CartItemViewModelBuilder(cartItems).Build());
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody]AddToCartViewModel model)
        {
            var cartItem = new AddToCartViewModelBuilder(model).Build();
            _cartItemService.UpsertToCartItem(cartItem, CartId);
            return Ok(new JsonResponseModel(true, _localizer.GetString("CartUpdated")));
        }

        [HttpPost]
        public IActionResult RemoveFromCart([FromBody]RemoveFromCartViewModel model)
        {
            var cartItem = new RemoveFromCartViewBuilder(model).Build();
            _cartItemService.RemoveToCartItem(cartItem, CartId);
            return Ok(new JsonResponseModel(true, _localizer.GetString("CartUpdated")));
        }
    }
}