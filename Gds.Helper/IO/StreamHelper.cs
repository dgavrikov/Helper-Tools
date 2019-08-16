using System;
using System.IO;
using System.IO.Compression;

namespace Gds.Helper.IO
{
    /// <summary>
    /// Provide helper method for Stream.
    /// </summary>
    public static class StreamHelper
    {
        /// <summary>
        /// Get stream from file.
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>Data stream.</returns>
        public static byte[] GetStreamFromFile(
            this string fileName)
        {
            byte[] resultBytes;
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    using (var br = new BinaryReader(fs))
                    {
                        resultBytes = br.ReadBytes((int) fs.Length);
                        br.Close();
                    }
                }
                finally
                {
                    fs.Close();
                }
            }

            return resultBytes;
        }

        /// <summary>
        /// Get compress stream from file. Use DeflateStream.
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="compressMethod">Compression method</param>
        /// <returns>Compress stream.</returns>
        public static byte[] GetCompressStreamFromFile(
            this string fileName,
            CompressMethod compressMethod = CompressMethod.Deflate)
        {
            var tmpFile = Path.GetTempFileName();
            FileStream fs = null;
            Stream zip = null;
            try
            {
                switch (compressMethod)
                {
                    case CompressMethod.Deflate:
                        zip = new DeflateStream(new FileStream(tmpFile, FileMode.Create, FileAccess.Write),
                            CompressionMode.Compress);
                        break;
                    case CompressMethod.GZip:
                        zip = new GZipStream(new FileStream(tmpFile, FileMode.Create, FileAccess.Write),
                            CompressionMode.Compress);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(compressMethod));
                }

                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                fs.CopyTo(zip);
            }
            catch (Exception)
            {
                if (File.Exists(tmpFile))
                    File.Delete(tmpFile);
                throw;
            }
            finally
            {
                zip?.Flush();
                zip?.Close();
                zip?.Dispose();
                fs?.Close();
                fs?.Dispose();
            }

            byte[] res = null;
            try
            {
                if (File.Exists(tmpFile))
                    res = GetStreamFromFile(tmpFile);
            }
            finally
            {
                if (File.Exists(tmpFile))
                    File.Delete(tmpFile);
            }

            return res;
        }

        /// <summary>
        /// Save stream to file.
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="fileName">Filename</param>
        /// <param name="replace">Override file</param>
        public static void WriteStreamToFile(
            this string fileName,
            byte[] stream,
            bool replace = true)
        {
            if (replace && File.Exists(fileName))
                File.Delete(fileName);
            using (var bw = new BinaryWriter(new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite)))
            {
                try
                {
                    bw.Write(stream);
                }
                finally
                {
                    bw.Flush();
                    bw.Close();
                }
            }
        }

        /// <summary>
        /// Save compress stream to file.
        /// </summary>
        /// <param name="stream">Compress stream.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="compressMethod">Compress method.</param>
        /// <param name="replace">Override file.</param>
        public static void WriteCompressStreamToFile(
            this string fileName,
            byte[] stream,
            CompressMethod compressMethod = CompressMethod.Deflate,
            bool replace = true)
        {
            if (replace && File.Exists(fileName))
                File.Delete(fileName);
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite))
            using (var ms = new MemoryStream(stream))
            {
                Stream zs;
                switch (compressMethod)
                {
                    case CompressMethod.Deflate:
                        zs = new DeflateStream(ms, CompressionMode.Decompress);
                        break;
                    case CompressMethod.GZip:
                        zs = new GZipStream(ms, CompressionMode.Decompress);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(compressMethod));
                }

                try
                {
                    zs.CopyTo(fs);
                }
                finally
                {
                    zs.Flush();
                    zs.Close();
                }
            }

        }
    }

    public enum CompressMethod
    {
        Deflate,
        GZip
    }
}
