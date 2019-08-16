using System;

namespace Gds.Helper
{
    /// <summary>
    /// Enum helper class
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Get enum by string value.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">String value</param>
        /// <returns>Enum value</returns>
        public static T ToEnum<T>(this string value) => (T)Enum.Parse(typeof(T), value, true);

        /// <summary>
        /// Get enum by string value or default value.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">String value</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>Enum value</returns>
        public static T ToEnum<T>(this string value, T defaultValue) where T : struct =>
            Enum.TryParse(value, true, out T result) ? result : defaultValue;

        /// <summary>
        /// Get enum by int value.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Integer value</param>
        /// <returns>Enum value</returns>
        public static T ToEnum<T>(this int value)
        {
            var name = Enum.GetName(typeof(T), value);
            return name.ToEnum<T>();
        }

        /// <summary>
        /// Get enum by int value or default value.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Integer value</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>Enum value</returns>
        public static T ToEnum<T>(this int value, T defaultValue) where T : struct
        {
            var name = Enum.GetName(typeof(T), value);
            return name.ToEnum(defaultValue);
        }
    }

}
