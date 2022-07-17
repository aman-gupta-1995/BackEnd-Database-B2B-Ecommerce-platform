using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AybCommerce.UI.ViewModels.JsonResponseModel
{
    public class JsonDataResponseModel<T>
    {
        public JsonDataResponseModel(bool success, string message)
        {
            Success = success;
            Message = message;
        } 

        public JsonDataResponseModel(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public JsonDataResponseModel(bool success, string message, Dictionary<string, object> externalData) : this(success, message)
        {
            ExternalData = externalData;
        }

        [JsonProperty]
        private T Data { get; set; }

        [JsonProperty]
        private bool Success { get; set; }

        [JsonProperty]
        private string Message { get; set; }

        [JsonProperty]
        private Dictionary<string, object> ExternalData { get; set; } = new Dictionary<string, object>();
    }
}
