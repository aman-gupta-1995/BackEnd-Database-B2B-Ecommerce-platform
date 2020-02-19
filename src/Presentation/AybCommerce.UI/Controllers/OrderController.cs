using System.Collections.Generic;
using System.Threading.Tasks;
using AybCommerce.Common.Models;
using AybCommerce.Core.Interfaces.Services;
using AybCommerce.UI.Resources;
using AybCommerce.UI.ViewModels.JsonResponseModel;
using AybCommerce.UI.ViewModels.Order;
using AybCommerce.UI.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AybCommerce.UI.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly LocalizationService _localizer;

        public OrderController(IHttpContextAccessor httpContextAccessor, IOrderService orderService, LocalizationService localizer) : base(httpContextAccessor)
        {
            _orderService = orderService;
            _localizer = localizer;
        }


        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PreviousOrders()
        {
            return View();
        }

        public async Task<IActionResult> Invoice(int orderId)
        {
            var order = await _orderService.RetrieveOrder(orderId, UserId);
            if (order == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            return View(new InvoiceViewModelBuilder(order).Build());
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AdminInvoice(int orderId)
        {
            var order = await _orderService.RetrieveOrderByAdmin(orderId);
            return View("Invoice", new InvoiceViewModelBuilder(order).Build());
        }

        public async Task<IActionResult> GetOrderList(bool isPreviousOrders = false)
        {
            var request = new DataTableFormRequest(HttpContext.Request);
            var userId = isPreviousOrders ? UserId : null;
            var orders = _orderService.RetrieveFilteredOrders(request, userId);
            var ordersData = new OrdersViewModelBuilder(orders.Data).Build();
            var response = new DataTableResponseModel<List<OrdersViewModel>>(ordersData, orders.Draw, orders.RecordsTotal, orders.RecordsFiltered);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult UpdateOrderStatus([FromBody]UpdateOrderStatusViewModel model)
        {
            var result = _orderService.UpdateOrderStatus(model.OrderId, model.StatusId);

            if (!result) { return BadRequest(new JsonResponseModel(false, _localizer.GetString("OrderStatusUpdateFail"))); }

            return Ok(new JsonResponseModel(true, _localizer.GetString("OrderStatusUpdated")));
        }
    }
}