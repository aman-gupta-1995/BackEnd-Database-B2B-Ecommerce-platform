using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AybCommerce.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : BaseController
    {
        public AdminController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public IActionResult Orders()
        {
            return View();
        }

    }
}