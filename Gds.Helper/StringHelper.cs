using System;
using System.Collections.Generic;
using System.Linq;

namespace Gds.Helper
{
    /// <summary>
    /// Provide helper methods for string.
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Parse string to Dictionary
        /// </summary>
        /// <param name="stringData">String</param>
        /// <param name="propertyDelimiter">Delimiter properties</param>
        /// <param name="keyValueDelimiter">Delimiter key values</param>
        /// <returns></returns>
        public static Dictionary<string, string> ToParseDictionary(
            this string stringData, 
            char propertyDelimiter = ';', 
            char keyValueDelimiter = '=')
        {
            var result = new Dictionary<string, string>();
            Array.ForEach(stringData.Split(propertyDelimiter), s =>
            {
                var item = s.Split(keyValueDelimiter);
                if(item.Length < 1)
                    return;
                if (result.ContainsKey(item[0]))
                    result[item[0]] = item.Length > 1 ? item[1] : null;
                else
                    result.Add(item[0], item.Length > 1 ? item[1] : null);

            });

            return result;
        }
        /// <summary>
        /// Create new string only digit chars. 
        /// </summary>
        /// <param name="sValue">Input string.</param>
        /// <returns>String only digits.</returns>
        public static string OnlyDigits(this string sValue) => new string(sValue.Where(char.IsDigit).ToArray());
        /// <summary>
        /// Create new string only letter. 
        /// </summary>
        /// <param name="sValue">Input string.</param>
        /// <returns>String only letters.</returns>
        public static string OnlyLetters(this string sValue) => new string(sValue.Where(char.IsLetter).ToArray());
        /// <summary>
        /// Create new string digit and letter.
        /// </summary>
        /// <param name="sValue">Input string.</param>
        /// <returns>String letter and digit.</returns>
        public static string OnlyLettersOrDigits(this string sValue) => new string(sValue.Where(char.IsLetterOrDigit).ToArray());
    }
}
