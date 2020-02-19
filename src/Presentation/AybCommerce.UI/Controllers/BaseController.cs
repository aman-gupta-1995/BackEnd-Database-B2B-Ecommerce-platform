using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace AybCommerce.UI.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        public string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        protected readonly string CartId;

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            CartId = httpContextAccessor.HttpContext.Session.GetString("CartId") ?? Guid.NewGuid().ToString();
            httpContextAccessor.HttpContext.Session.SetString("CartId", CartId);
        }

        [NonAction]
        public void ClearCart(IHttpContextAccessor httpContextAccessor)
        {
            httpContextAccessor.HttpContext.Session.Remove("CartId");
        }
    }
}