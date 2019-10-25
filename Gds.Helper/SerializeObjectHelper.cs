using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Gds.Helper
{
    /// <summary>
    /// Provide serialize an deserialize methods. 
    /// </summary>
    public static class SerializeObjectHelper
    {
        /// <summary>
        /// Serialize object to xml.
        /// </summary>
        /// <param name="obj">Serialize object.</param>
        /// <returns>Serialize string.</returns>
        public static string SerializeToXml(this object obj)
        {
            if (obj == null)
                return "null";

            var x = new XmlSerializer(obj.GetType());

            byte[] b;

            using (var ms = new MemoryStream())
            {
                x.Serialize(ms, obj);
                b = ms.ToArray();
            }

            return Encoding.UTF8.GetString(b);
        }

        /// <summary>
        /// Deserialize object from xml string.
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="obj">Xml string</param>
        /// <returns>Object instance</returns>
        public static T DeserializeFromXml<T>(this string obj)
        {
            var x = new XmlSerializer(typeof(T));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(obj)))
            { return (T)x.Deserialize(ms); }
        }
    }
}
