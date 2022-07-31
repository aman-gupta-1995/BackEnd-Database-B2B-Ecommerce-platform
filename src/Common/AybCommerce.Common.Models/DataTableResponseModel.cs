using System;

namespace AybCommerce.Common.Models
{
    public class DataTableResponseModel<T> 
    {      
        public DataTableResponseModel(T data, string draw, int recordsTotal, int recordsFiltered)     
        {      
            Data = data;
            Draw = draw ?? throw new ArgumentNullException(nameof(draw));
            RecordsTotal = recordsTotal; 
            RecordsFiltered = recordsFiltered;
        }

        public T Data { get; set; }

        public string Draw { get; set; }

        public int RecordsTotal { get; set; }

        public int RecordsFiltered { get; set; }
    }
}
