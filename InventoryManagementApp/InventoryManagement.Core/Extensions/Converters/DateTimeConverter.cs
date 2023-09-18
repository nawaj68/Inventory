using System;

namespace InventoryManagement.Core.Extensions.Converters
{
    public static class DateTimeConverter
    {
        public static long UnixTimestampConversion(DateTime dateTime)
        {
            long unixTime = (long)(TimeZoneInfo.ConvertTimeToUtc(DateTime.UtcNow)
                           - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;

            return unixTime;
        }

        public static string ToMetlifeFormat(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMddhhmmssfffffff");
        }

        public static DateTime ToDateTime(string dateString, string dateFormat = "dd-MM-yyyy")
        {
            try
            {
                var datetime = DateTime.ParseExact(dateString, dateFormat, System.Globalization.CultureInfo.InvariantCulture);

                return datetime;
            }
            catch (Exception)
            {
                throw new ArgumentException($"Date string '{dateString}' is not valid, can not parse.");
            }
        }

        public static DateTime? ToNullableDateTime(string dateString, string dateFormat = "dd-MM-yyyy")
        {
            if (string.IsNullOrEmpty(dateString))
                return null;

            return ToDateTime(dateString, dateFormat);
        }
    }
}
