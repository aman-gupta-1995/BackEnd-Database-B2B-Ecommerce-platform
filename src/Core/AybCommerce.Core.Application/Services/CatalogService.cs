using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AybCommerce.Common.Models;
using AybCommerce.Core.Interfaces.Services;
using AybCommerce.Domain.Entities;
using AybCommerce.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace AybCommerce.Core.Application.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly AybCommerceDbContext _dbContext;

        public CatalogService(AybCommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CatalogResponseModel> RetrieveProducts(int pageIndex, int pageSize, int? categoryId)
        {
            IQueryable<Product> products = _dbContext.Products;

            #region FILTER

            if (categoryId != null)
            {
                products = products.Where(x => x.CategoryId == categoryId.Value);
            }

            int totalCount = products.Count();

            #endregion

            #region SORTING



            #endregion

            #region PAGING

            products = products.Skip(pageIndex * pageSize).Take(pageSize);

            #endregion

            var vm = new CatalogResponseModel
            {
                Products = await products.ToListAsync(),
                CategoryFilterApplied = categoryId,
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = pageSize,
                    TotalItems = totalCount,
                    TotalPages = (totalCount / pageSize) + 1
                }
            };

            //vm.PaginationInfo.Next = vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1 ? "is-disabled" : "";
            //vm.PaginationInfo.Previous = vm.PaginationInfo.ActualPage == 0 ? "is-disabled" : "";
            return vm;
        }
    }
}
