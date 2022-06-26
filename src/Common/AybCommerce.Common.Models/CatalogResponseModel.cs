using System.Collections.Generic;
using AybCommerce.Domain.Entities;

namespace AybCommerce.Common.Models
{ 
    public class CatalogResponseModel
    {   
        public List<Product> Products { get; set; }  
 
        public PaginationInfoViewModel PaginationInfo { get; set; }

        public int? CategoryFilterApplied { get; set; }
    }
}
 
