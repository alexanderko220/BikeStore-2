using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BikeStore.Data.Extensions.Helpers
{
    public class DecimalConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            decimal? d = default;
            if (value != null)
            {
                d = value as decimal?;
                if (d.HasValue) // If value was a decimal?, then this is possible
                {
                    d = new decimal(decimal.ToDouble(d.Value)); // The ToDouble-conversion removes all unnessecary precision
                }
            }
            JToken.FromObject(d).WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            NumberFormatInfo formatInfo = new NumberFormatInfo
            {
                NegativeSign = "-",
                CurrencyDecimalSeparator = ".",
                CurrencyGroupSeparator = ",",
                CurrencySymbol = "₴"
            };



            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Float || token.Type == JTokenType.Integer)
            {
                return token.ToObject<decimal>();
            }
            if (token.Type == JTokenType.String)
            {
                // customize this to suit your needs
                return Decimal.Parse(token.ToString(), NumberStyles.Currency, formatInfo);
            }
            if (token.Type == JTokenType.Null && objectType == typeof(decimal?))
            {
                return null;
            }
            throw new JsonSerializationException("Unexpected token type: " +
                                                 token.Type.ToString());
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(decimal) || objectType == typeof(decimal?));
        }
    }
}
