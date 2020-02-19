using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Localization;

namespace AybCommerce.UI.Resources
{
    public class LocalizationService
    {
        private readonly IStringLocalizer _localizer;

        public LocalizationService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name);
        }

        public LocalizedString GetString(string key)
        {
            return _localizer[key];
        }

        public Dictionary<string, string> GetAllStrings()
        {
            return _localizer.GetAllStrings().Select(x => new KeyValuePair<string, string>(x.Name, x.Value))
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
