using InventoryManagement.Core.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Data;

namespace WebApp.Core.Extensions.Serializers
{
    public static class JsonSerialize
    {
        public static string ToJsonSerialize(this DataSet ds)
        {
            var jsonString = JsonConvert.SerializeObject(ds, Formatting.None, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            return jsonString;
        }

        public static string ToJsonSerialize(this DataTable dt)
        {
            return JsonConvert.SerializeObject(dt, Formatting.None, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public static string ToJsonSerialize(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public static T ToModel<T>(this string jsonString, string dateFormat = "dd/MM/yyyy")
        {
            if (string.IsNullOrEmpty(dateFormat))
                dateFormat = "dd/MM/yyyy";

            var model = JsonConvert.DeserializeObject<T>(jsonString, new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new JsonDateTimeConverter(dateFormat) },
                DateParseHandling = DateParseHandling.None
            });

            return model;
        }
    }
}
