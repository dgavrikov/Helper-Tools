using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Gds.Helper.IO
{
    /// <summary>
    /// MD5 hash Helper
    /// </summary>
    public static class Md5HashHelper
    {
        /// <summary>
        ///  Convert hash to string
        /// </summary>
        /// <param name="hash">hash </param>
        /// <returns>Hash string value.</returns>
        private static string HashToString(this IEnumerable<byte> hash)
            => hash.Aggregate("", (current, t) => current + $"{t:X1}");
        /// <summary>
        /// Calculate Md5 hash.
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>Md5 hash</returns>
        public static string CalculateMd5ForFile(this string fileName)
        {
            if (!File.Exists(fileName)) throw new FileNotFoundException("File not found.", fileName);

            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                var outHashSum = md5.ComputeHash(fs);
                return outHashSum.HashToString();
            }

        }

        /// <summary>
        /// Compare hash file name and hash string
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <param name="hash">hash string</param>
        /// <returns></returns>
        public static bool CompareHashFilename(string fileName, string hash) => hash == fileName.CalculateMd5ForFile();


    }
}
