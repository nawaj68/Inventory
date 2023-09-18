using System;

namespace InventoryManagement.Core.Extensions
{
    public static class ValidatorExtensions
    {
        public static string DefaultDateFormat = "yyyy/MM/dd";
        public static string MsSqlDateFormat = "yyyy-MM-ddThh:mm:ss";   //yyyy-MM-ddThh:mm:ss.fffZ

        public static bool IsValidString(this string value, bool isRequire = false, int? length = null)
        {
            bool valid = true;

            if (isRequire && string.IsNullOrEmpty(value))
                valid = false;

            if (!string.IsNullOrEmpty(value) && length < value.Length)
                valid = false;

            return valid;
        }


        public static bool IsValidDate(this string dateString, bool isRequired, string format = null)
        {
            if (string.IsNullOrEmpty(format))
                format = DateTimeExtension.DefaultFormat;

            try
            {
                if (isRequired)
                {
                    if (string.IsNullOrEmpty(dateString))
                        return false;
                }
                else
                {
                    if (string.IsNullOrEmpty(dateString))
                        return true;
                }

                DateTime.ParseExact(dateString, format, null);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsValidDecimal(this string decimalString, bool isRequired, decimal max = 99999999999.99M)
        {
            if (isRequired)
            {
                if (string.IsNullOrEmpty(decimalString))
                    return false;
            }
            else
            {
                if (string.IsNullOrEmpty(decimalString))
                    return true;
            }

            try
            {
                var d = Convert.ToDecimal(decimalString);
                if (d <= max)
                    return true;

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static DateTime ToDateTime(string dateString, string varName, string format = null)
        {
            if (string.IsNullOrEmpty(format))
                format = DefaultDateFormat;

            try
            {
                return DateTime.ParseExact(dateString, format, null);
            }
            catch
            {
                throw new ArgumentException($"{varName} '{dateString}' is not recognized as a valid DateTime.");
            }
        }

        public static string ToDateTimeString(this DateTime value, string varName, string format = null)
        {
            if (string.IsNullOrEmpty(format))
                format = DefaultDateFormat;

            try
            {
                return value.ToString(format);
            }
            catch
            {
                throw new ArgumentException($"{varName} is not recognized as a valid DateTime.");
            }
        }

        public static string ToNullableDateTimeString(this DateTime? value, string varName, string format = null)
        {
            if (value == null)
                return string.Empty;

            if (string.IsNullOrEmpty(format))
                format = DefaultDateFormat;

            try
            {
                return value.Value.ToString(format);
            }
            catch
            {
                throw new ArgumentException($"{varName} is not recognized as a valid DateTime.");
            }
        }

        public static DateTime? ToNullableDateTime(string dateString, string varName, string format = null)
        {
            if (string.IsNullOrEmpty(format))
                format = DefaultDateFormat;

            try
            {
                if (string.IsNullOrEmpty(dateString))
                    return null;

                return DateTime.ParseExact(dateString, format, null);
            }
            catch
            {
                throw new ArgumentException($"{varName} is not recognized as a valid DateTime.");
            }
        }
    }
}
