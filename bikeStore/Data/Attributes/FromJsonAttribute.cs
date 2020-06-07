using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Data.Extensions.Helpers;
using Newtonsoft.Json;

namespace BikeStore.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FromJsonAttribute : Attribute, IJsonAttribute
    {
        JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            Formatting = Formatting.None,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            Converters = new List<JsonConverter> { new DecimalConverter() }
        };

        public object TryConvert(string modelValue, Type targetType, out bool success)
        {
            var value = JsonConvert.DeserializeObject(modelValue, targetType, _settings);
            success = value != null;
            return value;
        }
    }
}
