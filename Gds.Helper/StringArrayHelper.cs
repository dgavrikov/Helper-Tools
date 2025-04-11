using System;
using System.Collections.Generic;

namespace Gds.Helper
{
    /// <summary>
    /// Provide helper methods for string array.
    /// </summary>
    public static class StringArrayHelper
    {
        /// <summary>
        /// Parse String array to Dictionary
        /// </summary>
        /// <param name="args">string array</param>
        /// <param name="keyValueDelimiter">KeyValue delimiter</param>
        /// <returns>Dictionary list</returns>
        public static Dictionary<string, string>? ParseToDictionary(
            string[] args,
            char keyValueDelimiter = '=')
            {
                if ((args == null) || (args.Length <= 0)) return null;
                var result = new Dictionary<string, string>(args.Length);
                Array.ForEach(args, s =>
                {
                    var item = s.Split(keyValueDelimiter);
                    if (item.Length < 1) return;
                    if (result.ContainsKey(item[0]))
                        result[item[0]] = item.Length > 1 ? item[1] : string.Empty;
                    else
                        result.Add(item[0], item.Length > 1 ? item[1] : string.Empty);
                });
                return result;
            }
    }
}
