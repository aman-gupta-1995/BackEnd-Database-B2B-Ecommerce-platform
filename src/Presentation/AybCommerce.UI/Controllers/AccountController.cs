using AybCommerce.Core.Interfaces.Services;
using AybCommerce.Domain.Entities;
using AybCommerce.Domain.Enumerations;
using AybCommerce.UI.Extensions;
using AybCommerce.UI.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AybCommerce.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Login()
        {
            ClearCart(_httpContextAccessor);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "Üyeliğiniz bulunamadı.");
                return View(model);
            }

            if (user.Status != EntityStatus.Active)
            {
                return RedirectToAction("PendingApproval", "Account");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username/Password not found");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult PendingApproval()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }

            var user = new User() { Name = model.Name, Surname = model.Surname, UserName = model.Email, Email = model.Email, Status = EntityStatus.Draft };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded) { return RedirectToAction("PendingApproval", "Account"); }

            AddErrors(result);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }

            var user = _userManager.FindByNameAsync(model.Email).Result;
            if (user == null)
            {
                ModelState.AddModelError("", "This username does not have an account!");
                return View(model);
            }

            string code = await _userManager.GeneratePasswordResetTokenAsync(user);
            string callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);

#if DEBUG
            model.Email = "arslanaybars@gmail.com";
#endif

            using (var smtpClient = HttpContext.RequestServices.GetRequiredService<SmtpClient>())
            {
                var message = new MailMessage("aybmailsender@gmail.com", model.Email, "Reset Password",
                   $"Please reset your password by clicking here: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>link</a>");
                message.IsBodyHtml = true;
                await smtpClient.SendMailAsync(message);
            }
            
            return RedirectToAction("ForgotPasswordConfirmation", "Account");
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) { return RedirectToAction(nameof(ResetPasswordConfirmation)); }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded) { return RedirectToAction(nameof(ResetPasswordConfirmation)); }

            AddErrors(result);
            return View();
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion
    }
}