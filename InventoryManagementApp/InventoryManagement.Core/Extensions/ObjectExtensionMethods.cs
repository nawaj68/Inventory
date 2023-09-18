using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InventoryManagement.Core.Extensions
{
    public static class ObjectExtensionMethods
    {
        public static void CopyPropertiesFrom(this object self, object parent)
        {
            var fromProperties = parent.GetType().GetProperties();
            var toProperties = self.GetType().GetProperties();
            var notMapProperty = new string[] { "CreatedDateUtc", "CreatedBy", "UpdatedBy", "UpdatedDateUtc" };

            foreach (var fromProperty in fromProperties)
            {
                foreach (var toProperty in toProperties)
                {
                    if (fromProperty.Name == toProperty.Name && fromProperty.PropertyType == toProperty.PropertyType && !notMapProperty.Contains(fromProperty.Name))
                    {
                        toProperty.SetValue(self, fromProperty.GetValue(parent));
                        break;
                    }
                }
            }
        }

        public static T ToTypeOf<T>(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var data = JsonConvert.DeserializeObject<T>(json);
            return data;
        }

        public static T ToTypeOf<T>(this Dictionary<string, object> obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var data = JsonConvert.DeserializeObject<T>(json);
            return data;
        }

        public static Dictionary<string, object> ToDictionary<T>(this T obj) where T : class
        {
            var json = JsonConvert.SerializeObject(obj);
            var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            var dictionary = new Dictionary<string, object>();
            foreach (var value in values)
            {
                if (value.Value is JObject)
                {
                    dictionary.Add(value.Key, ToDictionary(value.Value));
                }
                else
                {
                    dictionary.Add(value.Key, value.Value);
                }
            }
            return dictionary;
        }

        public static Dictionary<string, string> ToDictionary<T>(this T obj, string dateFormat = null) where T : class
        {
            var properties = obj.GetType()
                 .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                 .ToList();
            var dic = new Dictionary<string, string>();
            properties.ForEach(prop =>
            {
                var data = prop.GetValue(obj, null);
                string key = prop.Name;
                string value;
                if (!string.IsNullOrEmpty(dateFormat)
                    && (prop.PropertyType == typeof(DateTime?)
                        || prop.PropertyType == typeof(DateTime)
                        || prop.PropertyType == typeof(DateTimeOffset?)
                        || prop.PropertyType == typeof(DateTimeOffset))
                    && data is not null)
                {
                    value = ((DateTime)prop.GetValue(obj, null)).ToString(dateFormat);
                }
                else
                {
                    value = data?.ToString();
                }

                var attr = Attribute.GetCustomAttribute(prop, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attr is not null)
                    key = attr.Description;

                dic.Add(key, value);
            });

            return dic;
        }
    }
}
