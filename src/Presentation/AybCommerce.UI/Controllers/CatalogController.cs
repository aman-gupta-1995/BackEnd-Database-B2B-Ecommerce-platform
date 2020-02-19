using System;
using System.Threading.Tasks;
using AybCommerce.Common.Models;
using AybCommerce.Core.Interfaces.Services;
using AybCommerce.UI.Constants;
using AybCommerce.UI.Resources;
using AybCommerce.UI.ViewModels.Catalog;
using AybCommerce.UI.ViewModels.JsonResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AybCommerce.UI.Controllers
{
    public class CatalogController : BaseController
    {
        private readonly ICatalogService _catalogService;
        private readonly LocalizationService _localizer;
        private readonly IMemoryCache _memoryCache;

        public CatalogController(ICatalogService catalogService, LocalizationService localizer, IMemoryCache memoryCache, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _catalogService = catalogService;
            _localizer = localizer;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RetrieveProducts([FromBody]RetrieveProductsViewModel model)
        {
            //var products = _catalogService.RetrieveProducts(model.PageIndex, model.PageSize, model.CategoryId);
            var cacheKey = string.Format(CacheEntryConstants.Products, model.PageIndex, model.PageSize, model.CategoryId);
            var cachedProducts = await _memoryCache.GetOrCreateAsync(
                cacheKey, entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromHours(6);
                    return _catalogService.RetrieveProducts(model.PageIndex, model.PageSize, model.CategoryId);
                });

            return Ok(new JsonDataResponseModel<CatalogResponseModel>(true, _localizer.GetString("UserStatusUpdated"), cachedProducts));
        }
    }
}