using AybCommerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AybCommerce.UI.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ICartItemService _cartItemService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingCartSummary(ICartItemService cartItemService, IHttpContextAccessor httpContextAccessor)
        {
            _cartItemService = cartItemService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            var cartId = _httpContextAccessor.HttpContext.Session.GetString("CartId");
            var cartItemCount = _cartItemService.RetrieveCartItems(cartId).Count;
            return View(cartItemCount);
        }
    }
}
