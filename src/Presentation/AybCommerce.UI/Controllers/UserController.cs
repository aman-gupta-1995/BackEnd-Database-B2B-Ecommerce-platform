using System.Collections.Generic;
using AybCommerce.Core.Interfaces.Services;
using AybCommerce.UI.ViewModels.JsonResponseModel;
using AybCommerce.UI.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AybCommerce.Common.Models;
using AybCommerce.UI.Resources;
using Microsoft.AspNetCore.Http;

namespace AybCommerce.UI.Controllers
{
    public class UserController : BaseController
    {
        private readonly IAddressService _addressService;
        private readonly IUserService _userService;
        private readonly LocalizationService _localizer;
        
        public UserController(IAddressService addressService, IUserService userService, LocalizationService localizer, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _addressService = addressService;
            _userService = userService;
            _localizer = localizer;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult GetUserList()
        {
            var request = new DataTableFormRequest(HttpContext.Request);
            var users = _userService.RetrieveFilteredUsers(request);
            var usersData = new UsersViewModelBuilder(users.Data).Build();
            var response = new DataTableResponseModel<List<UsersViewModel>>(usersData, users.Draw, users.RecordsTotal, users.RecordsFiltered);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult UpdateUserStatus([FromBody]UpdateUserStatusViewModel model)
        {
            var result = _userService.UpdateUserStatus(model.UserId, model.StatusId);

            if (!result) { return BadRequest(new JsonResponseModel(false, _localizer.GetString("UserStatusUpdateFail"))); }

            return Ok(new JsonResponseModel(true, _localizer.GetString("UserStatusUpdated")));
        }

        public IActionResult Profile()
        {
            var user = _userService.RetrieveUserWithAddress(UserId);
            var profileViewModel = new ProfileViewModelBuilder(user).Build();
            return View(profileViewModel);
        }

        [HttpPost]
        public IActionResult RetrieveProfileData()
        {
            var user = _userService.RetrieveUserWithAddress(UserId);
            var profileViewModel = new ProfileViewModelBuilder(user).Build();
            return Ok(new JsonDataResponseModel<ProfileViewModel>(true, "User Data Retrieved.", profileViewModel));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpsertProfileInfo([FromBody] UpdateUserInfoViewModel model)
        {
            if (!ModelState.IsValid) { return BadRequest(new JsonResponseModel(false, "Model is not valid")); }

            var result = _userService.UpdateUserInfo(model.BuildUser(UserId));
            if (result) { return Ok(new JsonResponseModel(true, "User informations updated")); }

            return BadRequest(new JsonResponseModel(false, "Model is not valid"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpsertAddress([FromBody] UpsertAddressViewModel model)
        {
            if (!ModelState.IsValid) { return BadRequest(new JsonResponseModel(false, "Model is not valid")); }

            var result = _addressService.UpsertAddress(model.BuildAddress(UserId));
            if (result) { return Ok(new JsonResponseModel(true, "Address updated")); }

            return BadRequest(new JsonResponseModel(false, "Model is not valid"));
        }
    }
}