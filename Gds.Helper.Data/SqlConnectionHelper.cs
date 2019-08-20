using System;
using System.Data.SqlClient;
using System.Data;

namespace Gds.Helper.Data
{
    /// <summary>
    /// Provide helper method for SqlConnection.
    /// </summary>
    public static class SqlConnectionHelper
    {
        /// <summary>
        /// Open SqlConnection and execute optimal SET'S for queries.
        /// </summary>
        /// <param name="connection">SqlConnection</param>
        /// <param name="logException">Log delegate.</param>
        /// <returns>Open or not.</returns>
        public static bool ReConnect(this SqlConnection connection, Action<SqlException> logException = null)
        {
            if (connection.State == ConnectionState.Open) return true;
            try
            {
                connection.Open();
                using (var sql = connection.CreateCommand())
                {
                    sql.CommandText = @"SET ANSI_WARNINGS ON";
                    try
                    {
                        sql.ExecuteNonQuery();
                    }
                    catch (SqlException) { }

                    sql.CommandText = @"SET ARITHABORT ON";
                    try
                    {
                        sql.ExecuteNonQuery();
                    }
                    catch (SqlException) { }
                }
                return true;
            }
            catch (SqlException sqlException)
            {
                logException?.Invoke(sqlException);
                return false;
            }
        }
    }
}
