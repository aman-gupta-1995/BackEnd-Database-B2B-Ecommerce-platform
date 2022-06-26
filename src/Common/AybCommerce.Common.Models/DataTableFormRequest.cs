using System;
using Microsoft.AspNetCore.Http;
 
namespace AybCommerce.Common.Models
{  
    public class DataTableFormRequest  
    {
        public DataTableFormRequest(HttpRequest request) 
        {
            Start = Convert.ToInt32(request.Form["start"]);

            Length = Convert.ToInt32(request.Form["length"]);

            SearchValue = request.Form["search[value]"];

            SortColumnName = request.Form["columns[" + request.Form["order[0][column]"] + "][name]"];

            SortDirection = request.Form["order[0][dir]"];

            Draw = request.Form["draw"];
        }

        public int Start { get; set; }

        public int Length { get; set; }

        public string SearchValue { get; set; }

        public string SortColumnName { get; set; }

        public string SortDirection { get; set; }
  
        public string Draw { get; set; }
    }
}
