using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace InventoryManagement.Core.Extensions
{
    public static class StringExtension
    {
        public static string ToHeaderCase(this string str)
        {
            return Regex.Replace(str, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ");
        }

        public static string GetHederDescription(this string value)
        {
            try
            {
                FieldInfo fi = value.GetType().GetField(value);

                if (fi == null) return "Description Not Found";

                DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

                if (attributes != null && attributes.Any())
                {
                    return attributes.First().Description;
                }

                //return value.ToString();
                return string.Join(" ", Regex.Split(value, @"([A-Z]?[a-z]+)").Where(str => !string.IsNullOrEmpty(str)));
            }
            catch (Exception e)
            {
                return "Description Not Found";
            }
        }

        public static string ToSentenceCase(this string str)
        {
            return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
        }
        public static string RemoveAllWhiteSpace(this string str)
        {
            return str.Replace(" ", "");
        }

        public static bool Equal(this string value, string input, bool ignoreCase = true)
        {
            if (ignoreCase)
                return value.Trim().Equals(input.Trim(), StringComparison.OrdinalIgnoreCase);
            else
                return value.Trim().Equals(input);
        }

        public static bool EqualNot(this string value, string input, bool ignoreCase = true)
        {
            return !value.Equal(input, ignoreCase);
        }

        public static string ToQuotedString(this string inputString)
        {
            if(string.IsNullOrEmpty(inputString))
                return "''";

            return "'" + inputString + "'";
        }

        public static string ToFixedLengthString(this string inputString, int stringLength)
        {
            if (string.IsNullOrEmpty(inputString))
                return "";
            else
            {
                if (inputString.Length > stringLength)
                    return inputString.Substring(0, stringLength);
                else
                    return inputString;
            }
        }

        public static IEnumerable<string> SplitBy(this string str, int chunkLength)
        {
            for (int i = 0; i < str.Length; i += chunkLength)
            {
                if (chunkLength + i > str.Length)
                    chunkLength = str.Length - i;

                yield return str.Substring(i, chunkLength);
            }
        }

        public static string FDDtToDate(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return "";

            try
            {
                return str.Substring(6, 2) + "/" + str.Substring(4, 2) + "/" + str.Substring(0, 4); 
            }
            catch(Exception e)
            {
                return "";
            }

        }

        public static string MetlifeDateToParsable(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return "";

            try
            {
                return str.Substring(4, 2) + "-" + str.Substring(6, 2) + "-" + str.Substring(0, 4);
            }
            catch (Exception e)
            {
                return "";
            }

        }

        public static string AccountInfoMasking(this string str)
        {
            if (string.IsNullOrWhiteSpace(str) || str.Length <= 3) return "Invalid BankAccount Information";

            try
            {
                string newStr = string.Empty;

                if (str.Length < 8)
                {

                    newStr = "***" + str.Substring(str.Length - 3);
                }
                else
                {
                    newStr = str.Substring(0, 2) + "***" + str.Substring(str.Length - 5);
                }

                return newStr;
            }
            catch (Exception e)
            {
                return "";
            }

        }

        public static string PartialString(this string str, string firstPat,string secondPart)
        {
            if (String.IsNullOrEmpty(str))
                return "";

            int sub1Index = str.LastIndexOf(firstPat);
            int sub2Index = str.LastIndexOf(secondPart);
            if (sub1Index == -1 || sub2Index == -1)
                return "";
            
            var substring = new StringBuilder();
            for (int i = sub1Index; i < sub2Index; i++)
            {
                substring.Append(str[i]);
            }
            return substring.ToString();
        }


        public static string ToTwoDecimalPlaces(this string inputString)
        {

            if (string.IsNullOrWhiteSpace(inputString)) return  "0.00"; 

            try
            { 
                return String.Format("{0:0.00}", Convert.ToDecimal(inputString));
            }
            catch (Exception e)
            {
                return "0.00";
            }
           
        }

    }
}
