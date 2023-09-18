using Newtonsoft.Json;
using System;

namespace InventoryManagement.Core.Extensions
{
    public class JsonDateTimeConverter : JsonConverter
    {
        private readonly string _dateFormat;

        public JsonDateTimeConverter(string dateFormat)
        {
            _dateFormat = dateFormat;
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(DateTime) || objectType == typeof(DateTime?));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string rawDate = (string)reader.Value;

            try
            {
                return DateTime.ParseExact(rawDate, _dateFormat, null);
            }
            catch
            {
                if (objectType == typeof(DateTime?))
                    return null;

                return DateTime.MinValue;
            }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
