using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AybCommerce.UI.ViewModels.Home;
using System.Net.Mail;
using System.Net;
using AybCommerce.UI.Resources;
using AybCommerce.UI.ViewModels.JsonResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AybCommerce.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LocalizationService _stringLocalizer;

        public HomeController(ILogger<HomeController> logger, LocalizationService stringLocalizer, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _logger = logger;
            _stringLocalizer = stringLocalizer;
        }

        public IActionResult Index()
        {
            var hello = _stringLocalizer.GetString("Hello");

            //_logger.LogCritical("nlog is working from a controller");
            //_logger.LogError("nlog is working from a controller");
            //_logger.LogDebug("nlog is working from a controller");
            //_logger.LogWarning("nlog is working from a controller");
            //_logger.LogInformation("nlog is working from a controller");
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Team()
        {
            return View();
        }

        public IActionResult BankAccounts()
        {
            return View();
        }

        public IActionResult References()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact([FromBody]ContactViewModel model)
        {
            using (var smtpClient = HttpContext.RequestServices.GetRequiredService<SmtpClient>())
            {
                var toEmail = "blabla@gmail.com";
                var message = new MailMessage("aybmailsender@gmail.com", toEmail, model.Subject, model.FullName
                    + " named user sent you this message; \n\n" + model.Message);
                await smtpClient.SendMailAsync(message);
            }
            return StatusCode((int)HttpStatusCode.OK, Json("123"));
        }

        public IActionResult MemberAgreement()
        {
            return View();
        }

        public IActionResult GuaranteeReturn()
        {
            return View();
        }

        public IActionResult PrivacySecurity()
        {
            return View();
        }

        public IActionResult DeliveryConditions()
        {
            return View();
        }

        // [FromRoute] int statusCode
        [AllowAnonymous]
        public IActionResult Error(int statusCode)
        {
            return View(statusCode);
        }

        [HttpGet("RetrieveTranslations")]
        [AllowAnonymous]
        public IActionResult GetTranslations()
        {
            return Ok(JsonConvert.SerializeObject(_stringLocalizer.GetAllStrings()));
        }

        [HttpGet("SetLanguage")]
        [AllowAnonymous]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), IsEssential = true }
            );

            return LocalRedirect(returnUrl ?? "/Home/Index");
        }

        [HttpGet("RetrieveShoppingCartSummary")]
        public IActionResult RetrieveShoppingCartSummary()
        {
            return ViewComponent("ShoppingCartSummary");
        }
    }
}
