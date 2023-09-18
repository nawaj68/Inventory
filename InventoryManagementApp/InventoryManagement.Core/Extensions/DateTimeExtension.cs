using System;

namespace InventoryManagement.Core.Extensions
{
    public static class DateTimeExtension
    {
        public static string DefaultFormat = "dd/MM/yyyy";

        public static bool IsGreaterThan(this DateTime dt1, DateTime dt2)
        {
            return dt1 > dt2;
        }

        public static bool IsLessThan(this DateTime dt1, DateTime dt2)
        {
            return dt1 < dt2;
        }

        public static DateTime ToUtcStartTime(this DateTime startDate)
        {
            return startDate.Date.ToUniversalTime();
        }

        public static DateTime ToUtcEndTime(this DateTime endDate)
        {
            return endDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59).ToUniversalTime();
        }

        public static string GetDateTimestampMetlifeFormat(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMddhhmmssfffffff");
        }
    }
}
