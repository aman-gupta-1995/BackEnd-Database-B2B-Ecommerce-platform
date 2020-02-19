using System.Collections.Generic;
using System.Threading.Tasks;
using AybCommerce.Core.Interfaces.Services;
using AybCommerce.UI.Resources;
using AybCommerce.UI.ViewModels.Checkout;
using AybCommerce.UI.ViewModels.JsonResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AybCommerce.UI.Controllers
{
    public class CheckoutController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly LocalizationService _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CheckoutController(IOrderService orderService, LocalizationService localizer, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _orderService = orderService;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> CompleteOrder([FromBody]CompletePaymentViewModel model)
        {
            var result = await _orderService.CreateOrderAsync(CartId, UserId, model.OrderNote);
            if (result < 1) { return BadRequest(new JsonResponseModel(false, _localizer.GetString("OrderIsNotCompleted"))); }

            ClearCart(_httpContextAccessor);
            var externalData = new Dictionary<string, object>() { { "orderId", result } };
            var response = new JsonResponseModel(true, _localizer.GetString("OrderIsCompleted"), externalData);
            return Ok(response);
        }

        public IActionResult VirtualPayment()
        {
            return View(new VirtualPosViewModel());
        }

        [HttpPost]
        public IActionResult VirtualPayment(VirtualPosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return View();
        }
    }
}