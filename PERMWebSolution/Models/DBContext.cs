using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PERMWebSolution.Models
{
    /// <summary>
    /// Class for DB connection management and data querying
    /// </summary>
    public static class DBContext
    {
        /// <summary>
        /// Static variable holds connection string 
        /// </summary>
        private static string conString = ConfigurationManager.ConnectionStrings["PERMLOCALServer"].ConnectionString; 
        //Connection string name should be configured to everyone local account configuration must be done in <--Web.config--> file

        /// <summary>
        /// Method executes query on database that does not have any parameters
        /// </summary>
        /// <param name="query">Query to execute</param>
        /// <returns>DataTable object</returns>
        public static DataTable ExecuteQueryTable(string query)
        {
            return ExecuteQueryTable(query, null);
        }
        /// <summary>
        /// Method executes query on database that has parameters
        /// </summary>
        /// <param name="query">Query to execute</param>
        /// <param name="parameters">Parameters to substitute into query</param>
        /// <returns>DataTable object</returns>
        public static DataTable ExecuteQueryTable(string query, Dictionary<string, object> parameters)
        {
            DataTable tbl = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query; //if procedure will be called this must be changed to procedure

                        if (parameters != null)
                        {
                            foreach (string parameter in parameters.Keys)
                            {
                                cmd.Parameters.AddWithValue(parameter, parameters[parameter]);
                            }
                        }
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(tbl); //actually executes query
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null; // If null is returned that mean query failed!
            }
            return tbl;
        }
    }
}