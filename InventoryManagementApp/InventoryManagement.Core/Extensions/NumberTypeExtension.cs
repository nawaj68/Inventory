using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Core.Extensions
{
    public static class NumberTypeExtension
    {
        public static string ToTwoDecimalPlaces(this decimal number)
        {
            return String.Format("{0:0.00}", number);
        }

        public static string ToTwoDecimalPlaces(this decimal? number)
        {
            if (number == null) return "0.00";

            return String.Format("{0:0.00}", number);
        }

        public static string ToTwoDecimalPlaces(this float number)
        {
            return String.Format("{0:0.00}", number);
        }

        public static string ToTwoDecimalPlaces(this float? number)
        {
            if (number == null) return "0.00";

            return String.Format("{0:0.00}", number);
        }
    }
}
