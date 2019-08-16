using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Gds.Helper.Data
{
    /// <summary>
    /// Provide helper method from DataTable.
    /// </summary>
    public static class DataTableHelper
    {
        /// <summary>
        /// Convert List-object model to DataTable model.
        /// </summary>
        /// <typeparam name="T">Type of List</typeparam>
        /// <param name="iList">List object.</param>
        /// <returns>DataTable result list.</returns>
        public static DataTable ToDataTable<T>(this List<T> iList)
        {
            var dataTable = new DataTable();
            var propertyDescriptorCollection =
                TypeDescriptor.GetProperties(typeof(T));
            for (var i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                var propertyDescriptor = propertyDescriptorCollection[i];
                var type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);

                dataTable.Columns.Add(propertyDescriptor.Name, type ?? throw new ArgumentNullException(nameof(type)));
            }
            var values = new object[propertyDescriptorCollection.Count];
            foreach (var iListItem in iList)
            {
                for (var i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}
