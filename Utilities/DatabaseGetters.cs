namespace MochaHomeAccounting.Utilities
{
    using System;
    using System.Data;
    using log4net;
    using Microsoft.Data.SqlClient;

    /// <summary>
    /// Run queries against the DB for fetching data.
    /// </summary>
    public class DatabaseGetters
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Execute queries against the database & retrieve resulting dataset.
        /// </summary>
        /// <param name="query">SQL query text to be executed.</param>
        /// <param name="connectionStringName">Connection string for connecting to the database.</param>
        /// <returns>DataTable object containing query search results.</returns>
        public static DataTable GetDataTable(string query, string connectionStringName)
        {
            SqlConnection sqlConnection = new (new DataBaseConnection().GetConfigurationValue(connectionStringName));

            try
            {
                sqlConnection.Open();
                Log.Info("Db Query " + query);
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.CommandTimeout = 60;
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                return dataTable;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }

            return null;
        }
    }
}
