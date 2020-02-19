using System.Threading.Tasks;
using AybCommerce.Common.Models;

namespace AybCommerce.Core.Interfaces.Services
{
    public interface ICatalogService
    {
        Task<CatalogResponseModel> RetrieveProducts(int pageIndex, int itemsPage, int? categoryId);
    }
}
