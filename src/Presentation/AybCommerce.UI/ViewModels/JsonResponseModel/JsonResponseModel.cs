using Newtonsoft.Json;
using System.Collections.Generic;

namespace AybCommerce.UI.ViewModels.JsonResponseModel
{
    public class JsonResponseModel
    {
        public JsonResponseModel(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public JsonResponseModel(bool success, string message, Dictionary<string, object> externalData) : this(success, message)
        {
            ExternalData = externalData;
        }


        [JsonProperty]
        private bool Success { get; set; }

        [JsonProperty]
        private string Message { get; set; }

        [JsonProperty]
        private Dictionary<string, object> ExternalData { get; set; } = new Dictionary<string, object>();

    }
}
